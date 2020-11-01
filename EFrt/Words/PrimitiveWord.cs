/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    using System;


    /// <summary>
    /// A word that is definig itself.
    /// </summary>
    public class PrimitiveWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A name.</param>
        /// <param name="isImmediate">If this word is immediate.</param>
        /// <param name="action">An action, this word is doing.</param>
        public PrimitiveWord(IInterpreter interpreter, string name, bool isImmediate, Action action, int sourcePos = -1)
            : base(interpreter)
        {
            Name = name;
            IsImmediate = isImmediate;
            Action = action;
            SourcePos = sourcePos;
        }
    }
}
