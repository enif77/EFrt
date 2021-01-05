/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.DoubleExt
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// The CORE words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "DOUBLE-EXT";

        private IInterpreter _interpreter;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter"></param>
        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        /// <summary>
        /// Definas words from this library.
        /// </summary>
        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2ROT", TwoRotAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D1+", AddOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D1-", SubOneAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2+", AddTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2-", SubTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D*", MulAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D/", DivAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DMOD", ModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D/MOD", DivModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DNOT", NotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DAND", AndAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DOR", OrAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DXOR", XorAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D<>", IsNeqAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D<=", IsLtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D>", IsGtAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D>=", IsGtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0<>", IsNonZeroAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0>", IsPosAction));
        }


        // (n1 n2 n3 n4 n5 n6 -- n3 n4 n5 n6 n1 n2)
        private int TwoRotAction()
        {
            var n6 = _interpreter.Pop();
            var n5 = _interpreter.Pop();
            var n4 = _interpreter.Pop();
            var n3 = _interpreter.Pop();
            var n2 = _interpreter.Pop();
            var n1 = _interpreter.Pop();

            _interpreter.Push(n3);
            _interpreter.Push(n4);
            _interpreter.Push(n5);
            _interpreter.Push(n6);
            _interpreter.Push(n1);
            _interpreter.Push(n2);

            return 1;
        }


        // Extra

        // (d1 -- d2)
        private int AddOneAction()
        {
           _interpreter.DFunction((a) => ++a);

            return 1;
        }

        // (d1 -- d2)
        private int SubOneAction()
        {
            _interpreter.DFunction((a) => --a);

            return 1;
        }

        // (d1 -- d2)
        private int AddTwoAction()
        {
            _interpreter.DFunction((a) => a + 2L);

            return 1;
        }

        // (d1 -- d2)
        private int SubTwoAction()
        {
            _interpreter.DFunction((a) => a - 2L);

            return 1;
        }

        // (d1 d2 -- d3)
        private int MulAction()
        {
            _interpreter.DFunction((a, b) => a * b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DivAction()
        {
            _interpreter.DFunction((a, b) => a / b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int ModAction()
        {
            _interpreter.DFunction((a, b) => a % b);

            return 1;
        }

        // (d1 d2 -- d3 d4)
        private int DivModAction()
        {
            var b = _interpreter.DPop();
            var a = _interpreter.DPop();

            _interpreter.DPush(a / b);
            _interpreter.DPush(a % b);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsNeqAction()
        {
            _interpreter.DFunction((a, b) => (a != b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsLtEAction()
        {
            _interpreter.DFunction((a, b) => (a <= b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsGtAction()
        {
            _interpreter.DFunction((a, b) => (a > b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int IsGtEAction()
        {
            _interpreter.DFunction((a, b) => (a >= b) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int IsNonZeroAction()
        {
            _interpreter.DFunction((a) => (a != 0) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int IsPosAction()
        {
            _interpreter.DFunction((a) => (a > 0) ? -1 : 0);

            return 1;
        }

        // (d1 -- d2)
        private int NotAction()
        {
            _interpreter.DFunction((a) => ~a);

            return 1;
        }

        // (d1 d2 -- d3)
        private int AndAction()
        {
            _interpreter.DFunction((a, b) => a & b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int OrAction()
        {
            _interpreter.DFunction((a, b) => a | b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int XorAction()
        {
            _interpreter.DFunction((a, b) => a ^ b);

            return 1;
        }
    }
}
