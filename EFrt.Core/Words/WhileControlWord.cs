/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word that is definig condition in the BEGIN-WHILE-REPEAT loop.
    /// </summary>
    public class WhileControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="currentIndex">The index of this WHILE word inside of a non primitive word.</param>
        public WhileControlWord(IInterpreter interpreter, int currentIndex)
            : base(interpreter)
        {
            Name = "WhileControlWord";
            IsControlWord = true;
            Action = Execute;

            _thisIndex = currentIndex;
            _repeatIndexIncrement = 0;
        }


        /** Volá slovo then. Vloží sem svůj index v rámci definovaného slova,
	 *  do kterého patří. */
        public void SetRepeatIndex(int repeatIndex)
        {
            _repeatIndexIncrement = repeatIndex - _thisIndex;
        }


        private int _thisIndex;
        private int _repeatIndexIncrement;


        private int Execute()
        {
            if (Interpreter.Pop() != 0)
            {
                // The flag is true, advance instruction index by one to loop body.
                return 1;
            }
            else
            {
                // The flag is false, advance to a word behind the REPEAT word.
                return _repeatIndexIncrement + 1;
            }
        }
    }
}
