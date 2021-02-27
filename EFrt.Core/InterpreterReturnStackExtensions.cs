/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;


    /// <summary>
    /// Extensions method for the return stack manipulations.
    /// </summary>
    public static class InterpreterReturnStackExtensions
    {
        #region stack

        // Return stack.

        /// <summary>
        /// Returns a value from the return stack at certain index.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the return stack.</returns>
        public static int RPick(this IInterpreter interpreter, int index)
        {
            return interpreter.State.ReturnStack.Pick(index);
        }

        /// <summary>
        /// Returns a value from the top of the return stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <returns>A value from the top of the return stack.</returns>
        public static int RPeek(this IInterpreter interpreter)
        {
            return interpreter.State.ReturnStack.Peek();
        }

        /// <summary>
        /// Removes a value from the top of the return stack and returns it.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <returns>A value from the top of the return stack.</returns>
        public static int RPop(this IInterpreter interpreter)
        {
            return interpreter.State.ReturnStack.Pop();
        }

        /// <summary>
        /// Inserts a value to the return stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="value">A value.</param>
        public static void RPush(this IInterpreter interpreter, int value)
        {
            interpreter.State.ReturnStack.Push(value);
        }

        /// <summary>
        /// Drops N values from the top of the return stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="count">The number of values to be dropped from the top of the return stack.</param>
        public static void RDrop(this IInterpreter interpreter, int count = 1)
        {
            interpreter.State.ReturnStack.Drop();
        }

        /// <summary>
        /// Duplicates the top value on the return stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void RDup(this IInterpreter interpreter)
        {
            interpreter.State.ReturnStack.Dup();
        }

        #endregion


        #region stack checks

        /// <summary>
        /// Expects N items on the return stack.
        /// Wont return (throws an InterpreterException), if not enough items are on the return stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="expectedItemsCount">The number of stack items expected on the return stack.</param>
        public static void ReturnStackExpect(this IInterpreter interpreter, int expectedItemsCount)
        {
            if (expectedItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedItemsCount));

            if (interpreter.State.ReturnStack.Count < expectedItemsCount)
            {
                throw new InterpreterException(-4, "return stack underflow");
            }
        }

        /// <summary>
        /// Expects N free items on the return stack, so N items can be pushed to the return stack.
        /// Wont return (throws an InterpreterException), if there is not enough free items on the return stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="expectedFreeItemsCount">The number of free return stack items expected.</param>
        public static void ReturnStackFree(this IInterpreter interpreter, int expectedFreeItemsCount)
        {
            if (expectedFreeItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedFreeItemsCount));

            if ((interpreter.State.ReturnStack.Count + expectedFreeItemsCount) >= interpreter.State.ReturnStack.Items.Length)
            {
                throw new InterpreterException(-3, "return stack overflow");
            }
        }

        #endregion
    }
}
