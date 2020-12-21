﻿/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.Float
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;


    /// <summary>
    /// The FLOAT words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        private IInterpreter _interpreter;


        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FCONSTANT", ConstantCompilationAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FVARIABLE", VariableCompilationAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F!", StoreToVariableAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F@", FetchFromVariableAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FDEPTH", DepthAction));

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


        // Floating point stack.

        private double FGet(int index)
        {
            return new DoubleVal()
            {
                A = _interpreter.Pick(index * 2),
                B = _interpreter.Pick(index * 2 + 2),
            }.D;
        }


        private double FPeek()
        {
            return new DoubleVal()
            {
                B = _interpreter.Pick(1),
                A = _interpreter.Peek(),
            }.D;
        }


        private double FPop()
        {
            return new DoubleVal()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D;
        }


        private void FPush(double value)
        {
            var v = new DoubleVal()
            {
                D = value
            };

            _interpreter.Push(v.A);
            _interpreter.Push(v.B);
        }


        private void Function(Func<double, double> func)
        {
            //var stack = _interpreter.FloatingPointStack;
            //var top = stack.Top;
            //stack.Items[stack.Top] = func(stack.Items[top]);

            FPush(func(FPop()));
        }


        private void Function(Func<double, double, double> func)
        {
            //var stack = _interpreter.FloatingPointStack;
            //var top = stack.Top;
            //stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);

            var b = FPop();
            FPush(func(FPop(), b));
        }


        // FCONSTANT word-name
        // (f -- )
        private int ConstantCompilationAction()
        {
            var n2 = _interpreter.Pop();
            _interpreter.AddWord(new DoubleCellConstantWord(_interpreter, _interpreter.BeginNewWordCompilation(), _interpreter.Pop(), n2));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // FVARIABLE word-name
        // ( -- )
        private int VariableCompilationAction()
        {
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.BeginNewWordCompilation(), _interpreter.Heap.Alloc(2)));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (f addr -- )
        private int StoreToVariableAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.Heap.Items[addr + 1] = _interpreter.Pop();  // n2
            _interpreter.Heap.Items[addr] = _interpreter.Pop();      // n1

            return 1;
        }

        // (addr -- f)
        private int FetchFromVariableAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.Push(_interpreter.Heap.Items[addr]);      // n1
            _interpreter.Push(_interpreter.Heap.Items[addr + 1]);  // n2

            return 1;
        }

        // ( -- n)
        private int DepthAction()
        {
            _interpreter.Push(_interpreter.Stack.Count / 2);

            return 1;
        }

        // (f1 f2 -- f3)
        private int AddAction()
        {
            Function((a, b) => a + b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int SubAction()
        {
            Function((a, b) => a - b);

            return 1;
        }

        // (f1 -- f2)
        private int AddOneAction()
        {
            Function((a) => a + 1.0);

            return 1;
        }

        // (f1 -- f2)
        private int SubOneAction()
        {
            Function((a) => a - 1.0);

            return 1;
        }

        // (f1 -- f2)
        private int AddTwoAction()
        {
            Function((a) => a + 2.0);

            return 1;
        }

        // (f1 -- f2)
        private int SubTwoAction()
        {
            Function((a) => a - 2.0);

            return 1;
        }

        // (f1 -- f2)
        private int MulTwoAction()
        {
            Function((a) => a * 2.0);

            return 1;
        }

        // (f1 -- f2)
        private int DivTwoAction()
        {
            Function((a) => a / 2.0);

            return 1;
        }

        // (f1 f2 -- f3)
        private int MulAction()
        {
            Function((a, b) => a * b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int DivAction()
        {
            Function((a, b) => a / b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int MaxAction()
        {
            Function((a, b) => (a > b) ? a : b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int MinAction()
        {
            Function((a, b) => (a < b) ? a : b);

            return 1;
        }

        // (f1 -- f2)
        private int AbsAction()
        {
            Function((a) => Math.Abs(a));

            return 1;
        }

        // (f -- n)
        private int FixAction()
        {
            _interpreter.Push((int)FPop());

            return 1;
        }

        // (n -- f)
        private int FloatAction()
        {
            FPush(_interpreter.Pop());

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsEqAction()
        {
            _interpreter.Push((FPop() == FPop()) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsNeqAction()
        {
            _interpreter.Push((FPop() != FPop()) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsLtAction()
        {
            var b = FPop();
            _interpreter.Push((FPop() < b) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsLtEAction()
        {
            var b = FPop();
            _interpreter.Push((FPop() <= b) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsGtAction()
        {
            var b = FPop();
            _interpreter.Push((FPop() > b) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsGtEAction()
        {
            var b = FPop();
            _interpreter.Push((FPop() >= b) ? -1 : 0);

            return 1;
        }

        // (f -- flag)
        private int IsZeroAction()
        {
            _interpreter.Push((FPop() == 0.0) ? -1 : 0);

            return 1;
        }

        // (f -- flag)
        private int IsNonZeroAction()
        {
            _interpreter.Push((FPop() != 0.0) ? -1 : 0);

            return 1;
        }

        // (f -- flag)
        private int IsNegAction()
        {
            _interpreter.Push((FPop() < 0.0) ? -1 : 0);

            return 1;
        }

        // (f -- flag)
        private int IsPosAction()
        {
            _interpreter.Push((FPop() > 0.0) ? -1 : 0);

            return 1;
        }
    }
}