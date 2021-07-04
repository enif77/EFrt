/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Double
{
    using System;
    using System.Globalization;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;


    public class Library : IWordsLibrary
    {
        public string Name => "DOUBLE";

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
            _interpreter.AddPrimitiveWord("2CONSTANT", TwoConstantAction);
            _interpreter.AddImmediateWord("2LITERAL", TwoLiteralAction);
            _interpreter.AddPrimitiveWord("2VARIABLE", TwoVariableAction);
            _interpreter.AddPrimitiveWord("D+", DPlusAction);
            _interpreter.AddPrimitiveWord("D-", DMinusAction);
            _interpreter.AddPrimitiveWord("D.", DDotAction);
            _interpreter.AddPrimitiveWord("D0<", DZeroLessAction);
            _interpreter.AddPrimitiveWord("D0=", DZeroEqualsAction);
            _interpreter.AddPrimitiveWord("D2*", DTwoStarAction);
            _interpreter.AddPrimitiveWord("D2/", DTwoSlashAction);
            _interpreter.AddPrimitiveWord("D<", DLessThanAction);
            _interpreter.AddPrimitiveWord("D=", DEqualsAction);
            _interpreter.AddPrimitiveWord("D>S", DToSAction);
            _interpreter.AddPrimitiveWord("DABS", DAbsAction);
            _interpreter.AddPrimitiveWord("DMAX", DMaxAction);
            _interpreter.AddPrimitiveWord("DMIN", DMinAction);
            _interpreter.AddPrimitiveWord("DNEGATE", DNegateAction);
        }


        // 2CONSTANT word-name
        // (n1 n2 -- )
        private int TwoConstantAction()
        {
            _interpreter.StackExpect(2);
                        
            _interpreter.BeginNewWordCompilation();
            var n2 = _interpreter.Pop();
            _interpreter.AddWord(new DoubleCellConstantWord(_interpreter, _interpreter.ParseWord(), _interpreter.Pop(), n2));
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

            _interpreter.StackExpect(2);

            _interpreter.WordBeingDefined.AddWord(new DoubleCellIntegerLiteralWord(_interpreter, _interpreter.DPop()));

            return 1;
        }

        // 2VARIABLE word-name
        // ( -- )
        private int TwoVariableAction()
        {
            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.ParseWord(), _interpreter.State.Heap.Alloc(2)));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (d1 d2 -- d3)
        private int DPlusAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => a + b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DMinusAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => a - b);

            return 1;
        }

        // (d --)
        private int DDotAction()
        {
            _interpreter.StackExpect(2);

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
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => (a < 0) ? -1 : 0);

            return 1;
        }

        // (d -- flag)
        private int DZeroEqualsAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => (a == 0) ? -1L : 0L);

            return 1;
        }

        // (d1 -- d2)
        private int DTwoStarAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => a * 2L);

            return 1;
        }

        // (d1 -- d2)
        private int DTwoSlashAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => a / 2L);

            return 1;
        }

        // (d1 d2 -- flag)
        private int DLessThanAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => (a < b) ? -1L : 0L);

            return 1;
        }

        // (d1 d2 -- flag)
        private int DEqualsAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => (a == b) ? -1L : 0L);

            return 1;
        }

        // (d -- n)
        private int DToSAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Push((int)_interpreter.DPop());

            return 1;
        }

        // (d1 -- d2)
        private int DAbsAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => a < 0L ? -a : a);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DMaxAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => (a > b) ? a : b);

            return 1;
        }

        // (d1 d2 -- d3)
        private int DMinAction()
        {
            _interpreter.StackExpect(4);

            _interpreter.DFunction((a, b) => (a < b) ? a : b);

            return 1;
        }

        // (d1 -- d2)
        private int DNegateAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DFunction((a) => -a);

            return 1;
        }
    }
}
