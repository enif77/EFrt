/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;
    using System.Globalization;

    using EFrt.Stacks;
    using EFrt.Words;


    public class FloatLib : IWordsLIbrary
    {
        private IInterpreter _interpreter;
        private IOutputWriter _outputWriter;
        private IStack<double> _stack;


        public FloatLib(IInterpreter efrt, IOutputWriter outputWriter)
        {
            _interpreter = efrt;
            _outputWriter = outputWriter;
            _stack = new FloatingStack();
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F.", WriteFloatAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F+", AddAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F-", SubAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F1+", AddOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F1-", SubOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F2+", AddTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F2-", SubTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F2*", MulTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F2/", DivTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F*", MulAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F/", DivAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FMAX", MaxAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FMIN", MinAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FABS", AbsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FIX", FixAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FLOAT", FloatAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F=", IsEqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F<>", IsNeqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F<", IsLtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F<=", IsLtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F>", IsGtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F>=", IsGtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F0=", IsZeroAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F0<>", IsNonZeroAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F0<", IsNegAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F0>", IsPosAction));
        }


        private void Function(Func<double, double> func)
        {
            var top = _stack.Top;
            _stack.Items[_stack.Top] = func(_stack.Items[top]);
        }


        private void Function(Func<double, double, double> func)
        {
            var top = _stack.Top;
            _stack.Items[--_stack.Top] = func(_stack.Items[top - 1], _stack.Items[top]);
        }


        // F:(f --)
        private int WriteFloatAction()
        {
            _outputWriter.Write("{0} ", _stack.Pop().ToString(CultureInfo.InvariantCulture));

            return 1;
        }

        // F:(f1 f2 -- f3)
        private int AddAction()
        {
            Function((a, b) => a + b);

            return 1;
        }

        // F:(f1 f2 -- f3)
        private int SubAction()
        {
            Function((a, b) => a - b);

            return 1;
        }

        // F:(f1 -- f2)
        private int AddOneAction()
        {
            Function((a) => a + 1.0);

            return 1;
        }

        // F:(f1 -- f2)
        private int SubOneAction()
        {
            Function((a) => a - 1.0);

            return 1;
        }

        // F:(f1 -- f2)
        private int AddTwoAction()
        {
            Function((a) => a + 2.0);

            return 1;
        }

        // F:(f1 -- f2)
        private int SubTwoAction()
        {
            Function((a) => a - 2.0);

            return 1;
        }

        // F:(f1 -- f2)
        private int MulTwoAction()
        {
            Function((a) => a * 2.0);

            return 1;
        }

        // F:(f1 -- f2)
        private int DivTwoAction()
        {
            Function((a) => a / 2.0);

            return 1;
        }

        // F:(f1 f2 -- f3)
        private int MulAction()
        {
            Function((a, b) => a * b);

            return 1;
        }

        // F:(f1 f2 -- f3)
        private int DivAction()
        {
            Function((a, b) => a / b);

            return 1;
        }

        // F:(f1 f2 -- f3)
        private int MaxAction()
        {
            Function((a, b) => (a > b) ? a : b);

            return 1;
        }

        // F:(f1 f2 -- f3)
        private int MinAction()
        {
            Function((a, b) => (a < b) ? a : b);

            return 1;
        }

        // F:(f1 -- f2)
        private int AbsAction()
        {
            Function((a) => Math.Abs(a));

            return 1;
        }

        // F:(f -- ) ( -- n)
        private int FixAction()
        {
            _interpreter.Push((int)_stack.Pop());

            return 1;
        }

        // F:( -- f) (n -- )
        private int FloatAction()
        {
            _stack.Push((double)_interpreter.Pop());

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsEqAction()
        {
            _interpreter.Push((_stack.Pop() == _stack.Pop()) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsNeqAction()
        {
            _interpreter.Push((_stack.Pop() != _stack.Pop()) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsLtAction()
        {
            var b = _stack.Pop();
            _interpreter.Push((_stack.Pop() < b) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsLtEAction()
        {
            var b = _stack.Pop();
            _interpreter.Push((_stack.Pop() <= b) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsGtAction()
        {
            var b = _stack.Pop();
            _interpreter.Push((_stack.Pop() > b) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsGtEAction()
        {
            var b = _stack.Pop();
            _interpreter.Push((_stack.Pop() >= b) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsZeroAction()
        {
            _interpreter.Push((_stack.Pop() == 0.0) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsNonZeroAction()
        {
            _interpreter.Push((_stack.Pop() != 0.0) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsNegAction()
        {
            _interpreter.Push((_stack.Pop() < 0.0) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsPosAction()
        {
            _interpreter.Push((_stack.Pop() > 0.0) ? -1 : 0);

            return 1;
        }
    }
}
