/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;

    using EFrt.Core.Values;


    /// <summary>
    /// Extensions method for the stack manipulations.
    /// </summary>
    public static class InterpreterStackExtensions
    {
        #region stacks

        /// <summary>
        /// Returns a value from the stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the stack.</returns>
        public static int Pick(this IInterpreter interpreter, int index)
        {
            return interpreter.State.Stack.Pick(index);
        }

        /// <summary>
        /// Returns a value from the top of the stack.
        /// </summary>
        /// <returns>A value from the top of the stack.</returns>
        public static int Peek(this IInterpreter interpreter)
        {
            return interpreter.State.Stack.Peek();
        }

        /// <summary>
        /// Removes a value from the top of the stack and returns it.
        /// </summary>
        /// <returns>A value from the top of the stack.</returns>
        public static int Pop(this IInterpreter interpreter)
        {
            return interpreter.State.Stack.Pop();
        }

        /// <summary>
        /// Inserts a value to the stack.
        /// </summary>
        /// <param name="value">A value.</param>
        public static void Push(this IInterpreter interpreter, int value)
        {
            interpreter.State.Stack.Push(value);
        }

        /// <summary>
        /// Drops N values from the stack.
        /// </summary>
        /// <param name="count">The number of values to be dropped from the stack.</param>
        public static void Drop(this IInterpreter interpreter, int count = 1)
        {
            interpreter.State.Stack.Drop(count);
        }

        /// <summary>
        /// Duplicates the top value on the stack.
        /// ( a -- a a ) 
        /// </summary>
        public static void Dup(this IInterpreter interpreter)
        {
            interpreter.State.Stack.Dup();
        }

        /// <summary>
        /// Swaps two values on the top of the stack.
        /// ( a b -- b a )
        /// </summary>
        public static void Swap(this IInterpreter interpreter)
        {
            interpreter.State.Stack.Swap();
        }

        /// <summary>
        /// Gets a value below the top of the stack and pushes it to the stack.
        /// (a b -- a b a)
        /// </summary>
        public static void Over(this IInterpreter interpreter)
        {
            interpreter.State.Stack.Over();
        }

        /// <summary>
        /// Rotates the top three stack values.
        /// (a b c -- b c a)
        /// </summary>
        public static void Rot(this IInterpreter interpreter)
        {
            interpreter.State.Stack.Rot();
        }

        /// <summary>
        /// Rotates indexth item to the top.
        /// </summary>
        /// <param name="index">A stack item index, where 0 = stack top, 1 = first below top, etc.</param>
        public static void Roll(this IInterpreter interpreter, int index)
        {
            interpreter.State.Stack.Roll(index);
        }


        /// <summary>
        /// Returns a double cell value from the stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A double cell value from the stack.</returns>
        public static long DPick(this IInterpreter interpreter, int index)
        {
            return new DoubleCellIntegerValue()
            {
                A = Pick(interpreter, index * 2),
                B = Pick(interpreter, index * 2 + 2),
            }.D;
        }

        /// <summary>
        /// Returns a double cell value from the top of the stack.
        /// </summary>
        /// <returns>A double cell value from the top of the stack.</returns>
        public static long DPeek(this IInterpreter interpreter)
        {
            return new DoubleCellIntegerValue()
            {
                B = Pick(interpreter, 1),
                A = Peek(interpreter),
            }.D;
        }

        /// <summary>
        /// Removes a double cell value from the top of the stack and returns it.
        /// </summary>
        /// <returns>A doble cell value from the top of the stack.</returns>
        public static long DPop(this IInterpreter interpreter)
        {
            return new DoubleCellIntegerValue()
            {
                B = Pop(interpreter),
                A = Pop(interpreter),
            }.D;
        }

        /// <summary>
        /// Inserts a double cell value to the stack.
        /// </summary>
        /// <param name="value">A double cell value.</param>
        public static void DPush(this IInterpreter interpreter, long value)
        {
            var v = new DoubleCellIntegerValue()
            {
                D = value
            };

            Push(interpreter, v.A);
            Push(interpreter, v.B);
        }


        /// <summary>
        /// Inserts a double cell value to the stack.
        /// </summary>
        /// <param name="value">A double cell value.</param>
        public static void UDPush(this IInterpreter interpreter, ulong value)
        {
            var v = new UnsignedDoubleCellIntegerValue()
            {
                UD = value
            };

            Push(interpreter, v.A);
            Push(interpreter, v.B);
        }

        #endregion


        #region stack functions

        /// <summary>
        /// stack[top] = func(stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void Function(this IInterpreter interpreter, Func<int, int> func)
        {
            var stack = interpreter.State.Stack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top]);
        }

        /// <summary>
        /// stack[top] = func(stack[top - 1], stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void Function(this IInterpreter interpreter, Func<int, int, int> func)
        {
            var stack = interpreter.State.Stack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);
        }


        /// <summary>
        /// stack[top] = func(stack[top - 1], stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void UFunction(this IInterpreter interpreter, Func<uint, uint, int> func)
        {
            var stack = interpreter.State.Stack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func((uint)stack.Items[top - 1], (uint)stack.Items[top]);
        }


        /// <summary>
        /// stack[top] = func(stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void DFunction(this IInterpreter interpreter, Func<long, int> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[stack.Top] = func(stack.Items[top]);

            Push(interpreter, func(DPop(interpreter)));
        }

        /// <summary>
        /// stack[top] = func(stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void DFunction(this IInterpreter interpreter, Func<long, long> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[stack.Top] = func(stack.Items[top]);

            DPush(interpreter, func(DPop(interpreter)));
        }

        /// <summary>
        /// stack[top] = func(stack[top - 1], stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void DFunction(this IInterpreter interpreter, Func<long, long, int> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);

            var b = DPop(interpreter);
            Push(interpreter, func(DPop(interpreter), b));
        }

        /// <summary>
        /// stack[top] = func(stack[top - 1], stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void DFunction(this IInterpreter interpreter, Func<long, long, long> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);

            var b = DPop(interpreter);
            DPush(interpreter, func(DPop(interpreter), b));
        }

        #endregion


        #region stack checks

        /// <summary>
        /// Expects N items on the stack.
        /// Wont return (throws an InterpreterException), if not enough items are on the stack.
        /// </summary>
        /// <param name="expectedItemsCount">The number of stack items expected on the stack.</param>
        public static void StackExpect(this IInterpreter interpreter, int expectedItemsCount)
        {
            if (expectedItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedItemsCount));

            if (interpreter.State.Stack.Count < expectedItemsCount)
            {
                throw new InterpreterException(-4, "stack underflow");
            }
        }

        /// <summary>
        /// Expects N free items on the stack, so N items can be pushed to the stack.
        /// Wont return (throws an InterpreterException), if there is not enough free items on the stack.
        /// </summary>
        /// <param name="expectedFreeItemsCount">The number of free stack items expected.</param>
        public static void StackFree(this IInterpreter interpreter, int expectedFreeItemsCount)
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
