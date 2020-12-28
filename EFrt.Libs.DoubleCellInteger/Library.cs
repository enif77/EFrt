/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.DoubleCellInteger
{
    using System;
    using System.Globalization;

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
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2CONSTANT", TwoConstantAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2VARIABLE", TwoVariableAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D+", DPlusAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D-", DMinusAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D.", DDotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0<", DZeroLessAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0=", DZeroEqualsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2*", DTwoStarAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2/", DTwoSlashAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D<", DLessThanAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D=", DEqualsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D>S", DToSAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DABS", DAbsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DMAX", DMaxAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DMIN", DMinAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DNEGATE", DNegateAction));

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


        // 2CONSTANT word-name
        // (n1 n2 -- )
        private int TwoConstantAction()
        {
            var n2 = _interpreter.Pop();
            _interpreter.AddWord(new DoubleCellConstantWord(_interpreter, _interpreter.BeginNewWordCompilation(), _interpreter.Pop(), n2));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // 2VARIABLE word-name
        // ( -- )
        private int TwoVariableAction()
        {
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.BeginNewWordCompilation(), _interpreter.State.Heap.Alloc(2)));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (d1 d2 -- d3)
        private int DPlusAction()
        {
            Function((a, b) => a + b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DMinusAction()
        {
            Function((a, b) => a - b);

            return 1;
        }

        // (d --)
        private int DDotAction()
        {
            _interpreter.Output.Write("{0} ", new LongVal()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D.ToString(CultureInfo.InvariantCulture));

            return 1;
        }

        // (d -- flag)
        private int DZeroLessAction()
        {
            Function((a) => (a < 0) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int DZeroEqualsAction()
        {
            Function((a) => (a == 0) ? -1 : 0);

            return 1;
        }

        // (d1 -- d2)
        private int DTwoStarAction()
        {
            Function((a) => a * 2);

            return 1;
        }

        // (d1 -- d2)
        private int DTwoSlashAction()
        {
            Function((a) => a / 2);

            return 1;
        }

        // (d1 d2 -- flag)
        private int DLessThanAction()
        {
            Function((a, b) => (a < b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int DEqualsAction()
        {
            Function((a, b) => (a == b) ? -1 : 0);

            return 1;
        }

        // (d -- n)
        private int DToSAction()
        {
            _interpreter.Push((int)DPop());

            return 1;
        }

        // (d1 -- d2)
        private int DAbsAction()
        {
            Function((a) => a < 0 ? -a : a);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DMaxAction()
        {
            Function((a, b) => (a > b) ? a : b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DMinAction()
        {
            Function((a, b) => (a < b) ? a : b);

            return 1;
        }

        // (d1 -- d2)
        private int DNegateAction()
        {
            Function((a) => -a);

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
