﻿/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


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
            Name = "WHILE";
            IsControlWord = true;
            Action = () =>
            {
                Interpreter.StackExpect(1);

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
            };

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
    }
}
