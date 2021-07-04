/* EFrt - (C) 2021 Premysl Fara  */

using EFrt.Core.Extensions;

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Adds n to the contents at the address a-addr.
    /// (n a-addr -- )
    /// </summary>
    public class PlusStoreWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public PlusStoreWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "+!";
            Action = () =>
            {
                Interpreter.StackExpect(2);

                var addr = Interpreter.Pop();
            
                Interpreter.CheckCellAlignedAddress(addr);
            
                Interpreter.State.Heap.Write(addr, Interpreter.State.Heap.ReadInt32(addr) + Interpreter.Pop());

                return 1;
            };
        }
    }
}