/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Double
{
    using System;
    using System.Globalization;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;


    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "DOUBLE";

        private IInterpreter _interpreter;


        public Library(IInterpreter efrt)
        {
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2CONSTANT", TwoConstantAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "2LITERAL", TwoLiteralAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2VARIABLE", TwoVariableAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D+", DPlusAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D-", DMinusAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D.", DDotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0<", DZeroLessAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D0=", DZeroEqualsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2*", DTwoStarAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D2/", DTwoSlashAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D<", DLessThanAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D=", DEqualsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D>S", DToSAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DABS", DAbsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DMAX", DMaxAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DMIN", DMinAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DNEGATE", DNegateAction));

            
        }


        // 2CONSTANT word-name
        // (n1 n2 -- )
        private int TwoConstantAction()
        {
            var n2 = _interpreter.Pop();
            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new DoubleCellConstantWord(_interpreter, _interpreter.GetWordName(), _interpreter.Pop(), n2));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (n1 n2 -- )
        private int TwoLiteralAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("2LITERAL outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(new DoubleCellIntegerLiteralWord(_interpreter, _interpreter.DPop()));

            return 1;
        }

        // 2VARIABLE word-name
        // ( -- )
        private int TwoVariableAction()
        {
            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.GetWordName(), _interpreter.State.Heap.Alloc(2)));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (d1 d2 -- d3)
        private int DPlusAction()
        {
            _interpreter.DFunction((a, b) => a + b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DMinusAction()
        {
            _interpreter.DFunction((a, b) => a - b);

            return 1;
        }

        // (d --)
        private int DDotAction()
        {
            _interpreter.Output.Write("{0} ", new DoubleCellIntegerValue()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D.ToString(CultureInfo.InvariantCulture));

            return 1;
        }

        // (d -- flag)
        private int DZeroLessAction()
        {
            _interpreter.DFunction((a) => (a < 0) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int DZeroEqualsAction()
        {
            _interpreter.DFunction((a) => (a == 0) ? -1L : 0L);

            return 1;
        }

        // (d1 -- d2)
        private int DTwoStarAction()
        {
            _interpreter.DFunction((a) => a * 2L);

            return 1;
        }

        // (d1 -- d2)
        private int DTwoSlashAction()
        {
            _interpreter.DFunction((a) => a / 2L);

            return 1;
        }

        // (d1 d2 -- flag)
        private int DLessThanAction()
        {
            _interpreter.DFunction((a, b) => (a < b) ? -1L : 0L);

            return 1;
        }

        // (d1 d2 -- flag)
        private int DEqualsAction()
        {
            _interpreter.DFunction((a, b) => (a == b) ? -1L : 0L);

            return 1;
        }

        // (d -- n)
        private int DToSAction()
        {
            _interpreter.Push((int)_interpreter.DPop());

            return 1;
        }

        // (d1 -- d2)
        private int DAbsAction()
        {
            _interpreter.DFunction((a) => a < 0L ? -a : a);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DMaxAction()
        {
            _interpreter.DFunction((a, b) => (a > b) ? a : b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DMinAction()
        {
            _interpreter.DFunction((a, b) => (a < b) ? a : b);

            return 1;
        }

        // (d1 -- d2)
        private int DNegateAction()
        {
            _interpreter.DFunction((a) => -a);

            return 1;
        }
    }
}
