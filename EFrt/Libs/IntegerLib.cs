/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using EFrt.Words;


    public class IntegerLib
    {
        private IInterpreter _interpreter;


        public IntegerLib(IInterpreter efrt)
        {
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "+", false, AddAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "-", false, SubAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "1+", false, AddOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "1-", false, SubOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2+", false, AddTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2-", false, SubTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2*", false, MulTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2/", false, DivTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "*", false, MulAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "/", false, DivAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "MOD", false, ModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "/MOD", false, DivModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "NOT", false, NotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "AND", false, AndAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OR", false, OrAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "XOR", false, XorAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "MAX", false, MaxAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "MIN", false, MinAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ABS", false, AbsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FLOAT", false, FloatAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "=", false, IsEqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "<>", false, IsNeqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "<", false, IsLtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ">", false, IsGtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0=", false, IsZeroAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0<>", false, IsNonZeroAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0<", false, IsNegAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0>", false, IsPosAction));
        }

        // (a b -- result)
        private int AddAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int + b.Int));

            return 1;
        }

        // (a b -- result)
        private int SubAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int - b.Int));

            return 1;
        }

        // (a -- result)
        private int AddOneAction()
        {
            _interpreter.Function((a) => new EfrtValue(++a.Int));

            return 1;
        }

        // (a -- result)
        private int SubOneAction()
        {
            _interpreter.Function((a) => new EfrtValue(--a.Int));

            return 1;
        }

        // (a -- result)
        private int AddTwoAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Int + 2));

            return 1;
        }

        // (a -- result)
        private int SubTwoAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Int - 2));

            return 1;
        }

        // (a -- result)
        private int MulTwoAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Int * 2));

            return 1;
        }

        // (a -- result)
        private int DivTwoAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Int / 2));

            return 1;
        }

        // (a b -- result)
        private int MulAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int * b.Int));

            return 1;
        }

        // (a b -- result)
        private int DivAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int / b.Int));

            return 1;
        }

        // (a b -- result)
        private int ModAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int % b.Int));

            return 1;
        }

        // (a b -- div mod)
        private int DivModAction()
        {
            var b = _interpreter.Popi();
            var a = _interpreter.Popi();

            _interpreter.Pushi(a / b);
            _interpreter.Pushi(a % b);

            return 1;
        }

        // (a b -- result)
        private int MaxAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int > b.Int) ? a.Int : b.Int));

            return 1;
        }

        // (a b -- result)
        private int MinAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int < b.Int) ? a.Int : b.Int));

            return 1;
        }

        // (a -- result)
        private int AbsAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Int < 0 ? -a.Int : a.Int));

            return 1;
        }

        // (a -- result)
        private int FloatAction()
        {
            _interpreter.Function((a) => new EfrtValue((float)a.Int));

            return 1;
        }

        // (a b -- result)
        private int IsEqAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int == b.Int) ? -1 : 0));

            return 1;
        }

        // (a b -- result)
        private int IsNeqAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int != b.Int) ? -1 : 0));

            return 1;
        }

        // (a b -- result)
        private int IsLtAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int < b.Int) ? -1 : 0));

            return 1;
        }

        // (a b -- result)
        private int IsGtAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int > b.Int) ? -1 : 0));

            return 1;
        }

        // (a -- result)
        private int IsZeroAction()
        {
            _interpreter.Function((a) => new EfrtValue((a.Int == 0) ? -1 : 0));

            return 1;
        }

        // (a -- result)
        private int IsNonZeroAction()
        {
            _interpreter.Function((a) => new EfrtValue((a.Int != 0) ? -1 : 0));

            return 1;
        }

        // (a -- result)
        private int IsNegAction()
        {
            _interpreter.Function((a) => new EfrtValue((a.Int < 0) ? -1 : 0));

            return 1;
        }

        // (a -- result)
        private int NotAction()
        {
            _interpreter.Function((a) => new EfrtValue(~a.Int));

            return 1;
        }

        // (a b -- result)
        private int AndAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int != 0 && b.Int != 0) ? -1 : 0));

            return 1;
        }

        // (a b -- result)
        private int OrAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int != 0 || b.Int != 0) ? -1 : 0));

            return 1;
        }

        // (a b -- result)
        private int XorAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int ^ b.Int));

            return 1;
        }

        // (a -- result)
        private int IsPosAction()
        {
            _interpreter.Function((a) => new EfrtValue((a.Int > 0) ? -1 : 0));

            return 1;
        }
    }
}
