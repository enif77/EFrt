/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    using System;
    using System.Text;


    /// <summary>
    /// Splits the source into tokens.
    /// </summary>
    public class Tokenizer
    {
        public const char EoF = (char)0;

        /// <summary>
        /// The last read (current) character.
        /// </summary>
        public char CurrentChar => _sourceReader.CurrentChar;

        /// <summary>
        /// The current char position in the source.
        /// </summary>
        public int SourcePos => _sourceReader.CurrentChar;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="source">A program source.</param>
        public Tokenizer(ISourceReader sourceReader)
        {
            _sourceReader = sourceReader;
        }


        /// <summary>
        /// Reads the next character from the input.
        /// </summary>
        /// <returns>The next character from the input or 0 at the end of the source.</returns>
        public char NextChar()
        {
            return _sourceReader.NextChar();
        }

        /// <summary>
        /// Extracts the next token from the source.
        /// Expect the NextChar() to be called at least once at the beginning of the source processing.
        /// </summary>
        /// <returns>A token.</returns>
        public Token NextTok()
        {
            SkipWhite();

            switch (CurrentChar)
            {
                // EOF?
                case EoF: return Token.CreateEofToken();

                // A word or a number?
                default: return ParseWord();
            }
        }


        private ISourceReader _sourceReader;


        private Token ParseWord()
        {
            var sb = new StringBuilder();

            while (CurrentChar != EoF && IsWhite(CurrentChar) == false)
            {
                sb.Append(CurrentChar);

                NextChar();
            }

            return Token.CreateWordToken(sb.ToString());
        }

        /// <summary>
        /// Parses a single or double cell integer or a real number.
        /// It is called by the interpreter directly, because a word must be checked, if it is defined/known, 
        /// before it is parsed as a number.
        /// unsigned-single-cell-integer :: digit-sequence .
        /// unsigned-double-cell-integer :: digit-sequence ( 'L' | 'l' ) .
        /// unsigned-number :: unsigned-single-cell-integer | unsigned-double-cell-integer | unsigned-real .
        /// unsigned-real :: ( digit-sequence '.' fractional-part [ 'e' scale-factor ] ) | ( digit-sequence 'e' scale-factor ) .
        /// scale-factor :: [ sign ] digit-sequence .
        /// fractional-part :: digit-sequence .
        /// sign :: '+' | '-' .
        /// </summary>
        public Token ParseNumber(string word, bool allowLeadingWhite = false, bool allowTrailingWhite = false, bool allowTrailingChars = false)
        {
            var sourceReader = new StringSourceReader(word);

            // Read the first char of the word.
            sourceReader.NextChar();

            var isFloatingPoint = false;
            var isDoubleCellInteger = false;
            var integerValue = 0L;
            var floatingPointValue = 0.0;

            // Skip leading white chars.
            while (allowLeadingWhite && IsWhite(sourceReader.CurrentChar))
            {
                sourceReader.NextChar();
            }

            var sign = 1;
            if (sourceReader.CurrentChar == '-')
            {
                sign = -1;
                sourceReader.NextChar();
            }
            else if (sourceReader.CurrentChar == '+')
            {
                sourceReader.NextChar();
            }

            var haveDigit = false;
            while (IsDigit(sourceReader.CurrentChar))
            {
                haveDigit = true;
                integerValue = (integerValue * 10) + (sourceReader.CurrentChar - '0');

                // An integer number (long) overflow.
                if (integerValue < 0)
                {
                    //throw new Exception("An integer constant overflow: " + word);
                    return Token.CreateWordToken(word);
                }

                sourceReader.NextChar();
            }

            // Check, that we have atleast one digit here.
            if (haveDigit == false)
            {
                // No digit yet = badly formatted number.
                return Token.CreateWordToken(word);
            }

            // A double cell integer?
            if (sourceReader.CurrentChar == 'L' || sourceReader.CurrentChar == 'l')
            {
                isDoubleCellInteger = true;
                sourceReader.NextChar();
            }

            // A floating point number?
            if (sourceReader.CurrentChar == 'D' || sourceReader.CurrentChar == 'd')
            {
                if (isDoubleCellInteger)
                {
                    // LD = bad number suffix.
                    return Token.CreateWordToken(word);
                }

                floatingPointValue = integerValue;
                isFloatingPoint = true;
                sourceReader.NextChar();
            }

            // digit-sequence '.' fractional-part
            if (sourceReader.CurrentChar == '.')
            {
                // 123L. is not allowed.
                if (isDoubleCellInteger)
                {
                    //throw new Exception("Unexpected character in double cell constant: " + word);
                    return Token.CreateWordToken(word);
                }

                if (isFloatingPoint == false)
                {
                    floatingPointValue = integerValue;
                    isFloatingPoint = true;
                }
                
                // Eat '.'.
                sourceReader.NextChar();

                if (IsDigit(sourceReader.CurrentChar) == false)
                {
                    //throw new Exception("A fractional part of a real number expected.");
                    return Token.CreateWordToken(word);
                }

                var scale = 1.0;
                var frac = 0.0;
                while (IsDigit(sourceReader.CurrentChar))
                {
                    frac = (frac * 10.0) + (sourceReader.CurrentChar - '0');
                    scale *= 10.0;

                    sourceReader.NextChar();
                }

                floatingPointValue += frac / scale;
            }

            // digit-sequence [ '.' fractional-part ] 'e' scale-factor
            if (sourceReader.CurrentChar == 'e' || sourceReader.CurrentChar == 'E')
            {
                // 123Le is not allowed.
                if (isDoubleCellInteger)
                {
                    //throw new Exception("Unexpected character in double cell constant: " + word);
                    return Token.CreateWordToken(word);
                }

                if (isFloatingPoint == false)
                {
                    floatingPointValue = integerValue;
                    isFloatingPoint = true;
                }

                // Eat 'e'.
                sourceReader.NextChar();

                // scale-factor :: [ sign ] digit-sequence .
                var scaleFactorSign = 1.0;
                if (sourceReader.CurrentChar == '-')
                {
                    scaleFactorSign = -1.0;
                    sourceReader.NextChar();
                }
                else if (sourceReader.CurrentChar == '+')
                {
                    sourceReader.NextChar();
                }
                
                if (IsDigit(sourceReader.CurrentChar) == false)
                {
                    //throw new Exception("A scale factor of a real number expected.");
                    return Token.CreateWordToken(word);
                }

                var fact = 0.0;
                while (IsDigit(sourceReader.CurrentChar))
                {
                    fact = (fact * 10.0) + (sourceReader.CurrentChar - '0');

                    sourceReader.NextChar();
                }

                floatingPointValue *= Math.Pow(10, fact * scaleFactorSign);
            }

            // Skip leading white chars.
            while (allowTrailingWhite && IsWhite(sourceReader.CurrentChar))
            {
                sourceReader.NextChar();
            }

            // We expect to eat all chars from a word while parsing a number.
            if (sourceReader.CurrentChar != EoF && allowTrailingChars == false)
            {
                return Token.CreateWordToken(word);
            }

            // We eat all chars, its a number.
            if (isFloatingPoint)
            {
                return Token.CreateFloatToken((float)(floatingPointValue * sign));
            }
            else if (isDoubleCellInteger)
            {
                return Token.CreateDoubleCellIntegerToken(integerValue * sign);
            }
            else
            {
                integerValue *= sign;

                if (integerValue < int.MinValue || integerValue > int.MaxValue)
                {
                    // A double cell integer must be marked as such type with the L suffix.
                    return Token.CreateWordToken(word);
                }

                // A single cell integer found.
                return Token.CreateSingleCellIntegerToken((int)integerValue);
            }
        }

        /// <summary>
        /// Checks, if a character is a white character.
        /// white-char :: SPACE | TAB | CR | LF .
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a white character.</returns>
        public static bool IsWhite(char c)
        {
            return char.IsWhiteSpace(c);
        }


        /// <summary>
        /// Checks, if an character is a digit.
        /// digit :: '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' .
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a digit.</returns>
        public static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }


        public void SkipWhite()
        {
            while (IsWhite(CurrentChar))
            {
                NextChar();
            }
        }
    }
}
