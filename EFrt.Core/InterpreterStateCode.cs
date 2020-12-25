/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    /// <summary>
    /// Defines a state, in which an interpreter can be.
    /// </summary>
    public enum InterpreterStateCode
    {
        /// <summary>
        /// The interpreter is in the interpretation mode.
        /// </summary>
        Interpreting,

        /// <summary>
        /// The interpreter is compiling a new word, variable or constant.
        /// </summary>
        Compiling,

        /// <summary>
        /// The interpreter just executed the QUIT word. The current interpretation is terminated and can be resumed.
        /// </summary>
        Breaking,

        /// <summary>
        /// The interpreter just executed the BYE word. The current interpretation is terminated and should not be restart.
        /// </summary>
        Terminating
    }
}
