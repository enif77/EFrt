/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using System;
    
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Add n to the loop index. If the loop index did not cross the boundary between the loop limit
    /// minus one and the loop limit, continue execution at the beginning of the loop. Otherwise,
    /// discard the current loop control parameters and continue execution immediately following the loop.
    /// Compilation: [index of the word following DO/?DO -- ], runtime: (n -- )
    /// </summary>
    public class PlusLoopWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public PlusLoopWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "+LOOP";
            IsImmediate = true;
            Action = () =>
            {
                if (Interpreter.IsCompiling == false)
                {
                    throw new Exception("+LOOP outside a new word definition.");
                }

                Interpreter.ReturnStackExpect(1);

                var cWordIndex = Interpreter.RPop();

                var loopIndex = Interpreter.WordBeingDefined.AddWord(
                    new PlusLoopControlWord(
                        Interpreter,
                        (cWordIndex + 1) - Interpreter.WordBeingDefined.NextWordIndex));  // c + 1 -> index of the word following DO/?DO.

                var cWord = Interpreter.WordBeingDefined.GetWord(cWordIndex);
                if (cWord is IBranchingWord)
                {
                    ((IBranchingWord)cWord).SetBranchTargetIndex(loopIndex);
                }

                return 1;
            };
        }
    }
}

/*

\ Example:

: GD2 DO I DUP . -1 +LOOP CR ;

1 4 GD2 -> 4 3 2
-1 2 GD2 -> 2 1 0
  
 */