/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.FloatingExt
{
    using System;
    using System.Globalization;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;


    /// <summary>
    /// The FLOATING-EXT words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "FLOATING-EXT";

        private IInterpreter _interpreter;


        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void DefineWords()
        {
            _interpreter.AddPrimitiveWord("DF!", DFStoreAction);          // Does the same as the word F!.
            _interpreter.AddPrimitiveWord("DF@", DFFetchAction);          // Does the same as the word F@.
            _interpreter.AddPrimitiveWord("DFALIGN", () => 1);            // Does nothing.
            _interpreter.AddPrimitiveWord("DFALIGNED", DFAlignedAction);  // Does the same as the word FALIGNED.
            _interpreter.AddPrimitiveWord("DFLOAT+", DFloatPlusAction);   // Does the same as the word FLOAT+.
            _interpreter.AddPrimitiveWord("DFLOATS", DFloatsAction);      // Does the same as the word FLOATS.
            _interpreter.AddPrimitiveWord("F**", FStarStarAction);
            _interpreter.AddPrimitiveWord("F.", FdotAction);
            _interpreter.AddPrimitiveWord("F>S", FToSAction);
            _interpreter.AddPrimitiveWord("FABS", FAbsAction);
            _interpreter.AddPrimitiveWord("FACOS", FAcosAction);
            _interpreter.AddPrimitiveWord("FACOSH", FAcoshAction);
            _interpreter.AddPrimitiveWord("FALOG", FAlogAction);
            _interpreter.AddPrimitiveWord("FASIN", FAsinAction);
            _interpreter.AddPrimitiveWord("FASINH", FAsinhAction);
            _interpreter.AddPrimitiveWord("FATAN", FAtanAction);
            _interpreter.AddPrimitiveWord("FATAN2", FAtan2Action);
            _interpreter.AddPrimitiveWord("FATANH", FAtanhAction);
            _interpreter.AddPrimitiveWord("FCOS", FCosAction);
            _interpreter.AddPrimitiveWord("FCOSH", FCoshAction);
            _interpreter.AddPrimitiveWord("FEXP", FExpAction);
            _interpreter.AddPrimitiveWord("FEXPM1", FExpM1Action);
            _interpreter.AddPrimitiveWord("FLN", FLnAction);
            _interpreter.AddPrimitiveWord("FLNP1", FLnP1Action);
            _interpreter.AddPrimitiveWord("FLOG", FLogAction);
            _interpreter.AddPrimitiveWord("FSIN", FSinAction);
            _interpreter.AddPrimitiveWord("FSINCOS", FSinCosAction);
            _interpreter.AddPrimitiveWord("FSINH", FSinhAction);
            _interpreter.AddPrimitiveWord("FSQRT", FSqrtAction);
            _interpreter.AddPrimitiveWord("FTAN", FTanAction);
            _interpreter.AddPrimitiveWord("FTANH", FTanhAction);
            _interpreter.AddPrimitiveWord("FTRUNC", FTruncAction);
            _interpreter.AddPrimitiveWord("FVALUE", FValueAction);
            _interpreter.AddPrimitiveWord("S>F", SToFAction);
            _interpreter.AddPrimitiveWord("SF!", SFStoreAction);
            _interpreter.AddPrimitiveWord("SF@", SFFetchAction);
            _interpreter.AddPrimitiveWord("SFALIGN", () => 1);            // Does nothing.
            _interpreter.AddPrimitiveWord("SFALIGNED", SFAlignedAction);  // Does the same as the word DFALIGNED.
            _interpreter.AddPrimitiveWord("SFLOAT+", SFloatPlusAction);   // Does the same as the word CELL+.
            _interpreter.AddPrimitiveWord("SFLOATS", SFloatsAction);      // Does the same as the word CELLS.

            // Extra

            _interpreter.AddPrimitiveWord("F=", FEqualsAction);
            _interpreter.AddPrimitiveWord("F<>", FNotEqualsAction);
            _interpreter.AddPrimitiveWord("F<=", FLessThanOrEqualsAction);
            _interpreter.AddPrimitiveWord("F>", FGreaterThanAction);
            _interpreter.AddPrimitiveWord("F>=", FGreaterThanOrEqualsAction);
            _interpreter.AddPrimitiveWord("F0<>", FZeroNotEqualsAction);
            _interpreter.AddPrimitiveWord("F0>", FZeroGreaterThanAction);
            _interpreter.AddPrimitiveWord("F1+", FOnePlusAction);
            _interpreter.AddPrimitiveWord("F1-", FOneMinusAction);
            _interpreter.AddPrimitiveWord("F2+", FTwoPlusAction);
            _interpreter.AddPrimitiveWord("F2-", FTwoMinusAction);
            _interpreter.AddPrimitiveWord("F2*", FTwoStarAction);
            _interpreter.AddPrimitiveWord("F2/", FTwoSlashAction);
        }

        // (addr -- ) (F: f -- )
        private int DFStoreAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.FStackExpect(1);

            var addr = _interpreter.Pop();
            var f = new FloatingPointValue() { F = _interpreter.FPop() };

            _interpreter.State.Heap.Items[addr] = f.A;
            _interpreter.State.Heap.Items[addr + 1] = f.B;

            return 1;
        }

        // (addr -- ) (F: -- f)
        private int DFFetchAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.FStackFree(1);

            var addr = _interpreter.Pop();

            _interpreter.FPush(new FloatingPointValue()
            {
                A = _interpreter.State.Heap.Items[addr],
                B = _interpreter.State.Heap.Items[addr + 1]
            }.F);

            return 1;
        }

        // (addr -- addr)
        private int DFAlignedAction()
        {
            _interpreter.StackExpect(1);

            // Does nothing. Just checks its parameters.

            return 1;
        }

        // (addr -- addr)
        private int DFloatPlusAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Push(_interpreter.Pop() + 2);  // Floating point value uses two heap cells.

            return 1;
        }

        // (n1 -- n2)
        private int DFloatsAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Push(_interpreter.Pop() * 2);  // Floating point value uses two heap cells.

            return 1;
        }

        // (F: f1 f2 -- f3)
        private int FStarStarAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => Math.Pow(a, b));

            return 1;
        }

        // (F: f --)
        private int FdotAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.Output.Write("{0} ", _interpreter.FPop().ToString(CultureInfo.InvariantCulture));

            return 1;
        }

        // ( -- n) (F: f -- )
        private int FToSAction()
        {
            _interpreter.FStackExpect(1);
            _interpreter.StackFree(1);

            _interpreter.Push((int)_interpreter.FPop());

            return 1;
        }

        // (F: f1 -- f2)
        private int FAbsAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Abs(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FAcosAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Acos(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FAcoshAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Acosh(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FAlogAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Pow(10.0, a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FAsinAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Asin(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FAsinhAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Asinh(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FAtanAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Atan(a));

            return 1;
        }

        // (F: f1 f2 -- f3)
        private int FAtan2Action()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => Math.Atan2(a, b));

            return 1;
        }

        // (F: f1 -- f2)
        private int FAtanhAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Atanh(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FCosAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Cos(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FCoshAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Cosh(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FExpAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Exp(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FExpM1Action()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Exp(a) - 1.0);

            return 1;
        }

        // (F: f1 -- f2)
        private int FLnAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Log(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FLnP1Action()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Log(a) + 1.0);

            return 1;
        }

        // (F: f1 -- f2)
        private int FLogAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Log10(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FSinAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Sin(a));

            return 1;
        }

        // (F: f1 -- f2 f3)
        private int FSinCosAction()
        {
            _interpreter.FStackExpect(1);
            _interpreter.FStackFree(1);

            var f = _interpreter.FPop();
            _interpreter.FPush(Math.Sin(f));
            _interpreter.FPush(Math.Cos(f));

            return 1;
        }

        // (F: f1 -- f2)
        private int FSinhAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Sinh(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FSqrtAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Sqrt(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FTanAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Tan(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FTanhAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Tanh(a));

            return 1;
        }

        // (F: f1 -- f2)
        private int FTruncAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Truncate(a));

            return 1;
        }

        // FVALUE word-name
        // (F: f -- )
        private int FValueAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new FloatingPointValueWord(_interpreter, _interpreter.ParseWord(), _interpreter.FPop()));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (n -- ) (F: -- f)
        private int SToFAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.FStackFree(1);

            _interpreter.FPush(_interpreter.Pop());

            return 1;
        }

        // (addr -- ) (F: f -- )
        private int SFStoreAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.FStackExpect(1);

            var addr = _interpreter.Pop();
            var f = new SingleCellFloatingPointValue() { F = (float)_interpreter.FPop() };

            _interpreter.State.Heap.Items[addr] = f.A;

            return 1;
        }

        // (addr -- ) (F: -- f)
        private int SFFetchAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.FStackFree(1);

            var addr = _interpreter.Pop();

            _interpreter.FPush(new SingleCellFloatingPointValue()
            {
                A = _interpreter.State.Heap.Items[addr]
            }.F);

            return 1;
        }

        // (addr -- addr)
        private int SFAlignedAction()
        {
            _interpreter.StackExpect(1);

            // Does nothing. Just checks its parameters.

            return 1;
        }

        // (addr -- addr)
        private int SFloatPlusAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Push(_interpreter.Pop() + 1);  // Single cell floating point value uses one heap cell.

            return 1;
        }

        // (n1 -- n2)
        private int SFloatsAction()
        {
            _interpreter.StackExpect(1);

            // Single cell floating point value uses one heap cell.

            return 1;
        }

        // Extra

        // ( -- flag) (F: f1 f2 -- )
        private int FEqualsAction()
        {
            _interpreter.FStackExpect(2);
            _interpreter.StackFree(1);

            _interpreter.Push((_interpreter.FPop() == _interpreter.FPop()) ? -1 : 0);

            return 1;
        }

        // ( -- flag) (F: f1 f2 -- )
        private int FNotEqualsAction()
        {
            _interpreter.FStackExpect(2);
            _interpreter.StackFree(1);

            _interpreter.Push((_interpreter.FPop() != _interpreter.FPop()) ? -1 : 0);

            return 1;
        }

        // ( -- flag) (F: f1 f2 -- )
        private int FLessThanOrEqualsAction()
        {
            _interpreter.FStackExpect(2);
            _interpreter.StackFree(1);

            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() <= b) ? -1 : 0);

            return 1;
        }

        // ( -- flag) (F: f1 f2 -- )
        private int FGreaterThanAction()
        {
            _interpreter.FStackExpect(2);
            _interpreter.StackFree(1);

            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() > b) ? -1 : 0);

            return 1;
        }

        // ( -- flag) (F: f1 f2 -- )
        private int FGreaterThanOrEqualsAction()
        {
            _interpreter.FStackExpect(2);
            _interpreter.StackFree(1);

            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() >= b) ? -1 : 0);

            return 1;
        }

        // ( -- flag) (F: f -- )
        private int FZeroNotEqualsAction()
        {
            _interpreter.FStackExpect(1);
            _interpreter.StackFree(1);

            _interpreter.Push((_interpreter.FPop() != 0.0) ? -1 : 0);

            return 1;
        }

        // ( -- flag) (F: f -- )
        private int FZeroGreaterThanAction()
        {
            _interpreter.FStackExpect(1);
            _interpreter.StackFree(1);

            _interpreter.Push((_interpreter.FPop() > 0.0) ? -1 : 0);

            return 1;
        }

        // (F: f1 -- f2)
        private int FOnePlusAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => a + 1.0);

            return 1;
        }

        // (F: f1 -- f2)
        private int FOneMinusAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => a - 1.0);

            return 1;
        }

        // (F: f1 -- f2)
        private int FTwoPlusAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => a + 2.0);

            return 1;
        }

        // (F: f1 -- f2)
        private int FTwoMinusAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => a - 2.0);

            return 1;
        }

        // (F: f1 -- f2)
        private int FTwoStarAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => a * 2.0);

            return 1;
        }

        // (F: f1 -- f2)
        private int FTwoSlashAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => a / 2.0);

            return 1;
        }
    }
}
