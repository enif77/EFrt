/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.CoreExt
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Words;

    using EFrt.Libs.CoreEx.Words;


    /// <summary>
    /// The CORE-EXT words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "CORE-EXT";

        private readonly IInterpreter _interpreter;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        /// <summary>
        /// Definas words from this library.
        /// </summary>
        public void DefineWords()
        {
            _interpreter.AddImmediateWord(".(", DotParenAction);
            _interpreter.AddPrimitiveWord("0<>", ZeroNotEqualsAction);
            _interpreter.AddPrimitiveWord("0>", ZeroGreaterAction);
            _interpreter.AddPrimitiveWord("2>R", TwoToRAction);
            _interpreter.AddPrimitiveWord("2R>", TwoRFromAction);
            _interpreter.AddPrimitiveWord("2R@", TwoRFetchAction);
            _interpreter.AddPrimitiveWord(":NONAME", NonameAction);
            _interpreter.AddImmediateWord(";", SemicolonAction);  // Extended version.
            _interpreter.AddPrimitiveWord("<>", NotEqualsAction);
            _interpreter.AddImmediateWord("?DO", QuestionDoAction);
            _interpreter.AddImmediateWord("AGAIN", AgainAction);
            _interpreter.AddPrimitiveWord("HEX", HexAction);
            _interpreter.AddPrimitiveWord("NIP", NipAction);
            _interpreter.AddPrimitiveWord("PICK", PickAction);
            _interpreter.AddPrimitiveWord("ROLL", RollAction);
            _interpreter.AddImmediateWord("S\\\"", SBackslashQuoteAction);  // S\" ..."
            _interpreter.AddPrimitiveWord("TO", ToAction);
            _interpreter.AddPrimitiveWord("TUCK", TuckAction);
            _interpreter.AddPrimitiveWord("VALUE", ValueAction);
            _interpreter.AddImmediateWord("\\", BackslashAction);

            _interpreter.AddConstantWord("FALSE", 0);
            _interpreter.AddConstantWord("TRUE", -1);

            _interpreter.AddPrimitiveWord("B!", BStoreAction);
            _interpreter.AddPrimitiveWord("B,", BCommaAction);
            _interpreter.AddPrimitiveWord("B@", BFetchAction);
            _interpreter.AddPrimitiveWord("BYTE+", BytePlusAction); 
            _interpreter.AddPrimitiveWord("BYTES", BytesAction);
            
            _interpreter.AddPrimitiveWord("-ROLL", MinusRollAction);
            _interpreter.AddPrimitiveWord("-ROT", MinusRotAction);
            _interpreter.AddPrimitiveWord("2+", TwoPlusAction);
            _interpreter.AddPrimitiveWord("2-", TwoMinusAction);
            _interpreter.AddPrimitiveWord("2NIP", TwoNipAction);
            _interpreter.AddPrimitiveWord("2TUCK", TwoTuckAction);
            _interpreter.AddPrimitiveWord("<=", LessOrEqualsAction);
            _interpreter.AddPrimitiveWord(">=", GreaterOrEqualsAction);
            _interpreter.AddPrimitiveWord("CLEAR", ClearAction);
        }


        // ( -- )
        private int DotParenAction()
        {
            _interpreter.Output.Write(_interpreter.ParseTerminatedString(')'));

            return 1;
        }

        // (n -- flag)
        private int ZeroNotEqualsAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((a) => (a != 0) ? -1 : 0);

            return 1;
        }

        // (n -- flag)
        private int ZeroGreaterAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((a) => (a > 0) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- ) [ -- n1 n2]
        private int TwoToRAction()
        {
            _interpreter.StackExpect(2);
            _interpreter.ReturnStackFree(2);

            var n2 = _interpreter.Pop();
            _interpreter.RPush(_interpreter.Pop());
            _interpreter.RPush(n2);

            return 1;
        }

        // ( -- n1 n2) [n1 n2 -- ]
        private int TwoRFromAction()
        {
            _interpreter.ReturnStackExpect(2);
            _interpreter.StackFree(2);

            var n2 = _interpreter.RPop();
            _interpreter.Push(_interpreter.RPop());
            _interpreter.Push(n2);

            return 1;
        }

        // ( -- n1 n2) [n1 n2 -- ]
        private int TwoRFetchAction()
        {
            _interpreter.StackFree(2);
            _interpreter.ReturnStackExpect(2);

            _interpreter.Push(_interpreter.RPick(1));  // n1
            _interpreter.Push(_interpreter.RPick(0));  // n2

            return 1;
        }

        // :NONAME body ;
        private int NonameAction()
        {
            _interpreter.BeginNewWordCompilation();
            _interpreter.WordBeingDefined = new NonameWord(_interpreter);

            return 1;
        }

        // : word-name body ;
        // :NONAME body ;
        private int SemicolonAction()
        {
            // Each user defined word exits with the EXIT word.
            _interpreter.WordBeingDefined.AddWord(new ExitControlWord(_interpreter, _interpreter.WordBeingDefined));

            _interpreter.EndNewWordCompilation();

            // Compilation of NONAME words leaves their execution token on the stack.
            if (_interpreter.WordBeingDefined is NonameWord)
            {
                _interpreter.StackFree(1);

                _interpreter.Push(_interpreter.WordBeingDefined.ExecutionToken);
            }

            return 1;
        }

        // (n1 n2 -- flag)
        private int NotEqualsAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((a, b) => (a != b) ? -1 : 0);

            return 1;
        }

        // (limit end -- )
        private int QuestionDoAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("?DO outside a new word definition.");
            }

            _interpreter.ReturnStackFree(1);

            _interpreter.RPush(
                _interpreter.WordBeingDefined.AddWord(
                    new IfDoControlWord(_interpreter, _interpreter.WordBeingDefined.NextWordIndex)));

            return 1;
        }

        // [ index-of-a-word-following-BEGIN -- ]
        private int AgainAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("AGAIN outside a new word definition.");
            }

            _interpreter.ReturnStackExpect(1);

            // AGAIN word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(
                new AgainControlWord(
                    _interpreter,
                    _interpreter.RPop() - _interpreter.WordBeingDefined.NextWordIndex));

            return 1;
        }

        // ( -- )
        private int HexAction()
        {
            _interpreter.State.SetBaseValue(16);

            return 1;
        }

        // (n1 n2 -- n2)
        private int NipAction()
        {
            _interpreter.StackExpect(2);

            var n2 = _interpreter.Pop();
            _interpreter.Drop();     // n1
            _interpreter.Push(n2);

            return 1;
        }

        // (index -- n)
        private int PickAction()
        {
            _interpreter.StackExpect(1);

            var index = _interpreter.Pop();

            _interpreter.StackExpect(index);

            _interpreter.Push(_interpreter.Pick(index));

            return 1;
        }

        // (index -- )
        private int RollAction()
        {
            _interpreter.StackExpect(1);

            var index = _interpreter.Pop();

            _interpreter.StackExpect(index);

            _interpreter.Roll(_interpreter.Pop());

            return 1;
        }

        // { -- s}
        private int SBackslashQuoteAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("S\\\" outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(new StringLiteralWord(_interpreter, _interpreter.ParseTerminatedString('"', true)));

            return 1;
        }

        // (n -- ) or (F: f -- )
        private int ToAction()
        {
            _interpreter.StackExpect(1);

            var valueWord = _interpreter.GetWord(_interpreter.ParseWord());
            if (valueWord is SingleCellValueWord)
            {
                ((SingleCellValueWord)valueWord).Value = _interpreter.Pop();

                return 1;
            }
            else if (valueWord is FloatingPointValueWord)
            {
                ((FloatingPointValueWord)valueWord).Value = _interpreter.FPop();

                return 1;
            }

            throw new Exception("A VALUE or FVALUE created word expected.");            
        }

        // (n1 n2 -- n2 n1 n2)
        private int TuckAction()
        {
            _interpreter.StackExpect(2);
            _interpreter.StackFree(1);

            var n2 = _interpreter.Peek();
            _interpreter.Swap();    // n2 n1
            _interpreter.Push(n2);  // n2 n1 n2

            return 1;
        }

        // VALUE word-name
        // (n -- )
        private int ValueAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new SingleCellValueWord(_interpreter, _interpreter.ParseWord(), _interpreter.Pop()));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // ( -- )
        private int BackslashAction()
        {
            _interpreter.NextChar();
            while (_interpreter.CurrentChar() != 0)
            {
                if (_interpreter.CurrentChar() == '\n')
                {
                    _interpreter.NextChar();

                    break;
                }

                _interpreter.NextChar();
            }

            return 1;
        }


        // Extra

        // (byte addr -- )
        private int BStoreAction()
        {
            _interpreter.StackExpect(2);
            
            var addr = _interpreter.Pop();
            
            _interpreter.CheckByteAlignedAddress(addr);
           
            _interpreter.State.Heap.Write(addr, (byte)_interpreter.Pop());

            return 1;
        }
        
        // (byte -- )
        private int BCommaAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.CheckByteAlignedHereAddress();            
            
            _interpreter.State.Heap.Write(_interpreter.State.Heap.Alloc(1), (byte)_interpreter.Pop());
            
            return 1;
        }
        
        // (addr -- byte)
        private int BFetchAction()
        {
            _interpreter.StackExpect(1);

            var addr = _interpreter.Pop();
            
            _interpreter.CheckByteAlignedAddress(addr);
            _interpreter.CheckAddressesRange(addr, 1);
            
            _interpreter.Push(_interpreter.State.Heap.ReadByte(addr));

            return 1;
        }
        
        // (addr1 -- addr2)
        private int BytePlusAction()
        {
            _interpreter.StackExpect(1);

            var addr = _interpreter.Pop();
            
            _interpreter.CheckByteAlignedAddress(addr);
            
            _interpreter.Push(addr + 1);

            return 1;
        }
        
        // (n1 -- n2)
        private int BytesAction()
        {
            _interpreter.StackExpect(1);

            // Nothing to do here, since the size of a byte is 0;

            return 1;
        }
        
        
        // (index -- n)
        private int MinusRollAction()
        {
            _interpreter.StackExpect(1);

            // TODO: Check for the index.

            var index = _interpreter.Pop();
            var items = _interpreter.State.Stack.Items;
            var top = _interpreter.State.Stack.Top;

            var item = items[top];
            for (var i = top - 1; i >= top - index; i--)
            {
                items[i + 1] = items[i];
            }

            items[top - index] = item;

            return 1;
        }

        // (n1 -- n2)
        private int TwoPlusAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((a) => a + 2);

            return 1;
        }

        // (n1 -- n2)
        private int TwoMinusAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((a) => a - 2);

            return 1;
        }

        // (n1 n2 n3 n4 -- n3 n4)
        private int TwoNipAction()
        {
            _interpreter.StackExpect(4);

            var n4 = _interpreter.Pop();
            var n3 = _interpreter.Pop();
            _interpreter.Drop(2);
            _interpreter.Push(n3);
            _interpreter.Push(n4);

            return 1;
        }

        // (n1 n2 n3 n4 -- n3 n4 n1 n2 n3 n4)
        private int TwoTuckAction()
        {
            _interpreter.StackExpect(4);
            _interpreter.StackFree(2);

            var n4 = _interpreter.Pop();
            var n3 = _interpreter.Pop();
            var n2 = _interpreter.Pop();
            var n1 = _interpreter.Pop();
            _interpreter.Push(n3);
            _interpreter.Push(n4);
            _interpreter.Push(n1);
            _interpreter.Push(n2);
            _interpreter.Push(n3);
            _interpreter.Push(n4);

            return 1;
        }

        // (n1 n2 -- flag)
        private int LessOrEqualsAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((a, b) => (a <= b) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int GreaterOrEqualsAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((a, b) => (a >= b) ? -1 : 0);

            return 1;
        }

        // (n1 n2 n3 -- n3 n1 n2)
        private int MinusRotAction()
        {
            _interpreter.StackExpect(3);

            var v3 = _interpreter.Pop();
            var v2 = _interpreter.Pop();
            var v1 = _interpreter.Pop();

            _interpreter.Push(v3);
            _interpreter.Push(v1);
            _interpreter.Push(v2);

            return 1;
        }

        // ( -- )
        private int ClearAction()
        {
            _interpreter.State.Stack.Clear();

            return 1;
        }
    }
}
