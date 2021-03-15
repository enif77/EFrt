/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Stores the value n into the address addr (a heap array index).
    /// (n a-addr -- )
    /// </summary>
    public class StoreWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public StoreWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "!";
            Action = () =>
            {
                Interpreter.StackExpect(2);
            
                var addr = Interpreter.Pop();
            
                Interpreter.CheckCellAlignedAddress(addr);
           
                Interpreter.State.Heap.Write(addr, Interpreter.Pop());

                return 1; 
            };
        }
    }
}