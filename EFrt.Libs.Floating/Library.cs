/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Floating
{
    using System;
    using System.Globalization;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;


    /// <summary>
    /// The FLOAT words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "FLOATING";

        private IInterpreter _interpreter;


        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ">FLOAT", ToNumberAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F.", PrintFloatAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FCONSTANT", ConstantCompilationAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FVARIABLE", VariableCompilationAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F!", StoreToVariableAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F@", FetchFromVariableAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FDEPTH", DepthAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F>S", FloatToSingleCellIntAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S>F", SingleCellIntToFloatAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F>D", FloatToDoubleCellIntAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D>F", DoubleCellIntToFloatAction));

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
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FNEGATE", NegateAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FACOS", AcosAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FASIN", AsinAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FATAN", AtanAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FATAN2", Atan2Action));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FCOS", CosAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FEXP", ExpAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FLOG", LogAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FSIN", SinAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FSQRT", SqrtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FTAN", TanAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FLOOR", FloorAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F**", PowAction));

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


        // ( -- false | f true) {s -- }
        private int ToNumberAction()
        {
            var f = _interpreter.ParseFloatingPointNumber(_interpreter.OPop().ToString(), out var success);
            if (success)
            {
                _interpreter.FPush(f);
                _interpreter.Push(-1);
            }
            else
            {
                _interpreter.Push(0);
            }

            return 1;
        }

        // (f --)
        private int PrintFloatAction()
        {
            _interpreter.Output.Write("{0} ", new FloatingPointValue()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D.ToString(CultureInfo.InvariantCulture));

            return 1;
        }


        // FCONSTANT word-name
        // (f -- )
        private int ConstantCompilationAction()
        {
            var n2 = _interpreter.Pop();
            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new DoubleCellConstantWord(_interpreter, _interpreter.GetWordName(), _interpreter.Pop(), n2));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // FVARIABLE word-name
        // ( -- )
        private int VariableCompilationAction()
        {
            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.GetWordName(), _interpreter.State.Heap.Alloc(2)));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (f addr -- )
        private int StoreToVariableAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.State.Heap.Items[addr + 1] = _interpreter.Pop();  // n2
            _interpreter.State.Heap.Items[addr] = _interpreter.Pop();      // n1

            return 1;
        }

        // (addr -- f)
        private int FetchFromVariableAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.Push(_interpreter.State.Heap.Items[addr]);      // n1
            _interpreter.Push(_interpreter.State.Heap.Items[addr + 1]);  // n2

            return 1;
        }

        // ( -- n)
        private int DepthAction()
        {
            _interpreter.Push(_interpreter.State.Stack.Count / 2);

            return 1;
        }

        // (f1 f2 -- f3)
        private int AddAction()
        {
            _interpreter.FFunction((a, b) => a + b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int SubAction()
        {
            _interpreter.FFunction((a, b) => a - b);

            return 1;
        }

        // (f1 -- f2)
        private int AddOneAction()
        {
            _interpreter.FFunction((a) => a + 1.0);

            return 1;
        }

        // (f1 -- f2)
        private int SubOneAction()
        {
            _interpreter.FFunction((a) => a - 1.0);

            return 1;
        }

        // (f1 -- f2)
        private int AddTwoAction()
        {
            _interpreter.FFunction((a) => a + 2.0);

            return 1;
        }

        // (f1 -- f2)
        private int SubTwoAction()
        {
            _interpreter.FFunction((a) => a - 2.0);

            return 1;
        }

        // (f1 -- f2)
        private int MulTwoAction()
        {
            _interpreter.FFunction((a) => a * 2.0);

            return 1;
        }

        // (f1 -- f2)
        private int DivTwoAction()
        {
            _interpreter.FFunction((a) => a / 2.0);

            return 1;
        }

        // (f1 f2 -- f3)
        private int MulAction()
        {
            _interpreter.FFunction((a, b) => a * b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int DivAction()
        {
            _interpreter.FFunction((a, b) => a / b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int MaxAction()
        {
            _interpreter.FFunction((a, b) => (a > b) ? a : b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int MinAction()
        {
            _interpreter.FFunction((a, b) => (a < b) ? a : b);

            return 1;
        }

        // (f1 -- f2)
        private int AbsAction()
        {
            _interpreter.FFunction((a) => Math.Abs(a));

            return 1;
        }

        // (f1 -- f2)
        private int NegateAction()
        {
            _interpreter.FFunction((a) => -a);

            return 1;
        }

        // (f1 -- f2)
        private int AcosAction()
        {
            _interpreter.FFunction((a) => Math.Acos(a));

            return 1;
        }

        // (f1 -- f2)
        private int AsinAction()
        {
            _interpreter.FFunction((a) => Math.Asin(a));

            return 1;
        }

        // (f1 -- f2)
        private int AtanAction()
        {
            _interpreter.FFunction((a) => Math.Atan(a));

            return 1;
        }

        // (f1 f2 -- f3)
        private int Atan2Action()
        {
            _interpreter.FFunction((a, b) => Math.Atan2(a, b));

            return 1;
        }

        // (f1 -- f2)
        private int CosAction()
        {
            _interpreter.FFunction((a) => Math.Cos(a));

            return 1;
        }

        // (f1 -- f2)
        private int ExpAction()
        {
            _interpreter.FFunction((a) => Math.Exp(a));

            return 1;
        }

        // (f1 -- f2)
        private int LogAction()
        {
            _interpreter.FFunction((a) => Math.Log(a));

            return 1;
        }

        // (f1 -- f2)
        private int SinAction()
        {
            _interpreter.FFunction((a) => Math.Sin(a));

            return 1;
        }

        // (f1 -- f2)
        private int SqrtAction()
        {
            _interpreter.FFunction((a) => Math.Sqrt(a));

            return 1;
        }

        // (f1 -- f2)
        private int TanAction()
        {
            _interpreter.FFunction((a) => Math.Tan(a));

            return 1;
        }

        // (f1 -- f2)
        private int FloorAction()
        {
            _interpreter.FFunction((a) => Math.Floor(a));

            return 1;
        }

        // (f1 f2 -- f3)
        private int PowAction()
        {
            _interpreter.FFunction((a, b) => Math.Pow(a, b));

            return 1;
        }

        // (f -- n)
        private int FloatToSingleCellIntAction()
        {
            _interpreter.Push((int)_interpreter.FPop());

            return 1;
        }

        // (n -- f)
        private int SingleCellIntToFloatAction()
        {
            _interpreter.FPush(_interpreter.Pop());

            return 1;
        }

        // (f -- d)
        private int FloatToDoubleCellIntAction()
        {
            var v = new DoubleCellIntegerValue()
            {
                D = (long)_interpreter.FPop()
            };

            _interpreter.Push(v.A);
            _interpreter.Push(v.B);

            return 1;
        }

        // (d -- f)
        private int DoubleCellIntToFloatAction()
        {
            _interpreter.FPush(new DoubleCellIntegerValue()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop()
            }.D);

            return 1;
        }


        // (f1 f2 -- flag)
        private int IsEqAction()
        {
            _interpreter.Push((_interpreter.FPop() == _interpreter.FPop()) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsNeqAction()
        {
            _interpreter.Push((_interpreter.FPop() != _interpreter.FPop()) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsLtAction()
        {
            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() < b) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsLtEAction()
        {
            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() <= b) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsGtAction()
        {
            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() > b) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int IsGtEAction()
        {
            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() >= b) ? -1 : 0);

            return 1;
        }

        // (f -- flag)
        private int IsZeroAction()
        {
            _interpreter.Push((_interpreter.FPop() == 0.0) ? -1 : 0);

            return 1;
        }

        // (f -- flag)
        private int IsNonZeroAction()
        {
            _interpreter.Push((_interpreter.FPop() != 0.0) ? -1 : 0);

            return 1;
        }

        // (f -- flag)
        private int IsNegAction()
        {
            _interpreter.Push((_interpreter.FPop() < 0.0) ? -1 : 0);

            return 1;
        }

        // (f -- flag)
        private int IsPosAction()
        {
            _interpreter.Push((_interpreter.FPop() > 0.0) ? -1 : 0);

            return 1;
        }
    }
}
