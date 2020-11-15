/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    /// <summary>
    /// A word that is returnning the innermost loop index.
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
            Name = "IndexControlWord";
            IsControlWord = true;
            Action = Execute;
        }


        private int Execute()
        {
            // ( -- inner-index)  [ ... inner-limit inner-index -- ... inner-limit inner-index ]
            Interpreter.Pushi(Interpreter.RPeek());

            return 1;
        }
    }
}
