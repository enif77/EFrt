/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word that is definig loop begining (the first loop body word index).
    /// </summary>
    public class BeginControlWord : AWordBase
    {
        /// <summary>
        /// The index of a word folowing this BEGIN word.
        /// </summary>
        public int IndexFollowingBegin { get; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="indexFollowingBegin">The index of a word folowing this BEGIN word.</param>
        public BeginControlWord(IInterpreter interpreter, int indexFollowingBegin)
            : base(interpreter)
        {
            Name = "BeginControlWord";
            IsControlWord = true;
            Action = Execute;

            IndexFollowingBegin = indexFollowingBegin;
        }


        private int Execute()
        {
            return IndexFollowingBegin;
        }
    }
}
