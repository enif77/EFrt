﻿/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    /// <summary>
    /// A word that is definig ?DO loop begining.
    /// </summary>
    public class IfDoControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="currentIndex">The index of this ?DO word inside of a non primitive word.</param>
        public IfDoControlWord(IInterpreter interpreter, int currentIndex)
            : base(interpreter)
        {
            Name = "IfDoControlWord";
            IsControlWord = true;
            Action = Execute;

            _thisIndex = currentIndex;
            _loopIndexIncrement = 0;
        }


        /** Volá slovo LOOP or +LOOP. Vloží sem svůj index v rámci definovaného slova,
	     *  do kterého patří. */
        public void SetLoopIndex(int loopIndex)
        {
            _loopIndexIncrement = loopIndex - _thisIndex;
        }


        private int _thisIndex;
        private int _loopIndexIncrement;


        private int Execute()
        {
            var index = Interpreter.Popi();
            var limit = Interpreter.Popi();

            Interpreter.RPush(limit);
            Interpreter.RPush(index);

            if (limit == index)
            {
                return _loopIndexIncrement;
            }
            else
            {
                return 1;
            }
        }
    }
}
