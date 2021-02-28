/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Core
{
    using System;
    using System.Text;

    using EFrt.Core;
    using EFrt.Core.Stacks;
    using EFrt.Core.Words;

    using EFrt.Libs.Core.Words;


    /// <summary>
    /// The CORE words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "CORE";

        private readonly IInterpreter _interpreter;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter"></param>
        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        /// <summary>
        /// Defines words from this library.
        /// </summary>
        public void DefineWords()
        {
            _interpreter.AddPrimitiveWord("!", StoreAction);
            _interpreter.AddPrimitiveWord("'", TickAction);
            _interpreter.AddImmediateWord("(", ParenAction);
            _interpreter.AddPrimitiveWord("*", StarAction);
            _interpreter.AddPrimitiveWord("*/", StarSlashAction);
            _interpreter.AddPrimitiveWord("*/MOD", StarSlashModAction);
            _interpreter.AddPrimitiveWord("+", PlusAction);
            _interpreter.AddPrimitiveWord("+!", PlusStoreAction);
            _interpreter.AddImmediateWord("+LOOP", PlusLoopAction);
            _interpreter.AddPrimitiveWord(",", CommaAction);
            _interpreter.AddPrimitiveWord("-", MinusAction);
            _interpreter.AddPrimitiveWord(".", DotAction);
            _interpreter.AddImmediateWord(".\"", DotQuoteAction);
            _interpreter.AddPrimitiveWord("/", SlashAction);
            _interpreter.AddPrimitiveWord("/MOD", SlashModAction);
            _interpreter.AddPrimitiveWord("0<", ZeroLessAction);
            _interpreter.AddPrimitiveWord("0=", ZeroEqualsAction);
            _interpreter.AddPrimitiveWord("1+", OnePlusAction);
            _interpreter.AddPrimitiveWord("1-", OneMinusAction);
            _interpreter.AddPrimitiveWord("2!", TwoStoreAction);
            _interpreter.AddPrimitiveWord("2*", TwoStarAction);
            _interpreter.AddPrimitiveWord("2/", TwoSlashAction);
            _interpreter.AddPrimitiveWord("2@", TwoFetchAction);
            _interpreter.AddPrimitiveWord("2DROP", TwoDropAction);
            _interpreter.AddPrimitiveWord("2DUP", TwoDupAction);
            _interpreter.AddPrimitiveWord("2OVER", TwoOverAction);
            _interpreter.AddPrimitiveWord("2SWAP", TwoSwapAction);
            _interpreter.AddPrimitiveWord(":", ColonAction);
            _interpreter.AddImmediateWord(";", SemicolonAction);
            _interpreter.AddPrimitiveWord("<", LessThanAction);
            _interpreter.AddPrimitiveWord("=", EqualsAction);
            _interpreter.AddPrimitiveWord(">", GreaterThanAction);
            _interpreter.AddPrimitiveWord(">BODY", ToBodyAction);
            _interpreter.AddPrimitiveWord(">NUMBER", ToNumberAction);
            _interpreter.AddPrimitiveWord(">R", ToRAction);
            _interpreter.AddPrimitiveWord("?DUP", QuestionDupeAction);
            _interpreter.AddPrimitiveWord("@", FetchAction);
            _interpreter.AddPrimitiveWord("ABORT", AbortAction);
            _interpreter.AddImmediateWord("ABORT\"", AbortWithMessageAction);
            _interpreter.AddPrimitiveWord("ABS", AbsAction);
            _interpreter.AddPrimitiveWord("ALIGN", AlignAction);
            _interpreter.AddPrimitiveWord("ALIGNED", AlignedAction);
            _interpreter.AddPrimitiveWord("ALLOT", AllotAction);
            _interpreter.AddPrimitiveWord("AND", AndAction);
            _interpreter.AddPrimitiveWord("BASE", BaseAction);
            _interpreter.AddImmediateWord("BEGIN", BeginAction);
            _interpreter.AddConstantWord("BL", ' ');
            _interpreter.AddPrimitiveWord("CELL+", CellPlusAction); 
            _interpreter.AddPrimitiveWord("CELLS", CellsAction);
            _interpreter.AddPrimitiveWord("CHAR", CharAction);
            _interpreter.AddPrimitiveWord("CONSTANT", ConstantAction);
            _interpreter.AddPrimitiveWord("COUNT", CountAction);
            _interpreter.AddPrimitiveWord("CR", CrAction);
            _interpreter.AddPrimitiveWord("CREATE", CreateAction);
            _interpreter.AddPrimitiveWord("DECIMAL", DecimalAction);
            _interpreter.AddPrimitiveWord("DEPTH", DepthAction);
            _interpreter.AddImmediateWord("DO", DoAction);
            _interpreter.AddImmediateWord("DOES>", DoesAction);
            _interpreter.AddPrimitiveWord("DROP", DropAction);
            _interpreter.AddPrimitiveWord("DUP", DupAction);
            _interpreter.AddImmediateWord("ELSE", ElseAction);
            _interpreter.AddPrimitiveWord("EMIT", EmitAction);
            _interpreter.AddPrimitiveWord("EVALUATE", EvaluateAction);
            _interpreter.AddPrimitiveWord("EXECUTE", ExecuteAction);
            _interpreter.AddImmediateWord("EXIT", ExitAction);
            _interpreter.AddPrimitiveWord("FILL", FillAction);
            _interpreter.AddPrimitiveWord("FIND", FindAction);
            _interpreter.AddPrimitiveWord("FM/MOD", FMSlashModAction);
            _interpreter.AddPrimitiveWord("HERE", HereAction);
            _interpreter.AddImmediateWord("I", GetInnerIndexAction);
            _interpreter.AddImmediateWord("IF", IfAction);
            _interpreter.AddPrimitiveWord("IMMEDIATE", ImmediateAction);
            _interpreter.AddPrimitiveWord("INVERT", InvertAction);
            _interpreter.AddImmediateWord("J", GetOuterIndexAction);
            _interpreter.AddImmediateWord("LEAVE", LeaveAction);
            _interpreter.AddImmediateWord("LITERAL", LiteralAction);
            _interpreter.AddPrimitiveWord("LSHIFT", LShiftAction);
            _interpreter.AddPrimitiveWord("M*", MStarAction);
            _interpreter.AddImmediateWord("LOOP", LoopAction);
            _interpreter.AddPrimitiveWord("MAX", MaxAction);
            _interpreter.AddPrimitiveWord("MIN", MinAction);
            _interpreter.AddPrimitiveWord("MOD", ModAction);
            _interpreter.AddPrimitiveWord("MOVE", MoveAction);
            _interpreter.AddPrimitiveWord("NEGATE", NegateAction);
            _interpreter.AddPrimitiveWord("OR", OrAction);
            _interpreter.AddPrimitiveWord("OVER", OverAction);
            _interpreter.AddImmediateWord("POSTPONE", PostponeAction);
            _interpreter.AddPrimitiveWord("QUIT", QuitAction);
            _interpreter.AddPrimitiveWord("R>", RFromAction);
            _interpreter.AddPrimitiveWord("R@", RFetchAction);
            _interpreter.AddImmediateWord("RECURSE", RecurseAction);
            _interpreter.AddImmediateWord("REPEAT", RepeatAction);
            _interpreter.AddPrimitiveWord("ROT", RoteAction);
            _interpreter.AddPrimitiveWord("RSHIFT", RShiftAction);
            _interpreter.AddImmediateWord("S\"", SQuoteAction);
            _interpreter.AddPrimitiveWord("S>D", SToDAction);
            _interpreter.AddPrimitiveWord("SM/REM", SMSlashRemAction);
            _interpreter.AddPrimitiveWord("SPACE", SpaceAction);
            _interpreter.AddPrimitiveWord("SPACES", SpacesAction);
            _interpreter.AddPrimitiveWord("STATE", StateAction);
            _interpreter.AddPrimitiveWord("SWAP", SwapAction);
            _interpreter.AddImmediateWord("THEN", ThenAction);
            _interpreter.AddImmediateWord("TYPE", TypeAction);
            _interpreter.AddPrimitiveWord("U.", UDotAction);
            _interpreter.AddPrimitiveWord("U<", ULessThanAction);
            _interpreter.AddPrimitiveWord("UM*", UMStarAction);
            _interpreter.AddPrimitiveWord("UM/MOD", UMSlashModAction);
            _interpreter.AddImmediateWord("UNLOOP", UnloopAction);
            _interpreter.AddImmediateWord("UNTIL", UntilAction);
            _interpreter.AddPrimitiveWord("VARIABLE", VariableAction);
            _interpreter.AddImmediateWord("WHILE", WhileAction);
            _interpreter.AddPrimitiveWord("WORD", WordAction);
            _interpreter.AddPrimitiveWord("XOR", XorAction);
            _interpreter.AddImmediateWord("[", LeftBracketAction);
            _interpreter.AddImmediateWord("[']", BracketTickAction);
            _interpreter.AddImmediateWord("[CHAR]", BracketCharAction);
            _interpreter.AddImmediateWord("]", RightBracketAction);
        }

        // (n a-addr -- )
        private int StoreAction()
        {
            _interpreter.StackExpect(2);
            
            var addr = _interpreter.Pop();
            
            _interpreter.CheckCellAlignedAddress(addr);
           
            _interpreter.State.Heap.Write(addr, _interpreter.Pop());

            return 1;
        }

        // ' word-name
        // ( -- xt)
        private int TickAction()
        {
            _interpreter.StackFree(1);

            var wordName = _interpreter.ParseWord();
            if (_interpreter.State.WordsList.IsWordDefined(wordName))
            {
                _interpreter.Push(_interpreter.State.WordsList.GetWord(wordName).ExecutionToken);
            }
            else
            {
                _interpreter.Throw(-2, $"The '{wordName}' word is not defined.");
            }

            return 1;
        }

        // ( -- )
        private int ParenAction()
        {
            var c = _interpreter.NextChar();
            while (_interpreter.CurrentChar() != 0)
            {
                if (_interpreter.CurrentChar() == ')')
                {
                    _interpreter.NextChar();

                    break;
                }

                c = _interpreter.NextChar();
            }

            if (c != ')')
            {
                throw new Exception("')' expected.");
            }

            return 1;
        }

        // (n1 n2 -- n3)
        private int StarAction()
        {
            _interpreter.StackExpect(2);
            // StackFree() is not necessary here - we will pop 2 items, before we push one.

            _interpreter.Function((n1, n2) => n1 * n2);

            return 1;
        }

        // (n1 n2 n3 -- n4)
        private int StarSlashAction()
        {
            _interpreter.StackExpect(3);

            var n3 = (long)_interpreter.Pop();
            _interpreter.Push((int)((long)_interpreter.Pop() * (long)_interpreter.Pop() / n3));  // n2 * n1 / n3 

            return 1;
        }

        // (n1 n2 n3 -- n4 n5)
        private int StarSlashModAction()
        {
            _interpreter.StackExpect(3);

            var n3 = (long)_interpreter.Pop();
            var d = (long)_interpreter.Pop() * (long)_interpreter.Pop();
            _interpreter.Push((int)(d % n3));  // n4 = d % n3
            _interpreter.Push((int)(d / n3));  // n5 = d / n3

            return 1;
        }

        // (n1 n2 -- n3)
        private int PlusAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => n1 + n2);

            return 1;
        }

        // (n a-addr -- )
        private int PlusStoreAction()
        {
            _interpreter.StackExpect(2);

            var addr = _interpreter.Pop();
            
            _interpreter.CheckCellAlignedAddress(addr);
            
            _interpreter.State.Heap.Write(addr, _interpreter.State.Heap.ReadInt32(addr) + _interpreter.Pop());

            return 1;
        }

        // Compilation: [index of the word following DO/?DO -- ], runtime: (n -- )
        private int PlusLoopAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("+LOOP outside a new word definition.");
            }

            _interpreter.ReturnStackExpect(1);

            var cWordIndex = _interpreter.RPop();

            var loopIndex = _interpreter.WordBeingDefined.AddWord(
                new PlusLoopControlWord(
                    _interpreter,
                    (cWordIndex + 1) - _interpreter.WordBeingDefined.NextWordIndex));  // c + 1 -> index of the word folowing DO/?DO.

            var cWord = _interpreter.WordBeingDefined.GetWord(cWordIndex);
            if (cWord is IBranchingWord)
            {
                ((IBranchingWord)cWord).SetBranchTargetIndex(loopIndex);
            }

            return 1;
        }

        // (n -- )
        private int CommaAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.CheckCellAlignedHereAddress();            
            
            _interpreter.State.Heap.Write(_interpreter.State.Heap.AllocCells(1), _interpreter.Pop());
            
            return 1;
        }

        // (n1 n2 -- n3)
        private int MinusAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => n1 - n2);

            return 1;
        }

        // (n --)
        private int DotAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Output.Write("{0} ", _interpreter.Pop());

            return 1;
        }

        // ( -- )
        private int DotQuoteAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception(".\" outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(new PrintStringWord(_interpreter, _interpreter.ParseTerminatedString('"')));

            return 1;
        }

        // (n1 n2 -- n3)
        private int SlashAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => n1 / n2);

            return 1;
        }

        // (n1 n2 -- n3 n4)
        private int SlashModAction()
        {
            _interpreter.StackExpect(2);

            var n2 = _interpreter.Pop();
            var n1 = _interpreter.Pop();

            _interpreter.Push(n1 / n2);
            _interpreter.Push(n1 % n2);

            return 1;
        }

        // (n -- flag)
        private int ZeroLessAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((n) => (n < 0) ? -1 : 0);

            return 1;
        }

        // (n -- flag)
        private int ZeroEqualsAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((n) => (n == 0) ? -1 : 0);

            return 1;
        }

        // (n1 -- n2)
        private int OnePlusAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((n) => ++n);

            return 1;
        }

        // (n1 -- n2)
        private int OneMinusAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((n) => --n);

            return 1;
        }

        // (n1 n2 a-addr -- )
        private int TwoStoreAction()
        {
            _interpreter.StackExpect(3);

            var addr = _interpreter.Pop();
            
            _interpreter.CheckCellAlignedAddress(addr);
            _interpreter.CheckAddressesRange(addr, ByteHeap.DoubleCellSize);
           
            _interpreter.State.Heap.Write(addr + ByteHeap.CellSize, _interpreter.Pop());  // n2
            _interpreter.State.Heap.Write(addr, _interpreter.Pop());                          // n1

            return 1;
        }

        // (n1 -- n2)
        private int TwoStarAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((n) => n * 2);

            return 1;
        }

        // (n1 -- n2)
        private int TwoSlashAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((n) => n / 2);

            return 1;
        }

        // (a-addr -- n1 n2)
        private int TwoFetchAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.StackFree(1);  // We will remove one and add two, so we need just one extra to be free.

            var addr = _interpreter.Pop();
            
            _interpreter.CheckCellAlignedAddress(addr);
            _interpreter.CheckAddressesRange(addr, ByteHeap.DoubleCellSize);
            
            _interpreter.Push(_interpreter.State.Heap.ReadInt32(addr));                          // n1
            _interpreter.Push(_interpreter.State.Heap.ReadInt32(addr + ByteHeap.CellSize));  // n2

            return 1;
        }

        // (n1 n2 -- )
        private int TwoDropAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Drop(2);

            return 1;
        }

        // (n1 n2 -- n1 n2 n1 n2)
        private int TwoDupAction()
        {
            _interpreter.StackExpect(2);
            _interpreter.StackFree(2);  // Two new will be added.

            var n2 = _interpreter.Pop();
            var n1 = _interpreter.Peek();

            _interpreter.Push(n2);
            _interpreter.Push(n1);
            _interpreter.Push(n2);

            return 1;
        }

        // (n1 n2 n3 n4 -- n1 n2 n3 n4 n1 n2)
        private int TwoOverAction()
        {
            _interpreter.StackExpect(4);
            _interpreter.StackExpect(2);

            var n4 = _interpreter.Pop();
            var n3 = _interpreter.Pop();
            var n2 = _interpreter.Pop();
            var n1 = _interpreter.Peek();

            _interpreter.Push(n2);
            _interpreter.Push(n3);
            _interpreter.Push(n4);
            _interpreter.Push(n1);
            _interpreter.Push(n2);

            return 1;
        }

        // (n1 n2 n3 n4 -- n3 n4 n1 n2)
        private int TwoSwapAction()
        {
            _interpreter.StackExpect(4);

            var n4 = _interpreter.Pop();
            var n3 = _interpreter.Pop();
            var n2 = _interpreter.Pop();
            var n1 = _interpreter.Pop();

            _interpreter.Push(n3);
            _interpreter.Push(n4);
            _interpreter.Push(n1);
            _interpreter.Push(n2);

            return 1;
        }

        // : word-name body ;
        private int ColonAction()
        {
            _interpreter.BeginNewWordCompilation();
            _interpreter.WordBeingDefined = new NonPrimitiveWord(_interpreter, _interpreter.ParseWord());

            return 1;
        }

        // : word-name body ;
        private int SemicolonAction()
        {
            // Each user defined word exits with the EXIT word.
            _interpreter.WordBeingDefined.AddWord(new ExitControlWord(_interpreter, _interpreter.WordBeingDefined));

            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (n1 n2 -- flag)
        private int LessThanAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => (n1 < n2) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int EqualsAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => (n1 == n2) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int GreaterThanAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => (n1 > n2) ? -1 : 0);

            return 1;
        }

        // (xt -- addr)
        private int ToBodyAction()
        {
            _interpreter.StackExpect(1);

            var word = _interpreter.State.WordsList.GetWord(_interpreter.Pop());
            if (word is CreatedWord == false)
            {
                _interpreter.Throw(-31, ">BODY used on non-CREATEd definition.");
            }
            else
            {
                _interpreter.Push(((CreatedWord)word).DataFieldIndex);
            }

            return 1;
        }

        // ( -- false | n true) {s -- }
        private int ToNumberAction()
        {
            _interpreter.StackFree(1);

            var n = _interpreter.ParseIntegerNumber(_interpreter.OPop().ToString(), out var success);
            if (success)
            {
                _interpreter.StackFree(2);

                _interpreter.Push((int)n);
                _interpreter.Push(-1);
            }
            else
            {
                _interpreter.Push(0);
            }

            return 1;
        }

        // (a -- ) [ - a ]
        private int ToRAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.ReturnStackFree(1);

            _interpreter.RPush(_interpreter.Pop());

            return 1;
        }

        // (n -- n n) or ( -- )
        private int QuestionDupeAction()
        {
            _interpreter.StackExpect(1);

            if (_interpreter.Peek() != 0)
            {
                _interpreter.StackFree(1);

                _interpreter.Dup();
            }

            return 1;
        }

        // (a-addr -- n)
        private int FetchAction()
        {
            _interpreter.StackExpect(1);

            var addr = _interpreter.Pop();
            
            _interpreter.CheckCellAlignedAddress(addr);
            _interpreter.CheckAddressesRange(addr, ByteHeap.CellSize);
            
            _interpreter.Push(_interpreter.State.Heap.ReadInt32(addr));

            return 1;
        }

        // ( -- )
        private int AbortAction()
        {
            _interpreter.Abort();

            return 1;
        }

        // ( -- )
        private int AbortWithMessageAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("ABORT\" outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(new AbortWithMessageWord(_interpreter, _interpreter.ParseTerminatedString('"')));

            return 1;
        }

        // (n1 -- n2)
        private int AbsAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((n1) => n1 < 0 ? -n1 : n1);

            return 1;
        }
        
        // ( -- )
        private int AlignAction()
        {
            var here = _interpreter.State.Heap.Top + 1;
            var alignedHere = _interpreter.State.Heap.CellAligned(here);
            var alignBytes = alignedHere - here;
            
            if (alignBytes > 0)
            {
                _ =_interpreter.State.Heap.Alloc(alignBytes);
            }

            return 1;
        }

        // (addr -- a-addr)
        private int AlignedAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Push(_interpreter.State.Heap.CellAligned(_interpreter.Pop()));

            return 1;
        }

        // (n -- )
        private int AllotAction()
        {
            _interpreter.StackExpect(1);

            // We do not return the "address" returned by the Alloc() method.
            _ = _interpreter.State.Heap.Alloc(_interpreter.Pop());

            return 1;
        }

        // (n1 n2 -- n3)
        private int AndAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => n1 & n2);

            return 1;
        }

        // ( -- addr)
        private int BaseAction()
        {
            _interpreter.StackFree(1);

            _interpreter.Push(_interpreter.State.BaseVariableAddress);

            return 1;
        }

        // [ -- index-of-a-word-following-BEGIN ]
        private int BeginAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("BEGIN outside a new word definition.");
            }

            _interpreter.ReturnStackFree(1);

            // BEGIN word doesn't have a runtime behavior.

            _interpreter.RPush(_interpreter.WordBeingDefined.NextWordIndex);

            return 1;
        }

        // (a-addr1 -- a-addr2)
        private int CellPlusAction()
        {
            _interpreter.StackExpect(1);

            var addr = _interpreter.Pop();
            
            _interpreter.CheckCellAlignedAddress(addr);
            
            _interpreter.Push(addr + ByteHeap.CellSize);

            return 1;
        }
        
        // (n1 -- n2)
        private int CellsAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Push(_interpreter.Pop() * ByteHeap.CellSize);

            return 1;
        }

        // ( -- n)
        private int CharAction()
        {
            _interpreter.StackFree(1);

            _interpreter.Push(_interpreter.ParseWord(false)[0]);

            return 1;
        }

        // CONSTANT word-name
        // (n -- )
        private int ConstantAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.ParseWord(), _interpreter.Pop()));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // ( -- n) {s -- s}
        private int CountAction()
        {
            _interpreter.ObjectStackExpect(1);
            _interpreter.StackFree(1);

            var s = _interpreter.OPop().ToString();
            _interpreter.Push(s.Length);
            _interpreter.OPush(s);

            return 1;
        }

        // ( -- )
        private int CrAction()
        {
            _interpreter.Output.WriteLine();

            return 1;
        }

        // CREATE word-name
        // ( -- )
        private int CreateAction()
        {
            _interpreter.BeginNewWordCompilation();

            // Align the data pointer.
            AlignAction();
            
            _interpreter.WordBeingDefined = new CreatedWord(_interpreter, _interpreter.ParseWord(), _interpreter.State.Heap.Top + 1);
            _interpreter.AddWord(_interpreter.WordBeingDefined);
            
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // ( -- )
        private int DecimalAction()
        {
            _interpreter.State.SetBaseValue(10);

            return 1;
        }

        // ( -- n)
        private int DepthAction()
        {
            _interpreter.StackFree(1);

            _interpreter.Push(_interpreter.State.Stack.Count);

            return 1;
        }

        // (limit index -- ) [ - limit index ]
        private int DoAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("DO outside a new word definition.");
            }

            _interpreter.ReturnStackFree(1);

            _interpreter.RPush(
                _interpreter.WordBeingDefined.AddWord(
                    new DoControlWord(_interpreter)));

            return 1;
        }

        // ( -- )
        private int DoesAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("DOES> outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(
                    new DoesWord(_interpreter, _interpreter.WordBeingDefined, _interpreter.WordBeingDefined.NextWordIndex));

            return 1;
        }

        // (n -- )
        private int DropAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Drop();

            return 1;
        }

        // (n -- n n)
        private int DupAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.StackFree(1);

            _interpreter.Dup();

            return 1;
        }

        // ( -- )
        private int ElseAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("ELSE outside a new word definition.");
            }

            var ifControlWord = _interpreter.WordBeingDefined.GetWord(_interpreter.RPeek());
            if (ifControlWord is IfControlWord)
            {
                _interpreter.ReturnStackFree(1);

                // Get the index past where ELSE will be.
                var indexFollowingElse = _interpreter.WordBeingDefined.NextWordIndex + 1;

                // Instantiate the ELSE runtime code passing the index following ELSE.
                // Push execute address of ELSE word onto control flow stack.
                _interpreter.RPush(
                    _interpreter.WordBeingDefined.AddWord(
                        new ElseControlWord(_interpreter, indexFollowingElse)));

                // Inform the if control word of this index as well
                ((IfControlWord)ifControlWord).SetElseIndex(indexFollowingElse);
            }
            else
            {
                throw new Exception("ELSE requires a previous IF.");
            }

            return 1;
        }

        // (n -- )
        private int EmitAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Output.Write("{0}", (char)_interpreter.Pop());

            return 1;
        }
        
        // {s -- }
        private int EvaluateAction()
        {
            _interpreter.ObjectStackExpect(1);

            _interpreter.Evaluate(new StringSourceReader(_interpreter.OPop().ToString()));

            return 1;
        }

        // (xt -- )
        private int ExecuteAction()
        {
            _interpreter.StackExpect(1);

            return _interpreter.Execute(_interpreter.State.WordsList.GetWord(_interpreter.Pop()));
        }

        // ( - )
        private int ExitAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("EXIT outside a new word definition.");
            }

            // EXIT word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(new ExitControlWord(_interpreter, _interpreter.WordBeingDefined));

            return 1;
        }

        // (c-addr count char --)
        private int FillAction()
        {
            _interpreter.StackExpect(3);

            var v = (byte)_interpreter.Pop();
            var count = _interpreter.Pop();
            if (count > 0)
            {
                var addr = _interpreter.Pop();     
                
                _interpreter.CheckCellAlignedAddress(addr);
                _interpreter.CheckAddressesRange(addr, count);
                
                _interpreter.State.Heap.Fill(addr, count, v);
            }
            else
            {
                _interpreter.Drop();
            }

            return 1;
        }

        // ( -- 0 | xt 1 | xt -1) {s -- s | }
        private int FindAction()
        {
            _interpreter.ObjectStackExpect(1);
            _interpreter.StackFree(1);

            var wordName = _interpreter.OPop().ToString();
            if (_interpreter.State.WordsList.IsWordDefined(wordName.ToUpperInvariant()))
            {
                var word = _interpreter.State.WordsList.GetWord(wordName.ToUpperInvariant());

                _interpreter.Push(word.ExecutionToken);

                _interpreter.StackFree(1);

                _interpreter.Push(word.IsImmediate ? 1 : -1);
            }
            else
            {
                _interpreter.Push(0);
                _interpreter.OPush(wordName);
            }

            return 1;
        }

        // (d n1 -- n2 n3)
        private int FMSlashModAction()
        {
            _interpreter.StackExpect(3);

            // TODO: Fix to produce floored quotient.

            var n1 = (long)_interpreter.Pop();
            var d = _interpreter.DPop();
            _interpreter.Push((int)(d % n1));  // n2
            _interpreter.Push((int)(d / n1));  // n3

            return 1;
        }

        // ( -- addr)
        private int HereAction()
        {
            _interpreter.StackFree(1);

            _interpreter.Push(_interpreter.State.Heap.Top);

            return 1;
        }

        // ( -- )
        private int GetInnerIndexAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("I outside a new word definition.");
            }

            // I word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(new IndexControlWord(_interpreter));

            return 1;
        }

        // IF ... ELSE ... THEN
        // ( flag -- )
        private int IfAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("IF outside a new word definition.");
            }

            _interpreter.ReturnStackFree(1);

            _interpreter.RPush(
                _interpreter.WordBeingDefined.AddWord(
                    new IfControlWord(_interpreter, _interpreter.WordBeingDefined.NextWordIndex)));

            return 1;
        }

        // ( -- )
        private int ImmediateAction()
        {
            if (_interpreter.WordBeingDefined == null)
            {
                throw new Exception("No previous word definition to be set as immediate found.");
            }

            _interpreter.WordBeingDefined.SetImmediate();

            return 1;
        }

        // (n1 -- n2)
        private int InvertAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((a) => ~a);

            return 1;
        }

        // ( -- )
        private int GetOuterIndexAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("J outside a new word definition.");
            }

            // J word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(new SecondIndexControlWord(_interpreter));

            return 1;
        }
        
        // ( -- )
        private int LeaveAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("LEAVE outside a new word definition.");
            }

            // LEAVE word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(new LeaveControlWord(_interpreter));

            return 1;
        }

        // (n -- )
        private int LiteralAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("LITERAL outside a new word definition.");
            }

            _interpreter.StackExpect(1);

            _interpreter.WordBeingDefined.AddWord(new SingleCellIntegerLiteralWord(_interpreter, _interpreter.Pop()));

            return 1;
        }

        // ( -- )
        private int LoopAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("LOOP outside a new word definition.");
            }

            var doWordIndex = _interpreter.RPop();

            var loopIndex = _interpreter.WordBeingDefined.AddWord(
                new LoopControlWord(
                    _interpreter,
                    (doWordIndex + 1) - _interpreter.WordBeingDefined.NextWordIndex));  // c + 1 -> index of the word folowing DO/?DO.

            // TODO: Toto v CORE-EXT udělat LoopAction, která přepíše tuto základní verzi.
            var cWord = _interpreter.WordBeingDefined.GetWord(doWordIndex);
            if (cWord is IBranchingWord)
            {
                ((IBranchingWord)cWord).SetBranchTargetIndex(loopIndex);
            }

            return 1;
        }

        // (n1 u -- n2)
        private int LShiftAction()
        {
            _interpreter.StackExpect(2);

            var bits = _interpreter.Pop();
            _interpreter.Push(_interpreter.Pop() << bits);

            return 1;
        }

        // (n1 n2 -- d)
        private int MStarAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.DPush((long)_interpreter.Pop() * (long)_interpreter.Pop());

            return 1;
        }

        // (n1 n2 -- n3)
        private int MaxAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => (n1 > n2) ? n1 : n2);

            return 1;
        }

        // (n1 n2 -- n3)
        private int MinAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => (n1 < n2) ? n1 : n2);

            return 1;
        }

        // (n1 n2 -- n3)
        private int ModAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => n1 % n2);

            return 1;
        }

        // (addr1 addr2 u -- )
        private int MoveAction()
        {
            _interpreter.StackExpect(3);

            var count = _interpreter.Pop();
            if (count > 0)
            {
                var addr2 = _interpreter.Pop();
                _interpreter.State.Heap.Move(_interpreter.Pop(), addr2, count);
            }
            else
            {
                _interpreter.Drop(2);
            }
            
            return 1;
        }

        // (n1 -- n2)
        private int NegateAction()
        {
            _interpreter.StackExpect(1);

            _interpreter.Function((n1) => -n1);

            return 1;
        }

        // (n1 n2 -- n3)
        private int OrAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => n1 | n2);

            return 1;
        }

        // (a b -- a b a)
        private int OverAction()
        {
            _interpreter.StackExpect(2);
            _interpreter.StackFree(1);

            _interpreter.Over();

            return 1;
        }

        // POSTPONE word-name
        // ( -- )
        private int PostponeAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("POSTPONE outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(_interpreter.GetWord(_interpreter.ParseWord()));

            return 1;
        }

        // ( -- )
        private int QuitAction()
        {
            _interpreter.Quit();

            return 1;
        }

        // ( -- a) [a - ]
        private int RFromAction()
        {
            _interpreter.StackFree(1);
            _interpreter.ReturnStackExpect(1);

            _interpreter.Push(_interpreter.RPop());

            return 1;
        }

        // ( -- a) [a - a]
        private int RFetchAction()
        {
            _interpreter.StackFree(1);
            _interpreter.ReturnStackExpect(1);

            _interpreter.Push(_interpreter.RPeek());

            return 1;
        }

        // ( -- )
        private int RecurseAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("RECURSE outside a new word definition.");
            }

            // RECURSE word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(_interpreter.WordBeingDefined);

            return 1;
        }

        // [ index-of-a-word-following-BEGIN -- ]
        private int RepeatAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("REPEAT outside a new word definition.");
            }

            // REPEAT word doesn't have a runtime behavior.

            _interpreter.ReturnStackExpect(2);

            // Get the index of the next free slot in the non-primitive word being defined.
            var repeatIndex = _interpreter.WordBeingDefined.NextWordIndex;

            var controlWord = _interpreter.WordBeingDefined.GetWord(_interpreter.RPop());
            if (controlWord is WhileControlWord)
            {
                // We had a previous WHILE. Set the REPEAT index into
                // the WHILE control word.
                ((WhileControlWord)controlWord).SetRepeatIndex(repeatIndex);
            }
            else
            {
                throw new Exception("REPEAT requires a previous WHILE.");
            }

            _interpreter.WordBeingDefined.AddWord(
                new RepeatControlWord(
                    _interpreter,
                    _interpreter.RPop() - _interpreter.WordBeingDefined.NextWordIndex));

            return 1;
        }

        // (n1 n2 n3 -- n2 n3 n1)
        private int RoteAction()
        {
            _interpreter.StackExpect(3);

            _interpreter.Rot();

            return 1;
        }

        // (n1 u -- n2)
        private int RShiftAction()
        {
            _interpreter.StackExpect(2);

            var bits = _interpreter.Pop();
            _interpreter.Push(_interpreter.Pop() >> bits);

            return 1;
        }

        // { -- s}
        private int SQuoteAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("S\" outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(new StringLiteralWord(_interpreter, _interpreter.ParseTerminatedString('"')));

            return 1;
        }

        // (n -- d)
        private int SToDAction()
        {
            _interpreter.StackExpect(1);
            _interpreter.StackFree(1);

            _interpreter.DPush(_interpreter.Pop());

            return 1;
        }

        // (d n1 -- n2 n3)
        private int SMSlashRemAction()
        {
            _interpreter.StackExpect(3);

            // TODO: Fix to produce symmetric quotient.

            var n1 = (long)_interpreter.Pop();
            var d = _interpreter.DPop();
            _interpreter.Push((int)(d % n1));  // n2
            _interpreter.Push((int)(d / n1));  // n3

            return 1;
        }

        // ( -- )
        private int SpaceAction()
        {
            _interpreter.Output.Write(" ");

            return 1;
        }

        // (n -- )
        private int SpacesAction()
        {
            _interpreter.StackExpect(1);

            var count = _interpreter.Pop();
            if (count > 0)
            {
                var sb = new StringBuilder(count);
                for (var i = 0; i < count; i++)
                {
                    sb.Append(' ');
                }

                _interpreter.Output.Write(sb.ToString());
            }

            return 1;
        }

        // ( -- addr)
        private int StateAction()
        {
            _interpreter.StackFree(1);

            _interpreter.Push(_interpreter.State.StateVariableAddress);

            return 1;
        }

        // (n1 n2 -- n2 n1)
        private int SwapAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Swap();

            return 1;
        }

        // ( -- )
        private int ThenAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("THEN outside a new word definition.");
            }

            _interpreter.ReturnStackExpect(1);

            // Get the index of the next free slot in the non-primitive word being defined.
            var thenIndex = _interpreter.WordBeingDefined.NextWordIndex;

            var controlWord = _interpreter.WordBeingDefined.GetWord(_interpreter.RPop());
            if (controlWord is ElseControlWord)
            {
                // We had a previous else 
                ((ElseControlWord)controlWord).SetThenIndexIncrement(thenIndex);

                _interpreter.ReturnStackExpect(1);

                // Pop control stack again to find IF.
                controlWord = _interpreter.WordBeingDefined.GetWord(_interpreter.RPop());
            }

            if (controlWord is IfControlWord)
            {
                // We had a previous if. Set the then index into
                // the if control word.
                ((IfControlWord)controlWord).SetThenIndex(thenIndex);
            }
            else
            {
                throw new Exception("THEN requires a previous IF or ELSE.");
            }

            return 1;
        }

        // {s -- }
        private int TypeAction()
        {
            _interpreter.ObjectStackExpect(1);

            _interpreter.Output.Write(_interpreter.OPop().ToString());

            return 1;
        }

        // (u --)
        private int UDotAction()
        {
            _interpreter.StackExpect(1);

            //_interpreter.Output.Write("{0} ", new UnsignedSingleCellIntegerValue()
            //{
            //    V = _interpreter.Pop()
            //}.U);

            _interpreter.Output.Write("{0}", (uint)_interpreter.Pop());

            return 1;
        }

        // (u1 u2 -- flag)
        private int ULessThanAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.UFunction((u1, u2) => (u1 < u2) ? -1 : 0);

            return 1;
        }

        // (u1 u2 -- ud)
        private int UMStarAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.UDPush((ulong)_interpreter.Pop() * (ulong)_interpreter.Pop());

            return 1;
        }

        // (ud u1 -- u2 u3)
        private int UMSlashModAction()
        {
            _interpreter.StackExpect(3);

            var u1 = (ulong)_interpreter.Pop();
            var ud = (ulong)_interpreter.DPop();
            _interpreter.Push((int)(ud % u1));  // u2
            _interpreter.Push((int)(ud / u1));  // u3

            return 1;
        }

        // ( -- )
        private int UnloopAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("UNLOOP outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(new UnloopControlWord(_interpreter));

            return 1;
        }

        // [ index-of-a-word-following-BEGIN -- ]
        private int UntilAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("UNTIL outside a new word definition.");
            }

            _interpreter.ReturnStackExpect(1);

            // UNTIL word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(
                new UntilControlWord(
                    _interpreter,
                    _interpreter.RPop() - _interpreter.WordBeingDefined.NextWordIndex));

            return 1;
        }

        // VARIABLE word-name
        // ( -- )
        private int VariableAction()
        {
            if (_interpreter.IsCompiling)
            {
                throw new Exception("VARIABLE inside a new word definition.");
            }

            _interpreter.BeginNewWordCompilation();

            // Align the data pointer.
            AlignAction();
            
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.ParseWord(), _interpreter.State.Heap.Alloc(ByteHeap.CellSize)));
            
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // BEGIN ... WHILE ... REPEAT
        // ( flag -- )
        private int WhileAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("WHILE outside a new word definition.");
            }

            _interpreter.ReturnStackExpect(1);

            _interpreter.RPush(
                _interpreter.WordBeingDefined.AddWord(
                    new WhileControlWord(_interpreter, _interpreter.WordBeingDefined.NextWordIndex)));

            return 1;
        }

        // (c -- ) { -- s}
        private int WordAction()
        {
            //if (_interpreter.IsCompiling == false)
            //{
            //    throw new Exception("WORD outside a new word definition.");
            //}

            _interpreter.StackExpect(1);
            _interpreter.ObjectStackFree(1);

            //_interpreter.WordBeingDefined.AddWord(new StringLiteralWord(_interpreter, _interpreter.GetTerminatedString((char)_interpreter.Pop(), false, true)));
            _interpreter.OPush(_interpreter.ParseTerminatedString((char)_interpreter.Pop(), false, true));

            return 1;
        }

        // (n1 n2 -- n3)
        private int XorAction()
        {
            _interpreter.StackExpect(2);

            _interpreter.Function((n1, n2) => n1 ^ n2);

            return 1;
        }

        // ( -- )
        private int LeftBracketAction()
        {
            _interpreter.SuspendNewWordCompilation();

            return 1;
        }

        // ['] word-name
        // ( -- xt)
        private int BracketTickAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("['] outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(new SingleCellIntegerLiteralWord(_interpreter, _interpreter.GetWord(_interpreter.ParseWord()).ExecutionToken));

            return 1;
        }

        // ( -- n)
        private int BracketCharAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("[CHAR] outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(new SingleCellIntegerLiteralWord(_interpreter, _interpreter.ParseWord()[0]));

            return 1;
        }

        // ( -- )
        private int RightBracketAction()
        {
            _interpreter.ResumeNewWordCompilation();

            return 1;
        }
    }
}
