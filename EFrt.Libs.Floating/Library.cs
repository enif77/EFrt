/* EFrt - (C) 2020 - 2021 Premysl Fara  */

using EFrt.Core.Stacks;

namespace EFrt.Libs.Floating
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;


    /// <summary>
    /// The FLOATING words library.
    /// </summary>
    public class Library : IWordsLibrary
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
            _interpreter.AddPrimitiveWord("FALIGN", FAlignAction);
            _interpreter.AddPrimitiveWord("FALIGNED", FAlignedAction); 
            _interpreter.AddPrimitiveWord("FCONSTANT", FConstantAction);
            _interpreter.AddPrimitiveWord("FDEPTH", FDepthAction);
            _interpreter.AddPrimitiveWord("FDROP", FDropAction);
            _interpreter.AddPrimitiveWord("FDUP", FDupAction);
            _interpreter.AddPrimitiveWord("FDUP", FDupAction);
            _interpreter.AddImmediateWord("FLITERAL", FLiteralAction);
            _interpreter.AddPrimitiveWord("FLOAT+", FloatPlusAction);
            _interpreter.AddPrimitiveWord("FLOATS", FloatsAction);
            _interpreter.AddPrimitiveWord("FLOOR", FloorAction);
            _interpreter.AddPrimitiveWord("FMAX", FMaxAction);
            _interpreter.AddPrimitiveWord("FMIN", FMinAction);
            _interpreter.AddPrimitiveWord("FNEGATE", FNegateAction);
            _interpreter.AddPrimitiveWord("FOVER", FOverAction);
            _interpreter.AddPrimitiveWord("FROT", FRoteAction);
            _interpreter.AddPrimitiveWord("FROUND", FRoundAction);
            _interpreter.AddPrimitiveWord("FSWAP", FSwapAction);
            _interpreter.AddPrimitiveWord("FVARIABLE", FVariableAction);
        }


        // ( -- false | true) (F: | f -- ) {s -- }
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

        // (d -- ) (F: -- f)
        private int DToFAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.FPush(new DoubleCellIntegerValue()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop()
            }.D);

            return 1;
        }

        // (f-addr -- ) (F: f -- )
        private int FStoreAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.FStackExpect(1);

            var addr = _interpreter.Pop();
            
            _interpreter.CheckCellAlignedAddress(addr);
            _interpreter.CheckAddressesRange(addr, Heap.DoubleCellSize);
            
            _interpreter.State.Heap.Write(addr, _interpreter.FPop());

            return 1;
        }

        // (F: f1 f2 -- f3)
        private int FStarAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => a * b);

            return 1;
        }

        // (F: f1 f2 -- f3)
        private int FPlusAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => a + b);

            return 1;
        }

        // (F: f1 f2 -- f3)
        private int FMinusAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => a - b);

            return 1;
        }

        // (F: f1 f2 -- f3)
        private int FSlashAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => a / b);

            return 1;
        }

        // ( -- flag) (F: f -- )
        private int FZeroLessThanAction()
        {
            _interpreter.FStackExpect(1);
            _interpreter.StackFree(1);

            _interpreter.Push((_interpreter.FPop() < 0.0) ? -1 : 0);

            return 1;
        }

        // ( -- flag) (F: f -- )
        private int FZeroEqualsAction()
        {
            _interpreter.FStackExpect(1);
            _interpreter.StackFree(1);

            _interpreter.Push((_interpreter.FPop() == 0.0) ? -1 : 0);

            return 1;
        }
        
        // ( -- flag) (F: f1 f2 -- )
        private int FLessThanAction()
        {
            _interpreter.FStackExpect(2);
            _interpreter.StackFree(1);

            var b = _interpreter.FPop();
            _interpreter.Push((_interpreter.FPop() < b) ? -1 : 0);

            return 1;
        }

        // ( -- d) (F: f -- )
        private int FToDAction()
        {
            _interpreter.FStackExpect(1);
            _interpreter.StackFree(2);

            _interpreter.DPush((long)_interpreter.FPop());

            return 1;
        }

        // (f-addr -- ) (F: -- f)
        private int FFetchAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.FStackFree(1);

            var addr = _interpreter.Pop();

            _interpreter.CheckCellAlignedAddress(addr);
            _interpreter.CheckAddressesRange(addr, Heap.DoubleCellSize);
            
            _interpreter.FPush(_interpreter.State.Heap.ReadDouble(addr));

            return 1;
        }

        // ( -- )
        private int FAlignAction()
        {
            var here = _interpreter.State.Heap.Top + 1;
            var alignedHere = _interpreter.State.Heap.DoubleCellFloatingPointAligned(here);
            var alignBytes = alignedHere - here;
            
            if (alignBytes > 0)
            {
                _ =_interpreter.State.Heap.Alloc(alignBytes);
            }

            return 1;
        }
        
        // (addr -- f-addr)
        private int FAlignedAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Push(_interpreter.State.Heap.DoubleCellFloatingPointAligned(_interpreter.Pop()));

            return 1;
        }

        // FCONSTANT word-name
        // (F: f -- )
        private int FConstantAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new FloatingPointConstantWord(_interpreter, _interpreter.ParseWord(), _interpreter.FPop()));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // ( -- n)
        private int FDepthAction()
        {
            _interpreter.StackFree(1);

            _interpreter.Push(_interpreter.State.FloatingPointStack.Count);

            return 1;
        }

        // (F: f -- )
        private int FDropAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FDrop();

            return 1;
        }

        // (F: f -- f f)
        private int FDupAction()
        {
            _interpreter.FStackExpect(1);
            _interpreter.FStackFree(1);

            _interpreter.FDup();

            return 1;
        }

        // (F: f -- ), at runtime: (F: -- f)
        private int FLiteralAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("FLITERAL outside a new word definition.");
            }

            _interpreter.FStackExpect(1);

            _interpreter.WordBeingDefined.AddWord(new FloatingPointLiteralWord(_interpreter, _interpreter.FPop()));

            return 1;
        }

        // (addr -- addr)
        private int FloatPlusAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Push(_interpreter.Pop() + 2);  // Floating point value uses two heap cells.

            return 1;
        }

        // (n1 -- n2)
        private int FloatsAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Push(_interpreter.Pop() * Heap.DoubleCellSize);

            return 1;
        }

        // (F: f1 -- f2)
        private int FloorAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => Math.Floor(a));

            return 1;
        }

        // (F: f1 f2 -- f3)
        private int FMaxAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => (a > b) ? a : b);

            return 1;
        }

        // (F: f1 f2 -- f3)
        private int FMinAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FFunction((a, b) => (a < b) ? a : b);

            return 1;
        }

        // (F: f1 -- f2)
        private int FNegateAction()
        {
            _interpreter.FStackExpect(1);

            _interpreter.FFunction((a) => -a);

            return 1;
        }

        // (F: f1 f2 -- f1 f2 f1)
        private int FOverAction()
        {
            _interpreter.FStackExpect(2);
            _interpreter.FStackFree(1);

            _interpreter.FOver();

            return 1;
        }

        // (F: f1 f2 f3 -- f2 f3 f1)
        private int FRoteAction()
        {
            _interpreter.StackExpect(3);

            _interpreter.FRot();

            return 1;
        }

        // (F: f1 -- f2)
        private int FRoundAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.FPush(Math.Round(_interpreter.FPop()));

            return 1;
        }

        // (F: f1 f2 -- f2 f1)
        private int FSwapAction()
        {
            _interpreter.FStackExpect(2);

            _interpreter.FSwap();

            return 1;
        }

        // FVARIABLE word-name
        // ( -- ) (F: -- )
        private int FVariableAction()
        {
            _interpreter.BeginNewWordCompilation();
            
            // Align the data pointer.
            FAlignAction();
            
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.ParseWord(), _interpreter.State.Heap.Alloc(Heap.DoubleCellSize)));
            
            _interpreter.EndNewWordCompilation();

            return 1;
        }
    }
}
