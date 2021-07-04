/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Extensions
{
    using System;
    
    
    /// <summary>
    /// Extensions method for the object stack manipulations.
    /// </summary>
    public static class InterpreterObjectStackExtensions
    {
        #region stack
                
        /// <summary>
        /// Returns a value from the object stack at certain index.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the object stack.</returns>
        public static object OPick(this IInterpreter interpreter, int index)
        {
            return interpreter.State.ObjectStack.Pick(index);
        }

        /// <summary>
        /// Returns a value from the top of the object stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <returns>A value from the top of the object stack.</returns>
        public static object OPeek(this IInterpreter interpreter)
        {
            return interpreter.State.ObjectStack.Peek();
        }

        /// <summary>
        /// Removes a value from the top of the object stack and returns it.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <returns>A value from the top of the object stack.</returns>
        public static object OPop(this IInterpreter interpreter)
        {
            return interpreter.State.ObjectStack.Pop();
        }

        /// <summary>
        /// Inserts a value to the object stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="value">A value.</param>
        public static void OPush(this IInterpreter interpreter, object value)
        {
            interpreter.State.ObjectStack.Push(value);
        }

        /// <summary>
        /// Drops N values from the top of the object stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="count">The number of values to be dropped from the top of the object stack.</param>
        public static void ODrop(this IInterpreter interpreter, int count = 1)
        {
            interpreter.State.ObjectStack.Drop();
        }

        /// <summary>
        /// Duplicates the top value on the object stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void ODup(this IInterpreter interpreter)
        {
            interpreter.State.ObjectStack.Dup();
        }

        /// <summary>
        /// Swaps two values on the top of the object stack.
        /// ( a b -- b a )
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void OSwap(this IInterpreter interpreter)
        {
            interpreter.State.ObjectStack.Swap();
        }

        /// <summary>
        /// Gets a value below the top of the stack and pushes it to the object stack.
        /// (a b -- a b a)
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void OOver(this IInterpreter interpreter)
        {
            interpreter.State.ObjectStack.Over();
        }

        /// <summary>
        /// Rotates the top three object stack values.
        /// (a b c -- b c a)
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void ORot(this IInterpreter interpreter)
        {
            interpreter.State.ObjectStack.Rot();
        }

        /// <summary>
        /// Rotates indexth item on the object stack to the top of the object stack.
        /// </summary>
        /// <param name="index">A stack item index, where 0 = stack top, 1 = first below top, etc.</param>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void ORoll(this IInterpreter interpreter, int index)
        {
            interpreter.State.ObjectStack.Roll(index);
        }

        #endregion


        #region stack functions

        /// <summary>
        /// obj-stack[top] = func(obj-stack[top])
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="func">A function.</param>
        public static void SFunction(this IInterpreter interpreter, Func<string, string> func)
        {
            var stack = interpreter.State.ObjectStack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top].ToString());
        }

        /// <summary>
        /// obj-stack[top] = func(obj-stack[top - 1], obj-stack[top])
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="func">A function.</param>
        public static void SFunction(this IInterpreter interpreter, Func<string, string, string> func)
        {
            var stack = interpreter.State.ObjectStack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1].ToString(), stack.Items[top].ToString());
        }

        #endregion


        #region stack checks

        /// <summary>
        /// Expects N items on the object stack.
        /// Wont return (throws an InterpreterException), if not enough items are on the object stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="expectedItemsCount">The number of stack items expected on the object stack.</param>
        public static void ObjectStackExpect(this IInterpreter interpreter, int expectedItemsCount)
        {
            if (expectedItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedItemsCount));

            if (interpreter.State.ObjectStack.Count < expectedItemsCount)
            {
                throw new InterpreterException(-4, "object stack underflow");
            }
        }

        /// <summary>
        /// Expects N free items on the object stack, so N items can be pushed to the object stack.
        /// Wont return (throws an InterpreterException), if there is not enough free items on the object stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="expectedFreeItemsCount">The number of free object stack items expected.</param>
        public static void ObjectStackFree(this IInterpreter interpreter, int expectedFreeItemsCount)
        {
            if (expectedFreeItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedFreeItemsCount));

            if ((interpreter.State.ObjectStack.Count + expectedFreeItemsCount) >= interpreter.State.ObjectStack.Items.Length)
            {
                throw new InterpreterException(-3, "object stack overflow");
            }
        }

        #endregion
    }
}
