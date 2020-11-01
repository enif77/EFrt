/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
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
            Integer = 2
        }

        /// <summary>
        /// A token code.
        /// </summary>
        public TokenType Code { get; private set; }

        /// <summary>
        /// An integer value of the integer token.
        /// </summary>
        public int IValue { get; private set; }

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
        /// Creates a token representing an integer constant.
        /// </summary>
        /// <param name="i">An integer constant.</param>
        /// <returns>A token representing an integer constant.</returns>
        public static Token CreateIntegerToken(int i)
        {
            return new Token() { Code = TokenType.Integer, IValue = i };
        }

        
    }
}
