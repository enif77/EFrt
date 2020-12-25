/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    using EFrt.Core.Stacks;


    /// <summary>
    /// Defines an interpreter state.
    /// </summary>
    public interface IInterpreterState
    {
        /// <summary>
        /// Data stack.
        /// </summary>
        Stack Stack { get; }

        /// <summary>
        /// Optional stack for user data.
        /// </summary>
        ObjectStack ObjectStack { get; }

        /// <summary>
        /// Return stack.
        /// </summary>
        ReturnStack ReturnStack { get; }

        /// <summary>
        /// Heap - variables etc.
        /// </summary>
        Heap Heap { get; }

        /// <summary>
        /// Heap - variables etc.
        /// </summary>
        ObjectHeap ObjectHeap { get; }

        /// <summary>
        /// The list of known words.
        /// </summary>
        IWordsList WordsList { get; }


        /// <summary>
        /// Cleans up this state.
        /// </summary>
        void Reset();
    }
}
