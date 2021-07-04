/* EFrt - (C) 2020 - 2021 Premysl Fara  */

using EFrt.Core.Extensions;

namespace EFrt.Libs.DoubleExt
{
    using EFrt.Core;


    /// <summary>
    /// The CORE words library.
    /// </summary>
    public class Library : IWordsLibrary
    {
        public string Name => "DOUBLE-EXT";

        private readonly IInterpreter _interpreter;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void Initialize()
        {
            _interpreter.AddPrimitiveWord("2ROT", TwoRotAction);

            _interpreter.AddPrimitiveWord(">DOUBLE", ToNumberAction);
            _interpreter.AddPrimitiveWord("D1+", DOnePlusAction);
            _interpreter.AddPrimitiveWord("D1-", DOneMinusAction);
            _interpreter.AddPrimitiveWord("D2+", AddTwoAction);
            _interpreter.AddPrimitiveWord("D2-", DTwoMinusAction);
            _interpreter.AddPrimitiveWord("D*", DStarAction);
            _interpreter.AddPrimitiveWord("D/", DSlashAction);
            _interpreter.AddPrimitiveWord("DMOD", DModAction);
            _interpreter.AddPrimitiveWord("D/MOD", DSlashModAction);
            _interpreter.AddPrimitiveWord("DNOT", DNotAction);
            _interpreter.AddPrimitiveWord("DAND", DAndAction);
            _interpreter.AddPrimitiveWord("DOR", DOrAction);
            _interpreter.AddPrimitiveWord("DXOR", DXorAction);
            _interpreter.AddPrimitiveWord("D<>", DNotNequalsAction);
            _interpreter.AddPrimitiveWord("D<=", DLessThanOrEqualAction);
            _interpreter.AddPrimitiveWord("D>", DGreaterThanAction);
            _interpreter.AddPrimitiveWord("D>=", DGreaterThanOrEqualAction);
            _interpreter.AddPrimitiveWord("D0<>", DZeroNotEqualsAction);
            _interpreter.AddPrimitiveWord("D0>", DZeroGreaterAction);
        }


        // (n1 n2 n3 n4 n5 n6 -- n3 n4 n5 n6 n1 n2)
        private int TwoRotAction()
        {
            _interpreter.StackExpect(6);

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

        // ( -- false | d true) {s -- }
        private int ToNumberAction()
        {
            _interpreter.ObjectStackExpect(1);

            var n = _interpreter.ParseIntegerNumber(_interpreter.OPop().ToString(), out var success);
            if (success)
            {
                _interpreter.StackFree(3);

                _interpreter.DPush(n);
                _interpreter.Push(-1);
            }
            else
            {
                _interpreter.StackFree(1);

                _interpreter.Push(0);
            }

            return 1;
        }

        // (d1 -- d2)
        private int DOnePlusAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => ++a);

            return 1;
        }

        // (d1 -- d2)
        private int DOneMinusAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => --a);

            return 1;
        }

        // (d1 -- d2)
        private int AddTwoAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => a + 2L);

            return 1;
        }

        // (d1 -- d2)
        private int DTwoMinusAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => a - 2L);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DStarAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => a * b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DSlashAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => a / b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DModAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => a % b);

            return 1;
        }

        // (d1 d2 -- d3 d4)
        private int DSlashModAction()
        {
            _interpreter.StackExpect(4);

            var b = _interpreter.DPop();
            var a = _interpreter.DPop();

            _interpreter.DPush(a / b);
            _interpreter.DPush(a % b);

            return 1;
        }

        // (d1 d2 -- flag)
        private int DNotNequalsAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => (a != b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int DLessThanOrEqualAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => (a <= b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int DGreaterThanAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => (a > b) ? -1 : 0);

            return 1;
        }

        // (d1 d2 -- flag)
        private int DGreaterThanOrEqualAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => (a >= b) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int DZeroNotEqualsAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => (a != 0) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int DZeroGreaterAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => (a > 0) ? -1 : 0);

            return 1;
        }

        // (d1 -- d2)
        private int DNotAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => ~a);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DAndAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => a & b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DOrAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => a | b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DXorAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => a ^ b);

            return 1;
        }
    }
}
