namespace EFrt.Core
{
    /// <summary>
    /// Handles all input reading.
    /// </summary>
    public interface IInputSource : ISourceReader
    {
        /// <summary>
        /// Extracts the next token from the source.
        /// Expect the NextChar() to be called at least once at the beginning of the source processing.
        /// </summary>
        /// <returns>A token.</returns>
        Token NextTok();
        
        /// <summary>
        /// Returns a word name following in the input stream.
        /// </summary>
        /// <param name="toUpperCase">If true (the default), returned word is converted to UPPERCASE.</param>
        /// <returns>A word name.</returns>
        string ParseWord(bool toUpperCase = true);
        
        /// <summary>
        /// Gets a string terminated by a terminator char.
        /// </summary>
        /// <param name="terminator">A character terminating the string.</param>
        /// <param name="allowSpecialChars">If true, '\' escaped special chars are supported.</param>
        /// <param name="skipLeadingTerminators">If true, leading terminator chars are skipped.</param>
        /// <returns>A string.</returns>
        string ParseTerminatedString(char terminator, bool allowSpecialChars = false, bool skipLeadingTerminators = false);

        /// <summary>
        /// Parses a single or double cell integer or a floating point number.
        /// It is called by the interpreter directly, because a word must be checked, if it is defined/known, 
        /// before it is parsed as a number.
        /// </summary>
        Token ParseNumber(string word, bool allowLeadingWhite = false, bool allowTrailingWhite = false, bool allowTrailingChars = false);
        
        /// <summary>
        /// Parses an integer number from a string.
        /// </summary>
        /// <param name="s">A string containing a number.</param>
        /// <param name="success">True, if parsing succeeded.</param>
        /// <returns>A parsed number.</returns>
        long ParseIntegerNumber(string s, out bool success);
        
        /// <summary>
        /// Parses a floating point number from a string.
        /// </summary>
        /// <param name="s">A string containing a number.</param>
        /// <param name="success">True, if parsing succeeded.</param>
        /// <returns>A parsed number.</returns>
        double ParseFloatingPointNumber(string s, out bool success);
    }
}