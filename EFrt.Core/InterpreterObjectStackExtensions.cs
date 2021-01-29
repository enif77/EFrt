/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;


    /// <summary>
    /// Extensions method for the object stack manipulations.
    /// </summary>
    public static class InterpreterObjectStackExtensions
    {
        #region stacks
                
        /// <summary>
        /// Returns a value from the object stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the object stack.</returns>
        public static object OPick(this IInterpreter interpreter, int index)
        {
            return interpreter.State.Stack.Pick(index).ObjectValue;
        }

        /// <summary>
        /// Returns a value from the top of the object stack.
        /// </summary>
        /// <returns>A value from the top of the object stack.</returns>
        public static object OPeek(this IInterpreter interpreter)
        {
            return interpreter.State.Stack.Peek().ObjectValue;
        }

        /// <summary>
        /// Removes a value from the top of the object stack and returns it.
        /// </summary>
        /// <returns>A value from the top of the object stack.</returns>
        public static object OPop(this IInterpreter interpreter)
        {
            return interpreter.State.Stack.Pop().ObjectValue;
        }

        /// <summary>
        /// Inserts a value to the object stack.
        /// </summary>
        /// <param name="value">A value.</param>
        public static void OPush(this IInterpreter interpreter, object value)
        {
            interpreter.State.Stack.Push(value);
        }

        /// <summary>
        /// Drops N values from the top of the object stack.
        /// </summary>
        /// <param name="count">The number of values to be dropped from the top of the object stack.</param>
        public static void ODrop(this IInterpreter interpreter, int count = 1)
        {
            interpreter.State.Stack.Drop();
        }

        /// <summary>
        /// Duplicates the top value on the object stack.
        /// </summary>
        public static void ODup(this IInterpreter interpreter)
        {
            interpreter.State.Stack.Dup();
        }

        /// <summary>
        /// Swaps two values on the top of the object stack.
        /// ( a b -- b a )
        /// </summary>
        public static void OSwap(this IInterpreter interpreter)
        {
            interpreter.State.Stack.Swap();
        }

        /// <summary>
        /// Gets a value below the top of the stack and pushes it to the object stack.
        /// (a b -- a b a)
        /// </summary>
        public static void OOver(this IInterpreter interpreter)
        {
            interpreter.State.Stack.Over();
        }

        /// <summary>
        /// Rotates the top three object stack values.
        /// (a b c -- b c a)
        /// </summary>
        public static void ORot(this IInterpreter interpreter)
        {
            interpreter.State.Stack.Rot();
        }

        /// <summary>
        /// Rotates indexth item on the object stack to the top of the object stack.
        /// </summary>
        /// <param name="index">A stack item index, where 0 = stack top, 1 = first below top, etc.</param>
        public static void ORoll(this IInterpreter interpreter, int index)
        {
            interpreter.State.Stack.Roll(index);
        }

        #endregion


        #region stack functions

        /// <summary>
        /// obj-stack[top] = func(obj-stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void SFunction(this IInterpreter interpreter, Func<string, string> func)
        {
            var stack = interpreter.State.Stack;
            stack.Items[stack.Top].StringValue = func(stack.Items[stack.Top].StringValue);
        }

        /// <summary>
        /// obj-stack[top] = func(obj-stack[top - 1], obj-stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void SFunction(this IInterpreter interpreter, Func<string, string, string> func)
        {
            var stack = interpreter.State.Stack;
            var top = stack.Top;
            stack.Items[--stack.Top].StringValue = func(stack.Items[top - 1].StringValue, stack.Items[top].StringValue);
        }

        #endregion


        #region stack checks

        /// <summary>
        /// Expects N items on the object stack.
        /// Wont return (throws an InterpreterException), if not enough items are on the object stack.
        /// </summary>
        /// <param name="expectedItemsCount">The number of stack items expected on the object stack.</param>
        public static void ObjectStackExpect(this IInterpreter interpreter, int expectedItemsCount)
        {
            if (expectedItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedItemsCount));

            if (interpreter.State.Stack.Count < expectedItemsCount)
            {
                throw new InterpreterException(-4, "stack underflow");
            }

            // TODO: Stack values type check?
        }

        /// <summary>
        /// Expects N free items on the object stack, so N items can be pushed to the object stack.
        /// Wont return (throws an InterpreterException), if there is not enough free items on the object stack.
        /// </summary>
        /// <param name="expectedFreeItemsCount">The number of free object stack items expected.</param>
        public static void ObjectStackFree(this IInterpreter interpreter, int expectedFreeItemsCount)
        {
            if (expectedFreeItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedFreeItemsCount));

            if ((interpreter.State.Stack.Count + expectedFreeItemsCount) >= interpreter.State.Stack.Items.Length)
            {
                throw new InterpreterException(-3, "stack overflow");
            }
        }

        #endregion
    }
}
