/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is defining condition in the BEGIN-WHILE-REPEAT loop.
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


        /// <summary>
        /// Called by the word THEN. It inserts its index inside a word its defined in.
        /// </summary>
        /// <param name="repeatIndex">The REPEAT word index.</param>
        public void SetRepeatIndex(int repeatIndex)
        {
            _repeatIndexIncrement = repeatIndex - _thisIndex;
        }


        private readonly int _thisIndex;
        private int _repeatIndexIncrement;
    }
}
