/* EFrt - (C) 2020 Premysl Fara  */

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
            //_interpreter.AddWord(new ImmediateWord(_interpreter, "(FLIT)", LiteralAction));

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

        //// F:( -- f)
        //private int LiteralAction()
        //{
        //    if (_interpreter.IsCompiling == false)
        //    {
        //        throw new Exception("(FLIT) outside a new word definition.");
        //    }

        //    _interpreter.SkipWhite();

        //    var sign = 1;
        //    if (_interpreter.CurrentChar == '-')
        //    {
        //        sign = -1;
        //        _interpreter.NextChar();
        //    }
        //    else if (_interpreter.CurrentChar == '+')
        //    {
        //        _interpreter.NextChar();
        //    }

        //    var rValue = 0.0;
        //    while (_interpreter.IsDigit())
        //    {
        //        rValue = (rValue * 10.0) + (_interpreter.CurrentChar - '0');

        //        _interpreter.NextChar();
        //    }

        //    // digit-sequence '.' fractional-part
        //    if (_interpreter.CurrentChar == '.')
        //    {
        //        // Eat '.'.
        //        _interpreter.NextChar();

        //        if (_interpreter.IsDigit() == false)
        //        {
        //            throw new Exception("A fractional part of a real number expected.");
        //        }

        //        var scale = 1.0;
        //        var frac = 0.0;
        //        while (_interpreter.IsDigit())
        //        {
        //            frac = (frac * 10.0) + (_interpreter.CurrentChar - '0');
        //            scale *= 10.0;

        //            _interpreter.NextChar();
        //        }

        //        rValue += frac / scale;
        //    }

        //    // digit-sequence [ '.' fractional-part ] 'e' scale-factor
        //    if (_interpreter.CurrentChar == 'e' || _interpreter.CurrentChar == 'E')
        //    {
        //        // Eat 'e'.
        //        _interpreter.NextChar();

        //        if (_interpreter.IsDigit() == false)
        //        {
        //            throw new Exception("A scale factor of a real number expected.");
        //        }

        //        var fact = 0.0;
        //        while (_interpreter.IsDigit())
        //        {
        //            fact = (fact * 10.0) + (_interpreter.CurrentChar - '0');

        //            _interpreter.NextChar();
        //        }

        //        rValue *= Math.Pow(10, fact);
        //    }

        //    _interpreter.WordBeingDefined.AddWord(new FloatLiteralWord(_interpreter, _stack, rValue * sign));

        //    return 1;
        //}


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
            _interpreter.Push((int)FPop());

            return 1;
        }

        // F:( -- f) (n -- )
        private int FloatAction()
        {
            FPush(_interpreter.Pop());

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsEqAction()
        {
            _interpreter.Push((FPop() == FPop()) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsNeqAction()
        {
            _interpreter.Push((FPop() != FPop()) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsLtAction()
        {
            var b = FPop();
            _interpreter.Push((FPop() < b) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsLtEAction()
        {
            var b = FPop();
            _interpreter.Push((FPop() <= b) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsGtAction()
        {
            var b = FPop();
            _interpreter.Push((FPop() > b) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsGtEAction()
        {
            var b = FPop();
            _interpreter.Push((FPop() >= b) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsZeroAction()
        {
            _interpreter.Push((FPop() == 0.0) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsNonZeroAction()
        {
            _interpreter.Push((FPop() != 0.0) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsNegAction()
        {
            _interpreter.Push((FPop() < 0.0) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsPosAction()
        {
            _interpreter.Push((FPop() > 0.0) ? -1 : 0);

            return 1;
        }
    }
}
