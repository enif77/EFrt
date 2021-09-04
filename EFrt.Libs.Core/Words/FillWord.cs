/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Stacks;
    using EFrt.Core.Words;

    
    /// <summary>
    /// If u is greater than zero, store char in each of u consecutive characters of memory beginning at c-addr.
    /// ( c-addr u char -- )
    /// </summary>
    public class FillWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public FillWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "FILL";
            Action = () =>
            {
                Interpreter.StackExpect(3);

                var c = (char)Interpreter.Pop();
                var count = Interpreter.Pop();
                if (count > 0)
                {
                    var addr = Interpreter.Pop();     
                
                    Interpreter.CheckCharAlignedAddress(addr);
                    Interpreter.CheckAddressesRange(addr, count * Heap.CharSize);
                
                    Interpreter.State.Heap.Fill(addr, count, c);
                }
                else
                {
                    Interpreter.Drop();
                }

                return 1;
            };
        }
    }
}

/*

\ Example:

10 5 33 FILL
12 C@ . CR

-> 33 
  
20 C@ . CR

-> 0  
  
 */
 