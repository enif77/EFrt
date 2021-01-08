/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is returnning the outer loop index.
    /// </summary>
    public class SecondIndexControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public SecondIndexControlWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "SecondIndexControlWord";
            IsControlWord = true;
            Action = Execute;
        }


        private int Execute()
        {
            Interpreter.ReturnStackExpect(1);
            Interpreter.StackFree(1);

            // ( -- outer-index)  [ outer-limit outer-index inner-limit inner-index -- outer-limit outer-index inner-limit inner-index ]

            Interpreter.Push(Interpreter.RPick(2));

            //var innerIndex = Interpreter.RPop();
            //var innerLimit = Interpreter.RPop();

            //Interpreter.Pushi(Interpreter.RPeek());

            //Interpreter.RPush(innerLimit);
            //Interpreter.RPush(innerIndex);

            return 1;
        }
    }
}
