/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    /// <summary>
    /// A word that is definig IF-THEN-ELSE condition.
    /// </summary>
    public class IfControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="currentIndex">The index of this IF word inside of a non primitive word.</param>
        public IfControlWord(IInterpreter interpreter, int currentIndex)
            : base(interpreter)
        {
            Name = "IfControlWord";
            IsControlWord = true;
            Action = Execute;

            _thisIndex = currentIndex;
            _elseIndexIncrement = 0;
            _thenIndexIncrement = 0;
        }


        /** Volá slovo then. Vloží sem svůj index v rámci definovaného slova,
	 *  do kterého patří. */
        public void SetThenIndex(int thenIndex)
        {
            _thenIndexIncrement = thenIndex - _thisIndex;
        }

        /** Volá slovo else. Vloží sem svůj index v rámci definovaného slova,
         *  do kterého patří. */
        public void SetElseIndex(int elseIndex)
        {
            _elseIndexIncrement = elseIndex - _thisIndex;
        }


        private int _thisIndex;
        private int _elseIndexIncrement;
        private int _thenIndexIncrement;


        private int Execute()
        {
            if (Interpreter.Pop() != 0)
            {
                // The flag is true, advance instruction index by one to true portion of IF.
                return 1;
            }
            else
            {
                // The flag is false, advance to ELSE if present or to THEN if not.
                if (_elseIndexIncrement != 0)
                {
                    return _elseIndexIncrement;
                }
                else
                {
                    return _thenIndexIncrement;
                }
            }
        }
    }
}
