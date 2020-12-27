/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.DoubleCellInteger
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;


    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "DOUBLE";

        private IInterpreter _interpreter;


        public Library(IInterpreter efrt)
        {
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D+", AddAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D-", SubAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D1+", AddOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D1-", SubOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2+", AddTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2-", SubTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2*", MulTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2/", DivTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D*", MulAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D/", DivAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DMOD", ModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D/MOD", DivModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DNOT", NotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DAND", AndAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DOR", OrAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DXOR", XorAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DMAX", MaxAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DMIN", MinAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DABS", AbsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D=", IsEqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D<>", IsNeqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D<", IsLtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D<=", IsLtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D>", IsGtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D>=", IsGtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0=", IsZeroAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0<>", IsNonZeroAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0<", IsNegAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0>", IsPosAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DNEGATE", NegateAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D>S", LongToIntAction));
        }


        private long DGet(int index)
        {
            return new LongVal()
            {
                A = _interpreter.Pick(index * 2),
                B = _interpreter.Pick(index * 2 + 2),
            }.D;
        }


        private long DPeek()
        {
            return new LongVal()
            {
                B = _interpreter.Pick(1),
                A = _interpreter.Peek(),
            }.D;
        }


        private long DPop()
        {
            return new LongVal()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D;
        }


        private void DPush(long value)
        {
            var v = new LongVal()
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


        // (d -- n)
        private int LongToIntAction()
        {
            _interpreter.Push((int)DPop());

            return 1;
        }
        
        // (d1 d2 -- d3)
        private int AddAction()
        {
            Function((a, b) => a + b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int SubAction()
        {
            Function((a, b) => a - b);

            return 1;
        }

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

        // (d1 -- d2)
        private int MulTwoAction()
        {
            Function((a) => a * 2);

            return 1;
        }

        // (d1 -- d2)
        private int DivTwoAction()
        {
            Function((a) => a / 2);

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

        // (d1 d2 -- d3)
        private int MaxAction()
        {
            Function((a, b) => (a > b) ? a : b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int MinAction()
        {
            Function((a, b) => (a < b) ? a : b);

            return 1;
        }

        // (d1 -- d2)
        private int AbsAction()
        {
            Function((a) => a < 0 ? -a : a);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsEqAction()
        {
            Function((a, b) => (a == b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsNeqAction()
        {
            Function((a, b) => (a != b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsLtAction()
        {
            Function((a, b) => (a < b) ? -1 : 0);

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
        private int IsZeroAction()
        {
            Function((a) => (a == 0) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int IsNonZeroAction()
        {
            Function((a) => (a != 0) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int IsNegAction()
        {
            Function((a) => (a < 0) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int IsPosAction()
        {
            Function((a) => (a > 0) ? -1 : 0);

            return 1;
        }

        // (d1 -- d2)
        private int NegateAction()
        {
            Function((a) => -a);

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
