/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
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
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FLOAT", FloatAction));
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
        private int IsLtEAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int <= b.Int) ? -1 : 0));

            return 1;
        }

        // (a b -- result)
        private int IsGtAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int > b.Int) ? -1 : 0));

            return 1;
        }

        // (a b -- result)
        private int IsGtEAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int >= b.Int) ? -1 : 0));

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
