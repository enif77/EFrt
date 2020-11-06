/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
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
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F+", false, AddAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F-", false, SubAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "F1+", false, AddOneAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "F1-", false, SubOneAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "F2+", false, AddTwoAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "F2-", false, SubTwoAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "F2*", false, MulTwoAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "F2/", false, DivTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F*", false, MulAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F/", false, DivAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "MOD", false, ModAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "/MOD", false, DivModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FMAX", false, MaxAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FMIN", false, MinAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FABS", false, AbsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FIX", false, FixAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F=", false, IsEqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F<>", false, IsNeqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F<", false, IsLtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F>", false, IsGtAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "0=", false, IsZeroAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "0<>", false, IsNonZeroAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "0<", false, IsNegAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "0>", false, IsPosAction));
        }

        // (a b -- result)
        private int AddAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Float + b.Float));

            return 1;
        }

        // (a b -- result)
        private int SubAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Float - b.Float));

            return 1;
        }

        //// (a -- result)
        //private int AddOneAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue(++a.Int));

        //    return 1;
        //}

        //// (a -- result)
        //private int SubOneAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue(--a.Int));

        //    return 1;
        //}

        //// (a -- result)
        //private int AddTwoAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue(a.Int + 2));

        //    return 1;
        //}

        //// (a -- result)
        //private int SubTwoAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue(a.Int - 2));

        //    return 1;
        //}

        //// (a -- result)
        //private int MulTwoAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue(a.Int * 2));

        //    return 1;
        //}

        //// (a -- result)
        //private int DivTwoAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue(a.Int / 2));

        //    return 1;
        //}

        // (a b -- result)
        private int MulAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Float * b.Float));

            return 1;
        }

        // (a b -- result)
        private int DivAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Float / b.Float));

            return 1;
        }

        //// (a b -- result)
        //private int ModAction()
        //{
        //    _interpreter.Function((a, b) => new EfrtValue(a.Int % b.Int));

        //    return 1;
        //}

        //// (a b -- div mod)
        //private int DivModAction()
        //{
        //    var b = _interpreter.Popi();
        //    var a = _interpreter.Popi();

        //    _interpreter.Pushi(a / b);
        //    _interpreter.Pushi(a % b);

        //    return 1;
        //}

        // (a b -- result)
        private int MaxAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Float > b.Float) ? a.Float : b.Float));

            return 1;
        }

        // (a b -- result)
        private int MinAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Float < b.Float) ? a.Float : b.Float));

            return 1;
        }

        // (a -- result)
        private int AbsAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Float < 0 ? -a.Float : a.Float));

            return 1;
        }

        // (a -- result)
        private int FixAction()
        {
            _interpreter.Function((a) => new EfrtValue((int)a.Float));

            return 1;
        }

        // (a b -- result)
        private int IsEqAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Float == b.Float) ? -1 : 0));

            return 1;
        }

        // (a b -- result)
        private int IsNeqAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Float != b.Float) ? -1 : 0));

            return 1;
        }

        // (a b -- result)
        private int IsLtAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Float < b.Float) ? -1 : 0));

            return 1;
        }

        // (a b -- result)
        private int IsGtAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Float > b.Float) ? -1 : 0));

            return 1;
        }

        //// (a -- result)
        //private int IsZeroAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue((a.Int == 0) ? -1 : 0));

        //    return 1;
        //}

        //// (a -- result)
        //private int IsNonZeroAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue((a.Int != 0) ? -1 : 0));

        //    return 1;
        //}

        //// (a -- result)
        //private int IsNegAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue((a.Int < 0) ? -1 : 0));

        //    return 1;
        //}

        //// (a -- result)
        //private int NotAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue(~a.Int));

        //    return 1;
        //}

        //// (a b -- result)
        //private int AndAction()
        //{
        //    _interpreter.Function((a, b) => new EfrtValue((a.Int != 0 && b.Int != 0) ? -1 : 0));

        //    return 1;
        //}

        //// (a b -- result)
        //private int OrAction()
        //{
        //    _interpreter.Function((a, b) => new EfrtValue((a.Int != 0 || b.Int != 0) ? -1 : 0));

        //    return 1;
        //}

        //// (a b -- result)
        //private int XorAction()
        //{
        //    _interpreter.Function((a, b) => new EfrtValue(a.Int ^ b.Int));

        //    return 1;
        //}

        //// (a -- result)
        //private int IsPosAction()
        //{
        //    _interpreter.Function((a) => new EfrtValue((a.Int > 0) ? -1 : 0));

        //    return 1;
        //}
    }
}
