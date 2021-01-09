/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    /// <summary>
    /// Extension methods for the IInterpreter interface.
    /// </summary>
    public static class InterpreterExtensions
    {
        #region execution

        /// <summary>
        /// Executes a string as a FORTH program.
        /// </summary>
        /// <param name="src">A FORTH program source.</param>
        public static void Execute(this IInterpreter interpreter, string src)
        {
            interpreter.Execute(new StringSourceReader(src));
        }

        #endregion
    }
}
