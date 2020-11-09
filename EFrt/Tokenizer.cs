/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using System.Globalization;


    /// <summary>
    /// Splits the source into tokens.
    /// </summary>
    public class Tokenizer
    {
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

                return CurrentChar = (char)0;
            }

            return CurrentChar = _src[SourcePos];
        }

        /// <summary>
        /// Extracts the next token from the source.
        /// Expect the NextChar() to be called at least once at the beginning of the source processing.
        /// </summary>
        /// <returns></returns>
        public Token NextTok()
        {
            SkipWhite();

            //var sb = new StringBuilder();
            //while (IsWhite() == false && CurrentChar != 0)
            //{
            //    sb.Append(CurrentChar);

            //    NextChar();
            //}

            // var word = sb.ToString();


            var firsWorCharIndex = SourcePos;
            while (IsWhite() == false && CurrentChar != 0)
            {
                NextChar();
            }

            var word = _src.Substring(firsWorCharIndex, SourcePos - firsWorCharIndex);
            if (string.IsNullOrEmpty(word) && CurrentChar == 0)
            {
                return Token.CreateEofToken();
            }

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
