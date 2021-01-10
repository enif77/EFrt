/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.FloatingExt
{
    using System;
    using System.Globalization;

    using EFrt.Core;
    using EFrt.Core.Values;


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
            _interpreter.AddPrimitiveWord("F**", FStarStarAction);
            _interpreter.AddPrimitiveWord("F.", FdotAction);
            _interpreter.AddPrimitiveWord("FABS", FAbsAction);
            _interpreter.AddPrimitiveWord("FACOS", FAcosAction);
            _interpreter.AddPrimitiveWord("FASIN", FAsinAction);
            _interpreter.AddPrimitiveWord("FATAN", FAtanAction);
            _interpreter.AddPrimitiveWord("FATAN2", FAtan2Action);
            _interpreter.AddPrimitiveWord("FCOS", FCosAction);
            _interpreter.AddPrimitiveWord("FEXP", FExpAction);
            _interpreter.AddPrimitiveWord("FLOG", FLogAction);
            _interpreter.AddPrimitiveWord("FSIN", FSinAction);
            _interpreter.AddPrimitiveWord("FSQRT", FSqrtAction);
            _interpreter.AddPrimitiveWord("FTAN", FTanAction);

            // Extra

            _interpreter.AddPrimitiveWord("F>S", FToSAction);
            _interpreter.AddPrimitiveWord("S>F", SToFAction);
            _interpreter.AddPrimitiveWord("F0<>", FZeroNotEqualsAction);
            _interpreter.AddPrimitiveWord("F0>", FZeroGreaterThanAction);
            _interpreter.AddPrimitiveWord("F1+", FOnePlusAction);
            _interpreter.AddPrimitiveWord("F1-", FOneMinusAction);
            _interpreter.AddPrimitiveWord("F2+", FTwoPlusAction);
            _interpreter.AddPrimitiveWord("F2-", FTwoMinusAction);
            _interpreter.AddPrimitiveWord("F2*", FTwoStarAction);
            _interpreter.AddPrimitiveWord("F2/", FTwoSlashAction);
            _interpreter.AddPrimitiveWord("F=", FEqualsAction);
            _interpreter.AddPrimitiveWord("F<>", FNotEqualsAction);
            _interpreter.AddPrimitiveWord("F<=", FLessThanOrEqualsAction);
            _interpreter.AddPrimitiveWord("F>", FGreaterThanAction);
            _interpreter.AddPrimitiveWord("F>=", FGreaterThanOrEqualsAction);
            
        }

        // (f1 f2 -- f3)
        private int FStarStarAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.FFunction((a, b) => Math.Pow(a, b));

            return 1;
        }

        // (f --)
        private int FdotAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Output.Write("{0} ", new FloatingPointValue()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.F.ToString(CultureInfo.InvariantCulture));

            return 1;
        }

        // (f1 -- f2)
        private int FAbsAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => Math.Abs(a));

            return 1;
        }

        // (f1 -- f2)
        private int FAcosAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => Math.Acos(a));

            return 1;
        }

        // (f1 -- f2)
        private int FAsinAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => Math.Asin(a));

            return 1;
        }

        // (f1 -- f2)
        private int FAtanAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => Math.Atan(a));

            return 1;
        }

        // (f1 f2 -- f3)
        private int FAtan2Action()
        {
            _interpreter.StackExpect(4);

            _interpreter.FFunction((a, b) => Math.Atan2(a, b));

            return 1;
        }

        // (f1 -- f2)
        private int FCosAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => Math.Cos(a));

            return 1;
        }

        // (f1 -- f2)
        private int FExpAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => Math.Exp(a));

            return 1;
        }

        // (f1 -- f2)
        private int FLogAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => Math.Log(a));

            return 1;
        }

        // (f1 -- f2)
        private int FSinAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => Math.Sin(a));

            return 1;
        }

        // (f1 -- f2)
        private int FSqrtAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => Math.Sqrt(a));

            return 1;
        }

        // (f1 -- f2)
        private int FTanAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => Math.Tan(a));

            return 1;
        }


        // Extra

        // (f -- n)
        private int FToSAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Push((int)_interpreter.FPop());

            return 1;
        }

        // (n -- f)
        private int SToFAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.StackFree(1);

            _interpreter.FPush(_interpreter.Pop());

            return 1;
        }

        // (f -- flag)
        private int FZeroNotEqualsAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Push((_interpreter.FPop() != 0.0) ? -1 : 0);

            return 1;
        }

        // (f -- flag)
        private int FZeroGreaterThanAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Push((_interpreter.FPop() > 0.0) ? -1 : 0);

            return 1;
        }

        // (f1 -- f2)
        private int FOnePlusAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => a + 1.0);

            return 1;
        }

        // (f1 -- f2)
        private int FOneMinusAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => a - 1.0);

            return 1;
        }

        // (f1 -- f2)
        private int FTwoPlusAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => a + 2.0);

            return 1;
        }

        // (f1 -- f2)
        private int FTwoMinusAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => a - 2.0);

            return 1;
        }

        // (f1 -- f2)
        private int FTwoStarAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => a * 2.0);

            return 1;
        }

        // (f1 -- f2)
        private int FTwoSlashAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FFunction((a) => a / 2.0);

            return 1;
        }
                
        // (f1 f2 -- flag)
        private int FEqualsAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.Push((_interpreter.FPop() == _interpreter.FPop()) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int FNotEqualsAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.Push((_interpreter.FPop() != _interpreter.FPop()) ? -1 : 0);

            return 1;
        }
        
        // (f1 f2 -- flag)
        private int FLessThanOrEqualsAction()
        {
            _interpreter.StackExpect(4);

            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() <= b) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int FGreaterThanAction()
        {
            _interpreter.StackExpect(4);

            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() > b) ? -1 : 0);

            return 1;
        }

        // (f1 f2 -- flag)
        private int FGreaterThanOrEqualsAction()
        {
            _interpreter.StackExpect(4);

            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() >= b) ? -1 : 0);

            return 1;
        }
    }
}
