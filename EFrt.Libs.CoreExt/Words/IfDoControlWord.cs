/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.CoreExt.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is defining ?DO loop beginning.
    /// </summary>
    public class IfDoControlWord : AWordBase, IBranchingWord
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="currentIndex">The index of this ?DO word inside of a non primitive word.</param>
        public IfDoControlWord(IInterpreter interpreter, int currentIndex)
            : base(interpreter)
        {
            Name = "?DO";
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.StackExpect(2);
                Interpreter.ReturnStackFree(2);

                var index = Interpreter.Pop();
                var limit = Interpreter.Pop();

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
            };

            _thisIndex = currentIndex;
            _loopIndexIncrement = 0;
        }

        
        public void SetBranchTargetIndex(int branchIndex)
        {
            _loopIndexIncrement = branchIndex - _thisIndex;
        }


        private readonly int _thisIndex;
        private int _loopIndexIncrement;
    }
}
