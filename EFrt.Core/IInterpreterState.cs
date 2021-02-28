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
        /// Stack for floating point numbers.
        /// </summary>
        FloatingPointStack FloatingPointStack { get; }

        /// <summary>
        /// Optional stack for user data.
        /// </summary>
        ObjectStack ObjectStack { get; }

        /// <summary>
        /// Return stack.
        /// </summary>
        ReturnStack ReturnStack { get; }

        /// <summary>
        /// Exception stack.
        /// </summary>
        ExceptionStack ExceptionStack { get; }

        /// <summary>
        /// Stack for input sources.
        /// </summary>
        InputSourceStack InputSourceStack { get; }

        /// <summary>
        /// Heap - variables etc.
        /// </summary>
        ByteHeap Heap { get; }

        /// <summary>
        /// Heap - variables etc.
        /// </summary>
        ObjectHeap ObjectHeap { get; }

        /// <summary>
        /// The "address" of the STATE variable.
        /// </summary>
        int StateVariableAddress { get; }

        /// <summary>
        /// The "address" of the BASE variable.
        /// </summary>
        int BaseVariableAddress { get; }

        /// <summary>
        /// The list of known words.
        /// </summary>
        IWordsList WordsList { get; }


        /// <summary>
        /// Cleans up this state.
        /// </summary>
        void Reset();

        /// <summary>
        /// Sets the STATE variable value.
        /// </summary>
        /// <param name="value">A new STATE variable value.</param>
        void SetStateValue(bool value);

        /// <summary>
        /// Sets the BASE variable value.
        /// </summary>
        /// <param name="value">A new BASE variable value.</param>
        void SetBaseValue(int value);
    }
}
