/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
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
            Interpreter.Push(Interpreter.RPeek());

            return 1;
        }
    }
}
