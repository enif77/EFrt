/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Stacks
{
    using EFrt.Words;


    /// <summary>
    /// Control flow stack. Used in the compilation phase.
    /// </summary>
    public class ControlFlowStack : AStackBase<IWord>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="capacity">This stack capacity.</param>
        public ControlFlowStack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}