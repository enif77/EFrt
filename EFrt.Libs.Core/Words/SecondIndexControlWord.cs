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
            Name = "J";
            IsControlWord = true;
            Action = () =>
            {
                Interpreter.ReturnStackExpect(1);
                Interpreter.StackFree(1);

                // ( -- outer-index)  [ outer-limit outer-index inner-limit inner-index -- outer-limit outer-index inner-limit inner-index ]
                Interpreter.Push(Interpreter.RPick(2));

                return 1;
            };
        }
    }
}
