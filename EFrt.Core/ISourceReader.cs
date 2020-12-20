/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    public interface ISourceReader
    {
        /// <summary>
        /// The last read (current) character.
        /// </summary>
        char CurrentChar { get; }

        /// <summary>
        /// The current char position in the source.
        /// </summary>
        int SourcePos { get; }


        /// <summary>
        /// Reads the next character from the input.
        /// </summary>
        /// <returns>The next character from the input or 0 at the end of the source.</returns>
        char NextChar();
    }
}
