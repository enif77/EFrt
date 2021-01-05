/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;

    using EFrt.Core.Values;


    /// <summary>
    /// Extension methods for the IInterpreter interface.
    /// </summary>
    public static class InterpreterExtensions
    {
        #region stacks

        // Data stack

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
        /// Returns a floating point value from the stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A floating point value from the stack.</returns>
        public static double FPick(this IInterpreter interpreter, int index)
        {
            return new FloatingPointValue()
            {
                A = Pick(interpreter, index * 2),
                B = Pick(interpreter, index * 2 + 2),
            }.D;
        }

        /// <summary>
        /// Returns a floating point value from the top of the stack.
        /// </summary>
        /// <returns>A floating point value from the top of the stack.</returns>
        public static double FPeek(this IInterpreter interpreter)
        {
            return new FloatingPointValue()
            {
                B = Pick(interpreter, 1),
                A = Peek(interpreter),
            }.D;
        }

        /// <summary>
        /// Removes a floating point value from the top of the stack and returns it.
        /// </summary>
        /// <returns>A floating point value from the top of the stack.</returns>
        public static double FPop(this IInterpreter interpreter)
        {
            return new FloatingPointValue()
            {
                B = Pop(interpreter),
                A = Pop(interpreter),
            }.D;
        }

        /// <summary>
        /// Inserts a floating point value to the stack.
        /// </summary>
        /// <param name="value">A floating point value.</param>
        public static void FPush(this IInterpreter interpreter, double value)
        {
            var v = new FloatingPointValue()
            {
                D = value
            };

            Push(interpreter, v.A);
            Push(interpreter, v.B);
        }


        // Object stack.

        /// <summary>
        /// Returns a value from the object stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the object stack.</returns>
        public static object OPick(this IInterpreter interpreter, int index)
        {
            return interpreter.State.ObjectStack.Pick(index);
        }

        /// <summary>
        /// Returns a value from the top of the object stack.
        /// </summary>
        /// <returns>A value from the top of the object stack.</returns>
        public static object OPeek(this IInterpreter interpreter)
        {
            return interpreter.State.ObjectStack.Peek();
        }

        /// <summary>
        /// Removes a value from the top of the object stack and returns it.
        /// </summary>
        /// <returns>A value from the top of the object stack.</returns>
        public static object OPop(this IInterpreter interpreter)
        {
            return interpreter.State.ObjectStack.Pop();
        }

        /// <summary>
        /// Inserts a value to the object stack.
        /// </summary>
        /// <param name="value">A value.</param>
        public static void OPush(this IInterpreter interpreter, object value)
        {
            interpreter.State.ObjectStack.Push(value);
        }

        /// <summary>
        /// Drops N values from the top of the object stack.
        /// </summary>
        /// <param name="count">The number of values to be dropped from the top of the object stack.</param>
        public static void ODrop(this IInterpreter interpreter, int count = 1)
        {
            interpreter.State.ObjectStack.Drop();
        }

        /// <summary>
        /// Duplicates the top value on the object stack.
        /// </summary>
        public static void ODup(this IInterpreter interpreter)
        {
            interpreter.State.ObjectStack.Dup();
        }

        /// <summary>
        /// Swaps two values on the top of the object stack.
        /// ( a b -- b a )
        /// </summary>
        public static void OSwap(this IInterpreter interpreter)
        {
            interpreter.State.ObjectStack.Swap();
        }

        /// <summary>
        /// Gets a value below the top of the stack and pushes it to the object stack.
        /// (a b -- a b a)
        /// </summary>
        public static void OOver(this IInterpreter interpreter)
        {
            interpreter.State.ObjectStack.Over();
        }

        /// <summary>
        /// Rotates the top three object stack values.
        /// (a b c -- b c a)
        /// </summary>
        public static void ORot(this IInterpreter interpreter)
        {
            interpreter.State.ObjectStack.Rot();
        }

        /// <summary>
        /// Rotates indexth item on the object stack to the top of the object stack.
        /// </summary>
        /// <param name="index">A stack item index, where 0 = stack top, 1 = first below top, etc.</param>
        public static void ORoll(this IInterpreter interpreter, int index)
        {
            interpreter.State.ObjectStack.Roll(index);
        }


        // Return stack.

        /// <summary>
        /// Returns a value from the return stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the return stack.</returns>
        public static int RPick(this IInterpreter interpreter, int index)
        {
            return interpreter.State.ReturnStack.Pick(index);
        }

        /// <summary>
        /// Returns a value from the top of the return stack.
        /// </summary>
        /// <returns>A value from the top of the return stack.</returns>
        public static int RPeek(this IInterpreter interpreter)
        {
            return interpreter.State.ReturnStack.Peek();
        }

        /// <summary>
        /// Removes a value from the top of the return stack and returns it.
        /// </summary>
        /// <returns>A value from the top of the return stack.</returns>
        public static int RPop(this IInterpreter interpreter)
        {
            return interpreter.State.ReturnStack.Pop();
        }

        /// <summary>
        /// Inserts a value to the return stack.
        /// </summary>
        /// <param name="value">A value.</param>
        public static void RPush(this IInterpreter interpreter, int value)
        {
            interpreter.State.ReturnStack.Push(value);
        }

        /// <summary>
        /// Drops N values from the top of the return stack.
        /// </summary>
        /// <param name="count">The number of values to be dropped from the top of the return stack.</param>
        public static void RDrop(this IInterpreter interpreter, int count = 1)
        {
            interpreter.State.ReturnStack.Drop();
        }

        /// <summary>
        /// Duplicates the top value on the return stack.
        /// </summary>
        public static void RDup(this IInterpreter interpreter)
        {
            interpreter.State.ReturnStack.Dup();
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
            interpreter.Push(func(DPop(interpreter), b));
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


        /// <summary>
        /// stack[top] = func(stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void FFunction(this IInterpreter interpreter, Func<double, double> func)
        {
            //var stack = _interpreter.FloatingPointStack;
            //var top = stack.Top;
            //stack.Items[stack.Top] = func(stack.Items[top]);

            FPush(interpreter, func(FPop(interpreter)));
        }

        /// <summary>
        /// stack[top] = func(stack[top - 1], stack[top])
        /// </summary>
        /// <param name="func">A function.</param>
        public static void FFunction(this IInterpreter interpreter, Func<double, double, double> func)
        {
            //var stack = _interpreter.FloatingPointStack;
            //var top = stack.Top;
            //stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);

            var b = FPop(interpreter);
            FPush(interpreter, func(FPop(interpreter), b));
        }


        /// <summary>
        /// obj-stack[top] = func(obj-stack[top])
        /// </summary>
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
        /// <param name="func">A function.</param>
        public static void SFunction(this IInterpreter interpreter, Func<string, string, string> func)
        {
            var stack = interpreter.State.ObjectStack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1].ToString(), stack.Items[top].ToString());
        }

        #endregion
    }
}
