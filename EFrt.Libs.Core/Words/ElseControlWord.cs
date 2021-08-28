/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is defining IF-THEN-ELSE condition.
    /// </summary>
    public class ElseControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="indexFollowingElse">The index of a word following this ELSE word.</param>
        public ElseControlWord(IInterpreter interpreter, int indexFollowingElse)
            : base(interpreter)
        {
            Name = "ELSE";
            IsControlWord = true;
            Action = () => _thenIndexIncrement - _indexFollowingElse + 1;  // Skip words inside of the ELSE block.

            _indexFollowingElse = indexFollowingElse;
            _thenIndexIncrement = 0;
        }


        /// <summary>
        /// Sets the offset for jumping to the THEN word.
        /// </summary>
        public void SetThenIndexIncrement(int thenIndexIncrement)
        {
            _thenIndexIncrement = thenIndexIncrement;
        }


        private readonly int _indexFollowingElse;
        private int _thenIndexIncrement;
    }
}
