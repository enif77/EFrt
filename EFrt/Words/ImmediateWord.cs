/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
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
