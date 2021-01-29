/* EFrt - (C) 2020 - 2021 Premysl Fara  */

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
        /// Return stack.
        /// </summary>
        ReturnStack ReturnStack { get; }

        /// <summary>
        /// Exception stack.
        /// </summary>
        ExceptionStack ExceptionStack { get; }

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
