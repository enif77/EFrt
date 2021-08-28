/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Reserves a single cell of data heap, initialising it to n.
    /// </summary>
    public class CommaWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public CommaWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = ",";
            Action = () =>
            {
                Interpreter.StackExpect(1);
                Interpreter.CheckCellAlignedHereAddress();            
            
                Interpreter.State.Heap.Write(Interpreter.State.Heap.AllocCells(1), Interpreter.Pop());
            
                return 1;
            };
        }
    }
}