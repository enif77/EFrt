/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    public interface IBranchingWord
    {
        /// <summary>
        /// Sets a branch terget index.
        /// </summary>
        /// <param name="branchIndex">A branch target index.</param>
        void SetBranchTargetIndex(int branchIndex);
    }
}
