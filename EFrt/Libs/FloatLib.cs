/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;

    using EFrt.Words;


    public class FloatLib : IWordsLIbrary
    {
        private IInterpreter _interpreter;


        public FloatLib(IInterpreter efrt)
        {
            _interpreter = efrt;
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

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FDUP", DupAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F2DUP", DupTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F?DUP", DupPosAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FDROP", DropAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F2DROP", DropTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FSWAP", SwapAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F2SWAP", SwapTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FOVER", OverAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F2OVER", OverTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FROT", RotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F2ROT", RotTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F-ROT", RotBackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FDEPTH", DepthAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FCLEAR", ClearAction));
        }


        private void Function(Func<double, double> func)
        {
            var stack = _interpreter.FloatingPointStack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top]);
        }


        private void Function(Func<double, double, double> func)
        {
            var stack = _interpreter.FloatingPointStack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);
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
            _interpreter.Push((int)_interpreter.FPop());

            return 1;
        }

        // F:( -- f) (n -- )
        private int FloatAction()
        {
            _interpreter.FPush((double)_interpreter.Pop());

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsEqAction()
        {
            _interpreter.Push((_interpreter.FPop() == _interpreter.FPop()) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsNeqAction()
        {
            _interpreter.Push((_interpreter.FPop() != _interpreter.FPop()) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsLtAction()
        {
            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() < b) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsLtEAction()
        {
            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() <= b) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsGtAction()
        {
            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() > b) ? -1 : 0);

            return 1;
        }

        // F:(f1 f2 -- ) ( -- flag)
        private int IsGtEAction()
        {
            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() >= b) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsZeroAction()
        {
            _interpreter.Push((_interpreter.FPop() == 0.0) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsNonZeroAction()
        {
            _interpreter.Push((_interpreter.FPop() != 0.0) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsNegAction()
        {
            _interpreter.Push((_interpreter.FPop() < 0.0) ? -1 : 0);

            return 1;
        }

        // F:(f -- ) ( -- flag)
        private int IsPosAction()
        {
            _interpreter.Push((_interpreter.FPop() > 0.0) ? -1 : 0);

            return 1;
        }

        // F:(f -- f f)
        private int DupAction()
        {
            _interpreter.FDup();

            return 1;
        }

        // F:(f1 f2 -- f1 f2 f1 f2)
        private int DupTwoAction()
        {
            var b = _interpreter.FGet(1);
            var a = _interpreter.FGet(0);

            _interpreter.FPush(a);
            _interpreter.FPush(b);

            return 1;
        }

        // F:(f -- f f) or F:(f -- f)
        private int DupPosAction()
        {
            if (_interpreter.FPeek() != 0.0)
            {
                _interpreter.FDup();
            }

            return 1;
        }

        // F:(f --)
        private int DropAction()
        {
            _interpreter.FDrop();

            return 1;
        }

        // F:(f1 f2 --)
        private int DropTwoAction()
        {
            _interpreter.FDrop(2);

            return 1;
        }

        // F:(f1 f2 -- f2 f1)
        private int SwapAction()
        {
            _interpreter.FSwap();

            return 1;
        }

        // F:(f1 f2 f3 f4 -- f3 f4 f1 f2)
        private int SwapTwoAction()
        {
            var d = _interpreter.FPop();
            var c = _interpreter.FPop();
            var b = _interpreter.FPop();
            var a = _interpreter.FPop();

            _interpreter.FPush(c);
            _interpreter.FPush(d);
            _interpreter.FPush(a);
            _interpreter.FPush(b);

            return 1;
        }

        // F:(f1 f2 -- f1 f2 f1)
        private int OverAction()
        {
            _interpreter.FOver();

            return 1;
        }

        // F:(f1 f2 f3 f4 -- f1 f2 f3 f4 f1 f2)
        private int OverTwoAction()
        {
            var d = _interpreter.FPop();
            var c = _interpreter.FPop();
            var b = _interpreter.FPop();
            var a = _interpreter.FPeek();

            _interpreter.FPush(b);
            _interpreter.FPush(c);
            _interpreter.FPush(d);
            _interpreter.FPush(a);
            _interpreter.FPush(b);

            return 1;
        }

        // F:(f1 f2 f3 -- f2 f3 f1)
        private int RotAction()
        {
            _interpreter.FRot();

            return 1;
        }

        // F:(f1 f2 f3 f4 f5 f6 -- f3 f4 f5 f6 f1 f2)
        private int RotTwoAction()
        {
            var f = _interpreter.FPop();
            var e = _interpreter.FPop();
            var d = _interpreter.FPop();
            var c = _interpreter.FPop();
            var b = _interpreter.FPop();
            var a = _interpreter.FPop();

            _interpreter.FPush(c);
            _interpreter.FPush(d);
            _interpreter.FPush(e);
            _interpreter.FPush(f);
            _interpreter.FPush(a);
            _interpreter.FPush(b);

            return 1;
        }

        // F:(f1 f2 f3 -- f3 f1 f2)
        private int RotBackAction()
        {
            var v3 = _interpreter.FPop();
            var v2 = _interpreter.FPop();
            var v1 = _interpreter.FPop();

            _interpreter.FPush(v3);
            _interpreter.FPush(v1);
            _interpreter.FPush(v2);

            return 1;
        }

        // ( -- n)
        private int DepthAction()
        {
            _interpreter.Push(_interpreter.FloatingPointStack.Count);

            return 1;
        }

        // F:( -- )
        private int ClearAction()
        {
            _interpreter.FloatingPointStack.Clear();

            return 1;
        }

    }
}
