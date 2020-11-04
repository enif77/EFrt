/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    /// <summary>
    /// Defines an output writer.
    /// </summary>
    public interface IOutputWriter
    {
        /// <summary>
        /// Writes the text representation of the specified array of objects to an output using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        void Write(string format, params object[] arg);

        /// <summary>
        /// Writes a line terminator to an output.
        /// </summary>
        void WriteLine();

        /// <summary>
        /// Writes the text representation of the specified array of objects, followed by a line terminator, to an output using the specified format information.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="arg">An array of objects to write using format.</param>
        void WriteLine(string format, params object[] arg);
    }
}
