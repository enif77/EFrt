/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;

    using EFrt.Words;


    public class IntegerLib : IWordsLIbrary
    {
        private IInterpreter _interpreter;


        public IntegerLib(IInterpreter efrt)
        {
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "+", AddAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "-", SubAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "1+", AddOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "1-", SubOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2+", AddTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2-", SubTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2*", MulTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2/", DivTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "*", MulAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "/", DivAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "MOD", ModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "/MOD", DivModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "NOT", NotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "AND", AndAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OR", OrAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "XOR", XorAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "MAX", MaxAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "MIN", MinAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ABS", AbsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "=", IsEqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "<>", IsNeqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "<", IsLtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "<=", IsLtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ">", IsGtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ">=", IsGtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0=", IsZeroAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0<>", IsNonZeroAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0<", IsNegAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0>", IsPosAction));
        }


        private void Function(Func<int, int> func)
        {
            var stack = _interpreter.Stack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top]);
        }


        private void Function(Func<int, int, int> func)
        {
            var stack = _interpreter.Stack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);
        }


        // (n1 n2 -- n3)
        private int AddAction()
        {
            Function((a, b) => a + b);

            return 1;
        }

        // (n1 n2 -- n3)
        private int SubAction()
        {
            Function((a, b) => a - b);

            return 1;
        }

        // (n1 -- n2)
        private int AddOneAction()
        {
            Function((a) => ++a);

            return 1;
        }

        // (n1 -- n2)
        private int SubOneAction()
        {
            Function((a) => --a);

            return 1;
        }

        // (n1 -- n2)
        private int AddTwoAction()
        {
            Function((a) => a + 2);

            return 1;
        }

        // (n1 -- n2)
        private int SubTwoAction()
        {
            Function((a) => a - 2);

            return 1;
        }

        // (n1 -- n2)
        private int MulTwoAction()
        {
            Function((a) => a * 2);

            return 1;
        }

        // (n1 -- n2)
        private int DivTwoAction()
        {
            Function((a) => a / 2);

            return 1;
        }

        // (n1 n2 -- n3)
        private int MulAction()
        {
            Function((a, b) => a * b);

            return 1;
        }

        // (n1 n2 -- n3)
        private int DivAction()
        {
            Function((a, b) => a / b);

            return 1;
        }

        // (n1 n2 -- n3)
        private int ModAction()
        {
            Function((a, b) => a % b);

            return 1;
        }

        // (n1 n2b -- n3 n4)
        private int DivModAction()
        {
            var b = _interpreter.Pop();
            var a = _interpreter.Pop();

            _interpreter.Push(a / b);
            _interpreter.Push(a % b);

            return 1;
        }

        // (n1 n2 -- n3)
        private int MaxAction()
        {
            Function((a, b) => (a > b) ? a : b);

            return 1;
        }

        // (n1 n2 -- n3)
        private int MinAction()
        {
            Function((a, b) => (a < b) ? a : b);

            return 1;
        }

        // (n1 -- n2)
        private int AbsAction()
        {
            Function((a) => a < 0 ? -a : a);

            return 1;
        }

        // (n1 n2 -- flag)
        private int IsEqAction()
        {
            Function((a, b) => (a == b) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int IsNeqAction()
        {
            Function((a, b) => (a != b) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int IsLtAction()
        {
            Function((a, b) => (a < b) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int IsLtEAction()
        {
            Function((a, b) => (a <= b) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int IsGtAction()
        {
            Function((a, b) => (a > b) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int IsGtEAction()
        {
            Function((a, b) => (a >= b) ? -1 : 0);

            return 1;
        }

        // (n -- flag)
        private int IsZeroAction()
        {
            Function((a) => (a == 0) ? -1 : 0);

            return 1;
        }

        // (n -- flag)
        private int IsNonZeroAction()
        {
            Function((a) => (a != 0) ? -1 : 0);

            return 1;
        }

        // (n -- flag)
        private int IsNegAction()
        {
            Function((a) => (a < 0) ? -1 : 0);

            return 1;
        }

        // (n1 -- n2)
        private int NotAction()
        {
            Function((a) => ~a);

            return 1;
        }

        // (n1 n2 -- flag)
        private int AndAction()
        {
            Function((a, b) => (a != 0 && b != 0) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int OrAction()
        {
            Function((a, b) => (a != 0 || b != 0) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- n3)
        private int XorAction()
        {
            Function((a, b) => a ^ b);

            return 1;
        }

        // (n -- flag)
        private int IsPosAction()
        {
            Function((a) => (a > 0) ? -1 : 0);

            return 1;
        }
    }
}
