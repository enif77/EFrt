/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using System;
    
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;

    
    /// <summary>
    /// 
    /// (ud1 -- ud2)
    /// </summary>
    public class NumberSignSWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public NumberSignSWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "#S";
            Action = () =>
            {
                Interpreter.StackExpect(2);

                // TODO: Check, that we are inside <# and #> words.
                
                var numBase = Interpreter.State.Heap.ReadInt32(Interpreter.State.BaseVariableAddress);
                var n1 = Interpreter.DPop();

                while (n1 != 0)
                {
                    n1 = Math.DivRem(Math.Abs(n1), numBase, out var n3);
                    n3 = Math.Abs(n3);
                    Interpreter.State.Picture = (n3 < 10 ? (char)(n3 + '0') : (char)((n3 - 10) + 'A')) + Interpreter.State.Picture;
                }

                Interpreter.DPush(n1);

                return 1;
            };
        }
    }
}