/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Extensions
{
    using System;
    
    
    /// <summary>
    /// Extensions method for the floating point stack manipulations.
    /// </summary>
    public static class InterpreterFloatingPointStackExtensions
    {
        #region stack

        /// <summary>
        /// Returns a floating point value from the stack at certain index.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="index">A value index.</param>
        /// <returns>A floating point value from the stack.</returns>
        public static double FPick(this IInterpreter interpreter, int index)
        {
            return interpreter.State.FloatingPointStack.Pick(index);
        }

        /// <summary>
        /// Returns a floating point value from the top of the stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <returns>A floating point value from the top of the stack.</returns>
        public static double FPeek(this IInterpreter interpreter)
        {
            return interpreter.State.FloatingPointStack.Peek();
        }

        /// <summary>
        /// Removes a floating point value from the top of the stack and returns it.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <returns>A floating point value from the top of the stack.</returns>
        public static double FPop(this IInterpreter interpreter)
        {
            return interpreter.State.FloatingPointStack.Pop();
        }

        /// <summary>
        /// Inserts a floating point value to the stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="value">A floating point value.</param>
        public static void FPush(this IInterpreter interpreter, double value)
        {
            interpreter.State.FloatingPointStack.Push(value);
        }



        /// <summary>
        /// Drops N values from the stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="count">The number of values to be dropped from the stack.</param>
        public static void FDrop(this IInterpreter interpreter, int count = 1)
        {
            interpreter.State.FloatingPointStack.Drop(count);
        }

        /// <summary>
        /// Duplicates the top value on the stack.
        /// ( a -- a a ) 
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void FDup(this IInterpreter interpreter)
        {
            interpreter.State.FloatingPointStack.Dup();
        }

        /// <summary>
        /// Swaps two values on the top of the stack.
        /// ( a b -- b a )
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void FSwap(this IInterpreter interpreter)
        {
            interpreter.State.FloatingPointStack.Swap();
        }

        /// <summary>
        /// Gets a value below the top of the stack and pushes it to the stack.
        /// (a b -- a b a)
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void FOver(this IInterpreter interpreter)
        {
            interpreter.State.FloatingPointStack.Over();
        }

        /// <summary>
        /// Rotates the top three stack values.
        /// (a b c -- b c a)
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void FRot(this IInterpreter interpreter)
        {
            interpreter.State.FloatingPointStack.Rot();
        }

        /// <summary>
        /// Rotates indexth item to the top.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="index">A stack item index, where 0 = stack top, 1 = first below top, etc.</param>
        public static void FRoll(this IInterpreter interpreter, int index)
        {
            interpreter.State.FloatingPointStack.Roll(index);
        }

        #endregion


        #region stack functions

        /// <summary>
        /// stack[top] = func(stack[top])
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="func">A function.</param>
        public static void FFunction(this IInterpreter interpreter, Func<double, double> func)
        {
            var stack = interpreter.State.FloatingPointStack;
            var top = stack.Top;
            stack.Items[top] = func(stack.Items[top]);

            //FPush(interpreter, func(FPop(interpreter)));
        }

        /// <summary>
        /// stack[top] = func(stack[top - 1], stack[top])
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="func">A function.</param>
        public static void FFunction(this IInterpreter interpreter, Func<double, double, double> func)
        {
            var stack = interpreter.State.FloatingPointStack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);

            //var b = FPop(interpreter);
            //FPush(interpreter, func(FPop(interpreter), b));
        }

        #endregion


        #region stack checks

        /// <summary>
        /// Expects N items on the stack.
        /// Wont return (throws an InterpreterException), if not enough items are on the stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="expectedItemsCount">The number of stack items expected on the stack.</param>
        public static void FStackExpect(this IInterpreter interpreter, int expectedItemsCount)
        {
            if (expectedItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedItemsCount));

            if (interpreter.State.FloatingPointStack.Count < expectedItemsCount)
            {
                throw new InterpreterException(-4, "floating point stack underflow");
            }
        }

        /// <summary>
        /// Expects N free items on the stack, so N items can be pushed to the stack.
        /// Wont return (throws an InterpreterException), if there is not enough free items on the stack.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="expectedFreeItemsCount">The number of free stack items expected.</param>
        public static void FStackFree(this IInterpreter interpreter, int expectedFreeItemsCount)
        {
            if (expectedFreeItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedFreeItemsCount));

            if ((interpreter.State.FloatingPointStack.Count + expectedFreeItemsCount) >= interpreter.State.FloatingPointStack.Items.Length)
            {
                throw new InterpreterException(-3, "floating point stack overflow");
            }
        }

        #endregion
    }
}
