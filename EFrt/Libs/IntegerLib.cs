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
        private void AddAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int + b.Int));
        }

        // (a b -- result)
        private void SubAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int - b.Int));
        }

        // (a -- result)
        private void AddOneAction()
        {
            _interpreter.Function((a) => new EfrtValue(++a.Int));
        }

        // (a -- result)
        private void SubOneAction()
        {
            _interpreter.Function((a) => new EfrtValue(--a.Int));
        }

        // (a -- result)
        private void AddTwoAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Int + 2));
        }

        // (a -- result)
        private void SubTwoAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Int - 2));
        }

        // (a -- result)
        private void MulTwoAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Int * 2));
        }

        // (a -- result)
        private void DivTwoAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Int / 2));
        }

        // (a b -- result)
        private void MulAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int * b.Int));
        }

        // (a b -- result)
        private void DivAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int / b.Int));
        }

        // (a b -- result)
        private void ModAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int % b.Int));
        }

        // (a b -- div mod)
        private void DivModAction()
        {
            var b = _interpreter.Popi();
            var a = _interpreter.Popi();

            _interpreter.Pushi(a / b);
            _interpreter.Pushi(a % b);
        }

        // (a b -- result)
        private void MaxAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int > b.Int) ? a.Int : b.Int));
        }

        // (a b -- result)
        private void MinAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int < b.Int) ? a.Int : b.Int));
        }

        // (a -- result)
        private void AbsAction()
        {
            _interpreter.Function((a) => new EfrtValue(a.Int < 0 ? -a.Int : a.Int));
        }

        // (a -- result)
        private void FloatAction()
        {
            _interpreter.Function((a) => new EfrtValue((float)a.Int));
        }

        // (a b -- result)
        private void IsEqAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int == b.Int) ? -1 : 0));
        }

        // (a b -- result)
        private void IsNeqAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int != b.Int) ? -1 : 0));
        }

        // (a b -- result)
        private void IsLtAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int < b.Int) ? -1 : 0));
        }

        // (a b -- result)
        private void IsGtAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int > b.Int) ? -1 : 0));
        }

        // (a -- result)
        private void IsZeroAction()
        {
            _interpreter.Function((a) => new EfrtValue((a.Int == 0) ? -1 : 0));
        }

        // (a -- result)
        private void IsNonZeroAction()
        {
            _interpreter.Function((a) => new EfrtValue((a.Int != 0) ? -1 : 0));
        }

        // (a -- result)
        private void IsNegAction()
        {
            _interpreter.Function((a) => new EfrtValue((a.Int < 0) ? -1 : 0));
        }

        // (a -- result)
        private void NotAction()
        {
            _interpreter.Function((a) => new EfrtValue(~a.Int));
        }

        // (a b -- result)
        private void AndAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int != 0 && b.Int != 0) ? -1 : 0));
        }

        // (a b -- result)
        private void OrAction()
        {
            _interpreter.Function((a, b) => new EfrtValue((a.Int != 0 || b.Int != 0) ? -1 : 0));
        }

        // (a b -- result)
        private void XorAction()
        {
            _interpreter.Function((a, b) => new EfrtValue(a.Int ^ b.Int));
        }

        // (a -- result)
        private void IsPosAction()
        {
            _interpreter.Function((a) => new EfrtValue((a.Int > 0) ? -1 : 0));
        }
    }
}
