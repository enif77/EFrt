/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    using System;


    /// <summary>
    /// A word that is definig itself.
    /// </summary>
    public class ImmediateWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        /// <param name="name">A name.</param>
        /// <param name="action">An action, this word is doing.</param>
        public ImmediateWord(IInterpreter interpreter, string name, Func<int> action)
            : base(interpreter)
        {
            Name = name;
            IsImmediate = true;
            Action = action;
        }
    }
}
