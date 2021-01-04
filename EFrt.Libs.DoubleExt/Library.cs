/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.DoubleExt
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;


    /// <summary>
    /// The CORE words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "DOUBLE-EXT";

        private IInterpreter _interpreter;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter"></param>
        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        /// <summary>
        /// Definas words from this library.
        /// </summary>
        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2ROT", TwoRotAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D1+", AddOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D1-", SubOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2+", AddTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2-", SubTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D*", MulAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D/", DivAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DMOD", ModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D/MOD", DivModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DNOT", NotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DAND", AndAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DOR", OrAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DXOR", XorAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D<>", IsNeqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D<=", IsLtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D>", IsGtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D>=", IsGtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0<>", IsNonZeroAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0>", IsPosAction));
        }


        private long DPop()
        {
            return new DoubleCellIntegerValue()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D;
        }


        private void DPush(long value)
        {
            var v = new DoubleCellIntegerValue()
            {
                D = value
            };

            _interpreter.Push(v.A);
            _interpreter.Push(v.B);
        }


        private void Function(Func<long, int> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[stack.Top] = func(stack.Items[top]);

            _interpreter.Push(func(DPop()));
        }


        private void Function(Func<long, long> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[stack.Top] = func(stack.Items[top]);

            DPush(func(DPop()));
        }


        private void Function(Func<long, long, int> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);

            var b = DPop();
            _interpreter.Push(func(DPop(), b));
        }


        private void Function(Func<long, long, long> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);

            var b = DPop();
            DPush(func(DPop(), b));
        }


        // (n1 n2 n3 n4 n5 n6 -- n3 n4 n5 n6 n1 n2)
        private int TwoRotAction()
        {
            var n6 = _interpreter.Pop();
            var n5 = _interpreter.Pop();
            var n4 = _interpreter.Pop();
            var n3 = _interpreter.Pop();
            var n2 = _interpreter.Pop();
            var n1 = _interpreter.Pop();

            _interpreter.Push(n3);
            _interpreter.Push(n4);
            _interpreter.Push(n5);
            _interpreter.Push(n6);
            _interpreter.Push(n1);
            _interpreter.Push(n2);

            return 1;
        }


        // Extra

        // (d1 -- d2)
        private int AddOneAction()
        {
            Function((a) => ++a);

            return 1;
        }

        // (d1 -- d2)
        private int SubOneAction()
        {
            Function((a) => --a);

            return 1;
        }

        // (d1 -- d2)
        private int AddTwoAction()
        {
            Function((a) => a + 2L);

            return 1;
        }

        // (d1 -- d2)
        private int SubTwoAction()
        {
            Function((a) => a - 2L);

            return 1;
        }

        // (d1 d2 -- d3)
        private int MulAction()
        {
            Function((a, b) => a * b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DivAction()
        {
            Function((a, b) => a / b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int ModAction()
        {
            Function((a, b) => a % b);

            return 1;
        }

        // (d1 d2 -- d3 d4)
        private int DivModAction()
        {
            var b = DPop();
            var a = DPop();

            DPush(a / b);
            DPush(a % b);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsNeqAction()
        {
            Function((a, b) => (a != b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsLtEAction()
        {
            Function((a, b) => (a <= b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsGtAction()
        {
            Function((a, b) => (a > b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsGtEAction()
        {
            Function((a, b) => (a >= b) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int IsNonZeroAction()
        {
            Function((a) => (a != 0) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int IsPosAction()
        {
            Function((a) => (a > 0) ? -1 : 0);

            return 1;
        }

        // (d1 -- d2)
        private int NotAction()
        {
            Function((a) => ~a);

            return 1;
        }

        // (d1 d2 -- d3)
        private int AndAction()
        {
            Function((a, b) => a & b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int OrAction()
        {
            Function((a, b) => a | b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int XorAction()
        {
            Function((a, b) => a ^ b);

            return 1;
        }
    }
}
