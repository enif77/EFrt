/* EFrt - (C) 2020 - 2021 Premysl Fara  */

using EFrt.Core.Extensions;

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is defining loop end.
    /// </summary>
    public class LoopControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="indexIncrement">Offset back to a word folowing the DO word.</param>
        public LoopControlWord(IInterpreter interpreter, int indexIncrement)
            : base(interpreter)
        {
            Name = "LOOP";
            IsControlWord = true;
            Action = Execute;

            _incrementToWordFollowingDo = indexIncrement;
        }


        /// <summary>
        /// The index of a word folowing this DO word.
        /// </summary>
        private readonly int _incrementToWordFollowingDo;


        private int Execute()
        {
            Interpreter.ReturnStackExpect(2);

            var index = Interpreter.RPop();
            var limit = Interpreter.RPeek();

            index += 1;

            // Is the loop limit reached?
            if (index >= limit)
            {
                // Yes we're done. Pop limit off of variable stack and
                // return a positive one instruction increment.
                Interpreter.RPop();

                return 1;

            }
            else
            {
                // Loop index has not been reached. Push new index value
                // and return negative instruction increment to cause
                // control to return to word immediately following the DO word.
                Interpreter.RPush(index);

                return _incrementToWordFollowingDo;
            }
        }
    }
}
