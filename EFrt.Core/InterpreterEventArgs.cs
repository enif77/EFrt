/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;

    using EFrt.Core.Words;


    /// <summary>
    /// Interpreter event args.
    /// </summary>
    public class InterpreterEventArgs : EventArgs
    {
        /// <summary>
        /// A word, that will be executed or was executed.
        /// </summary>
        public IWord Word { get; set; } 
    }
}
