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
        Heap Heap { get; }

        /// <summary>
        /// Heap - variables etc.
        /// </summary>
        ObjectHeap ObjectHeap { get; }

        /// <summary>
        /// The working area for PICTURED numbers.
        /// </summary>
        string Picture { get; set; }

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
        /// Checks, if a stack with a certain name is registered.
        /// </summary>
        /// <param name="stackName">An unique stack name.</param>
        bool IsStackRegistered(string stackName);

        /// <summary>
        /// Registers an new stack instance.
        /// Throws an exception, if a stack with the same name already exists. 
        /// </summary>
        /// <param name="stackName">An unique stack name.</param>
        /// <param name="stack">A IStack instance.</param>
        void RegisterStack(string stackName, IStack stack);

        /// <summary>
        /// Returns a stack with a specific name.
        /// Throws an exception, if no such stack exists.
        /// </summary>
        /// <param name="stackName">A stack name.</param>
        /// <returns>An IStack instance.</returns>
        IStack GetRegisteredStack(string stackName);

        /// <summary>
        /// Removes a stack from registered stacks.
        /// Throws an exception, if no such stack exists.
        /// </summary>
        /// <param name="stackName">A stack name.</param>
        void RemoveRegisteredStack(string stackName);
        
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
