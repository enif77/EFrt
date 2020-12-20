/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    /// <summary>
    /// A token.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Defines known token types.
        /// </summary>
        public enum TokenType
        { 
            Eof = -1,
            Unknown = 0,
            Word = 1,
            SingleCellInteger = 2,
            DoubleCellInteger = 3,
            Float = 4
        }

        /// <summary>
        /// A token code.
        /// </summary>
        public TokenType Code { get; private set; }

        /// <summary>
        /// A single cell integer value of the Integer token.
        /// </summary>
        public int IValue { get; private set; }

        /// <summary>
        /// A double cell integer value of the Long token.
        /// </summary>
        public long LValue { get; private set; }

        /// <summary>
        /// An float/real value of the Float token.
        /// </summary>
        public float FValue { get; private set; }

        /// <summary>
        /// A name of a word or a string value of the string token.
        /// </summary>
        public string SValue { get; private set; }


        /// <summary>
        /// Creates the end-of-file (EOF) token.
        /// </summary>
        /// <returns>An EOF representing token.</returns>
        public static Token CreateEofToken()
        {
            return new Token() { Code = TokenType.Eof };
        }

        /// <summary>
        /// Creates a token representing a word.
        /// </summary>
        /// <param name="word">A word name.</param>
        /// <returns>A token representing a word.</returns>
        public static Token CreateWordToken(string word)
        {
            return new Token() { Code = TokenType.Word, SValue = word };
        }

        /// <summary>
        /// Creates a token representing a single cell integer constant.
        /// </summary>
        /// <param name="i">An integer constant.</param>
        /// <returns>A token representing an integer constant.</returns>
        public static Token CreateSingleCellIntegerToken(int i)
        {
            return new Token() { Code = TokenType.SingleCellInteger, IValue = i };
        }

        /// <summary>
        /// Creates a token representing a double cell integer constant.
        /// </summary>
        /// <param name="i">An integer constant.</param>
        /// <returns>A token representing an integer constant.</returns>
        public static Token CreateDoubleCellIntegerToken(long i)
        {
            return new Token() { Code = TokenType.DoubleCellInteger, LValue = i };
        }

        /// <summary>
        /// Creates a token representing a float/real constant.
        /// </summary>
        /// <param name="f">A float constant.</param>
        /// <returns>A token representing a float constant.</returns>
        public static Token CreateFloatToken(float f)
        {
            return new Token() { Code = TokenType.Float, FValue = f };
        }
    }
}
