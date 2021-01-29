/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Floating
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;


    /// <summary>
    /// The FLOATING words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "FLOATING";

        private IInterpreter _interpreter;


        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void DefineWords()
        {
            _interpreter.AddPrimitiveWord(">FLOAT", ToNumberAction);
            _interpreter.AddPrimitiveWord("D>F", DToFAction);
            _interpreter.AddPrimitiveWord("F!", FStoreAction);
            _interpreter.AddPrimitiveWord("F*", FStarAction);
            _interpreter.AddPrimitiveWord("F+", FPlusAction);
            _interpreter.AddPrimitiveWord("F-", FMinusAction);
            _interpreter.AddPrimitiveWord("F/", FSlashAction);
            _interpreter.AddPrimitiveWord("F0<", FZeroLessThanAction);
            _interpreter.AddPrimitiveWord("F0=", FZeroEqualsAction);
            _interpreter.AddPrimitiveWord("F<", FLessThanAction);
            _interpreter.AddPrimitiveWord("F>D", FToDAction);
            _interpreter.AddPrimitiveWord("F@", FFetchAction);
            _interpreter.AddPrimitiveWord("FCONSTANT", FConstantAction);
            _interpreter.AddPrimitiveWord("FDEPTH", FDepthAction);
            _interpreter.AddPrimitiveWord("FLOOR", FloorAction);
            _interpreter.AddPrimitiveWord("FMAX", FMaxAction);
            _interpreter.AddPrimitiveWord("FMIN", FMinAction);
            _interpreter.AddPrimitiveWord("FNEGATE", FNegateAction);
            _interpreter.AddPrimitiveWord("FVARIABLE", FVariableAction);
        }


        // ( -- false | f true) {s -- }
        private int ToNumberAction()
        {
            _interpreter.ObjectStackExpect(1);

            var f = _interpreter.ParseFloatingPointNumber(_interpreter.OPop().ToString(), out var success);
            if (success)
            {
                _interpreter.StackFree(1);
                _interpreter.FStackFree(1);

                _interpreter.FPush(f);
                _interpreter.Push(-1);
            }
            else
            {
                _interpreter.StackFree(1);

                _interpreter.Push(0);
            }

            return 1;
        }

        // (d -- f)
        private int DToFAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.FPush(_interpreter.DPop());

            return 1;
        }

        // (f addr -- )
        private int FStoreAction()
        {
            _interpreter.StackExpect(2);

            var addr = _interpreter.Pop();
            var f = new FloatingPointValue() { F = _interpreter.FPop() };
           
            _interpreter.State.Heap.Items[addr] = f.A;
            _interpreter.State.Heap.Items[addr + 1] = f.B;

            return 1;
        }

        // (f1 f2 -- f3)
        private int FStarAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => a * b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int FPlusAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => a + b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int FMinusAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => a - b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int FSlashAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => a / b);

            return 1;
        }

        // (f -- flag)
        private int FZeroLessThanAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Push((_interpreter.FPop() < 0.0) ? -1 : 0);

            return 1;
        }

        // (f -- flag)
        private int FZeroEqualsAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Push((_interpreter.FPop() == 0.0) ? -1 : 0);

            return 1;
        }
        
        // (f1 f2 -- flag)
        private int FLessThanAction()
        {
            _interpreter.FStackExpect(2);

            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() < b) ? -1 : 0);

            return 1;
        }

        // (f -- d)
        private int FToDAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.DPush((long)_interpreter.FPop());

            return 1;
        }

        // (addr -- f)
        private int FFetchAction()
        {
            _interpreter.StackExpect(1);

            var addr = _interpreter.Pop();

            _interpreter.FPush(new FloatingPointValue()
            {
                A = _interpreter.State.Heap.Items[addr],
                B = _interpreter.State.Heap.Items[addr + 1]
            }.F);

            return 1;
        }

        // FCONSTANT word-name
        // (f -- )
        private int FConstantAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new FloatingPointConstantWord(_interpreter, _interpreter.GetWordName(), _interpreter.FPop()));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // ( -- n)
        private int FDepthAction()
        {
            _interpreter.StackFree(1);

            _interpreter.Push(_interpreter.State.Stack.Count);

            return 1;
        }

        // (f1 -- f2)
        private int FloorAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Floor(a));

            return 1;
        }

        // (f1 f2 -- f3)
        private int FMaxAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => (a > b) ? a : b);

            return 1;
        }

        // (f1 f2 -- f3)
        private int FMinAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => (a < b) ? a : b);

            return 1;
        }

        // (f1 -- f2)
        private int FNegateAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => -a);

            return 1;
        }

        // FVARIABLE word-name
        // ( -- )
        private int FVariableAction()
        {
            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.GetWordName(), _interpreter.State.Heap.Alloc(2)));
            _interpreter.EndNewWordCompilation();

            return 1;
        }
    }
}
