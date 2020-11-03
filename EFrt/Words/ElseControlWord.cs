/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    /// <summary>
    /// A word that is definig IF-THEN-ELSE condition.
    /// </summary>
    public class ElseControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="indexFollowingElse">The index of a word folowing this ELSE word.</param>
        public ElseControlWord(IInterpreter interpreter, int indexFollowingElse)
            : base(interpreter)
        {
            Name = "ElseControlWord";
            IsControlWord = true;
            Action = Execute;

            _indexFollowingElse = indexFollowingElse;
            _thenIndexIncrement = 0;
        }


        /** Offset pro skok na THEN. */
        public void SetThenIndexIncrement(int thenIndexIncrement)
        {
            _thenIndexIncrement = thenIndexIncrement;
        }


        private int _indexFollowingElse;
        private int _thenIndexIncrement;


        private int Execute()
        {
            // Skip words inside of the ELSE block.
            return _thenIndexIncrement - _indexFollowingElse + 1;
        }
    }
}
