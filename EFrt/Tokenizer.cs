/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using System;
    using System.Globalization;
    using System.Text;


    /// <summary>
    /// Splits the source into tokens.
    /// </summary>
    public class Tokenizer
    {
        public const char EoF = (char)0;

        private string _src;

        /// <summary>
        /// The last read (current) character.
        /// </summary>
        public char CurrentChar { get; private set; }

        /// <summary>
        /// The current char position in the source.
        /// </summary>
        public int SourcePos { get; set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="source">A program source.</param>
        public Tokenizer(string source)
        {
            _src = source;
            SourcePos = -1;
        }


        /// <summary>
        /// Reads the next character from the input.
        /// </summary>
        /// <returns>The next character from the input or 0 at the end of the source.</returns>
        public char NextChar()
        {
            SourcePos++;
            if (SourcePos >= _src.Length)
            {
                SourcePos = _src.Length;

                return CurrentChar = EoF;
            }

            return CurrentChar = _src[SourcePos];
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

                // A "string"?
                case '"': return ParseString('"');
                case '\'': return ParseString('\'');

                // A word or an integert?
                default: return ParseWord();
            }
        }


        private Token ParseString(char stringTerminatorChar)
        {
            // Eat the stringTerminatorChar.
            NextChar();

            var sb = new StringBuilder();

            while (CurrentChar != EoF)
            {
                // TODO: Parse escape characters.

                if (CurrentChar == stringTerminatorChar)
                {
                    // Eat '"'.
                    NextChar();

                    // We expect an EOF or a whitespace after the string literal.
                    if (IsWhite() == false && CurrentChar != EoF)
                    {
                        throw new Exception("A whitespace or EOF after the end of a string literal expected.");
                    }

                    return Token.CreateStringToken(sb.ToString());
                }

                sb.Append(CurrentChar);

                NextChar();
            }

            throw new Exception("The end of the string literal expected.");
        }


        private Token ParseWord()
        {
            var sb = new StringBuilder();

            while (CurrentChar != EoF && IsWhite() == false)
            {
                sb.Append(CurrentChar);

                NextChar();
            }

            return ParseNumber(sb.ToString());
        }


        private Token ParseNumber(string word)
        {
            if (int.TryParse(word, NumberStyles.Integer, CultureInfo.InvariantCulture, out var iVal))
            {
                return Token.CreateIntegerToken(iVal);
            }

            return Token.CreateWordToken(word);
        }


        private bool IsWhite()
        {
            return (CurrentChar == ' ' || CurrentChar == '\t' || CurrentChar == '\r' || CurrentChar == '\n');
        }


        private void SkipWhite()
        {
            while (IsWhite())
            {
                NextChar();
            }
        }
    }
}
