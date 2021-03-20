/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using System;
    
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Converts ud1 to char using the BASE value and adds it to the State.Picture buffer.
    /// (ud1 -- ud2)
    /// </summary>
    public class NumberSignWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public NumberSignWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "#";
            Action = () =>
            {
                Interpreter.StackExpect(2);

                // TODO: Check, that we are inside <# and #> words.
                
                var n1 = Interpreter.DPop();

                var n3 = Math.DivRem(n1, Interpreter.State.Heap.ReadInt32(Interpreter.State.BaseVariableAddress), out var n2);
                n2 = Math.Abs(n2);
                Interpreter.State.Picture = (n2 < 10 ? (char)(n2 + '0') : (char)((n2 - 10) + 'A')) + Interpreter.State.Picture;

                Interpreter.DPush(n3);

                return 1;
            };
        }
    }
}