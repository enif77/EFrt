/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is returning the innermost loop index.
    /// </summary>
    public class IndexControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public IndexControlWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "I";
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.StackFree(1);
                Interpreter.ReturnStackExpect(1);

                // ( -- inner-index)  [ ... inner-limit inner-index -- ... inner-limit inner-index ]
                Interpreter.Push(Interpreter.RPeek());

                return 1;
            };
        }
    }
}
