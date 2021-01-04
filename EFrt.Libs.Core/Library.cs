/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.Core
{
    using System;
    using System.Text;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;

    using EFrt.Libs.Core.Words;

    using static EFrt.Core.Token;


    /// <summary>
    /// The CORE words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "CORE";

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
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "!", StoreAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "'", TickAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "(", ParenAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "*", StarAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "*/", StarSlashAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "*/MOD", StarSlashModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "+", PlusAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "+!", PlusStoreAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "+LOOP", PlusLoopAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ",", CommaAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "-", MinusAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".", DotAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, ".\"", DotQuoteAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "/", SlashAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "/MOD", SlashModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0<", ZeroLessAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0=", ZeroEqualsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "1+", OnePlusAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "1-", OneMinusAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2!", TwoStoreAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2*", TwoStarAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2/", TwoSlashAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2@", TwoFetchAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2DROP", TwoDropAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2DUP", TwoDupAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2OVER", TwoOverAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2SWAP", TwoSwapAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ":", ColonAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, ";", SemicolonAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "<", LessThanAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "=", EqualsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ">", GreaterThanAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ">R", ToRAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "?DUP", QuestionDupeAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "@", FetchAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ABORT", AbortAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "ABORT\"", AbortWithMessageAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ABS", AbsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ALLOT", AllotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "AND", AndAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "BEGIN", BeginAction));
            _interpreter.AddWord(new ConstantWord(_interpreter, "BL", ' '));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CELLS", () => 1));  // Does nothing, because the cell size is 1.
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CHAR", CharAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CONSTANT", ConstantAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CR", CrAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CREATE", CreateAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DEPTH", DepthAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "DO", DoAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "DOES>", DoesAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DROP", DropAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DUP", DupAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "ELSE", ElseAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "EMIT", EmitAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "EXECUTE", ExecuteAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "EXIT", ExitAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FM/MOD", FMSlashModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "HERE", HereAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "I", GetInnerIndexAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "IF", IfAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "IMMEDIATE", ImmediateAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "INVERT", InvertAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "J", GetOuterIndexAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "LEAVE", LeaveAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "LITERAL", LiteralAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "LSHIFT", LShiftAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "M*", MStarAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "LOOP", LoopAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "MAX", MaxAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "MIN", MinAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "MOD", ModAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "NEGATE", NegateAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OR", OrAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OVER", OverAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "POSTPONE", PostponeAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "QUIT", QuitAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "R>", RFromAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "R@", RFetchAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "RECURSE", RecurseAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "REPEAT", RepeatAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ROT", RoteAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "RSHIFT", RShiftAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "S\"", SQuoteAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S>D", SToDAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "SM/REM", SMSlashRemAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "SPACE", SpaceAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "SPACES", SpacesAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "SWAP", SwapAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "THEN", ThenAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "TYPE", TypeAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "UNLOOP", UnloopAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "UNTIL", UntilAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "VARIABLE", VariableAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "WHILE", WhileAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "XOR", XorAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "[']", BracketTickAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "[CHAR]", BracketCharAction));
        }


        private void Function(Func<int, int> func)
        {
            var stack = _interpreter.State.Stack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top]);
        }


        private void Function(Func<int, int, int> func)
        {
            var stack = _interpreter.State.Stack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);
        }


        private long DPop()
        {
            return new DoubleCellIntegerValue()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D;
        }


        private void DPush(long value)
        {
            var v = new DoubleCellIntegerValue()
            {
                D = value
            };

            _interpreter.Push(v.A);
            _interpreter.Push(v.B);
        }


        // (n addr -- )
        private int StoreAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.State.Heap.Items[addr] = _interpreter.Pop();

            return 1;
        }

        // ' word-name
        // ( -- xt)
        private int TickAction()
        {
            _interpreter.Push(_interpreter.GetWord(_interpreter.GetWordName()).ExecutionToken);

            return 1;
        }

        // ( -- )
        private int ParenAction()
        {
            var c = _interpreter.NextChar();
            while (_interpreter.CurrentChar != 0)
            {
                if (_interpreter.CurrentChar == ')')
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
            Function((n1, n2) => n1 * n2);

            return 1;
        }

        // (n1 n2 n3 -- n4)
        private int StarSlashAction()
        {
            var n3 = (long)_interpreter.Pop();
            _interpreter.Push((int)((long)_interpreter.Pop() * (long)_interpreter.Pop() / n3));  // n2 * n1 / n3 

            return 1;
        }

        // (n1 n2 n3 -- n4 n5)
        private int StarSlashModAction()
        {
            var n3 = (long)_interpreter.Pop();
            var d = (long)_interpreter.Pop() * (long)_interpreter.Pop();
            _interpreter.Push((int)(d % n3));  // n4 = d % n3
            _interpreter.Push((int)(d / n3));  // n5 = d / n3

            return 1;
        }

        // (n1 n2 -- n3)
        private int PlusAction()
        {
            Function((n1, n2) => n1 + n2);

            return 1;
        }

        // (n addr -- )
        private int PlusStoreAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.State.Heap.Items[addr] += _interpreter.Pop();

            return 1;
        }

        // Compilation: [index of the word folowing DO/?DO -- ], runtime: (n -- )
        private int PlusLoopAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("+LOOP outside a new word definition.");
            }

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
            _interpreter.State.Heap.Items[_interpreter.State.Heap.Alloc(1)] = _interpreter.Pop();

            return 1;
        }

        // (n1 n2 -- n3)
        private int MinusAction()
        {
            Function((n1, n2) => n1 - n2);

            return 1;
        }

        // (n --)
        private int DotAction()
        {
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

            var sb = new StringBuilder();

            var c = _interpreter.NextChar();
            while (_interpreter.CurrentChar != 0)
            {
                if (_interpreter.CurrentChar == '"')
                {
                    _interpreter.NextChar();

                    break;
                }

                sb.Append(_interpreter.CurrentChar);

                c = _interpreter.NextChar();
            }

            if (c != '"')
            {
                throw new Exception("'\"' expected.");
            }

            _interpreter.WordBeingDefined.AddWord(new PrintStringWord(_interpreter, sb.ToString()));

            return 1;
        }

        // (n1 n2 -- n3)
        private int SlashAction()
        {
            Function((n1, n2) => n1 / n2);

            return 1;
        }

        // (n1 n2b -- n3 n4)
        private int SlashModAction()
        {
            var n2 = _interpreter.Pop();
            var n1 = _interpreter.Pop();

            _interpreter.Push(n1 / n2);
            _interpreter.Push(n1 % n2);

            return 1;
        }

        // (n -- flag)
        private int ZeroLessAction()
        {
            Function((n) => (n < 0) ? -1 : 0);

            return 1;
        }

        // (n -- flag)
        private int ZeroEqualsAction()
        {
            Function((n) => (n == 0) ? -1 : 0);

            return 1;
        }

        // (n1 -- n2)
        private int OnePlusAction()
        {
            Function((n) => ++n);

            return 1;
        }

        // (n1 -- n2)
        private int OneMinusAction()
        {
            Function((n) => --n);

            return 1;
        }

        // (n1 n2 addr -- )
        private int TwoStoreAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.State.Heap.Items[addr + 1] = _interpreter.Pop();  // n2
            _interpreter.State.Heap.Items[addr] = _interpreter.Pop();      // n1

            return 1;
        }

        // (n1 -- n2)
        private int TwoStarAction()
        {
            Function((n) => n * 2);

            return 1;
        }

        // (n1 -- n2)
        private int TwoSlashAction()
        {
            Function((n) => n / 2);

            return 1;
        }

        // (addr -- n1 n2)
        private int TwoFetchAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.Push(_interpreter.State.Heap.Items[addr]);      // n1
            _interpreter.Push(_interpreter.State.Heap.Items[addr + 1]);  // n2

            return 1;
        }

        // (n1 n2 -- )
        private int TwoDropAction()
        {
            _interpreter.Drop(2);

            return 1;
        }

        // (n1 n2 -- n1 n2 n1 n2)
        private int TwoDupAction()
        {
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
            _interpreter.WordBeingDefined = new NonPrimitiveWord(_interpreter, _interpreter.GetWordName());

            return 1;
        }

        // : word-name body ;
        private int SemicolonAction()
        {
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (n1 n2 -- flag)
        private int LessThanAction()
        {
            Function((n1, n2) => (n1 < n2) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int EqualsAction()
        {
            Function((n1, n2) => (n1 == n2) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int GreaterThanAction()
        {
            Function((n1, n2) => (n1 > n2) ? -1 : 0);

            return 1;
        }

        // (a -- ) [ - a ]
        private int ToRAction()
        {
            _interpreter.RPush(_interpreter.Pop());

            return 1;
        }

        // (n -- n n) or ( -- )
        private int QuestionDupeAction()
        {
            if (_interpreter.Peek() != 0)
            {
                _interpreter.Dup();
            }

            return 1;
        }

        // (addr -- n)
        private int FetchAction()
        {
            _interpreter.Push(_interpreter.State.Heap.Items[_interpreter.Pop()]);

            return 1;
        }

        // ( -- )
        public int AbortAction()
        {
            _interpreter.State.Stack.Clear();
            _interpreter.State.ObjectStack.Clear();

            // TODO: Clear the heap?

            return QuitAction();
        }

        // ( -- )
        private int AbortWithMessageAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("ABORT\" outside a new word definition.");
            }

            // TODO: Interpreter.GetTerminatedString(string terminator)

            var sb = new StringBuilder();

            var c = _interpreter.NextChar();
            while (_interpreter.CurrentChar != 0)
            {
                if (_interpreter.CurrentChar == '"')
                {
                    _interpreter.NextChar();

                    break;
                }

                sb.Append(_interpreter.CurrentChar);

                c = _interpreter.NextChar();
            }

            if (c != '"')
            {
                throw new Exception("'\"' expected.");
            }

            _interpreter.WordBeingDefined.AddWord(new AbortWithMessageWord(_interpreter, sb.ToString()));

            return 1;
        }

        // (n1 -- n2)
        private int AbsAction()
        {
            Function((n1) => n1 < 0 ? -n1 : n1);

            return 1;
        }

        // (n -- )
        private int AllotAction()
        {
            // We do not return the "address" returned by the Alloc() method.
            _ = _interpreter.State.Heap.Alloc(_interpreter.Pop());

            return 1;
        }

        // (n1 n2 -- n3)
        private int AndAction()
        {
            Function((n1, n2) => n1 & n2);

            return 1;
        }

        // [ -- index-of-a-word-folowing-BEGIN ]
        private int BeginAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("BEGIN outside a new word definition.");
            }

            // BEGIN word doesn't have a runtime behavior.

            _interpreter.RPush(_interpreter.WordBeingDefined.NextWordIndex);

            return 1;
        }

        // ( -- n)
        private int CharAction()
        {
            // Get the name.
            var tok = _interpreter.NextTok();
            switch (tok.Code)
            {
                case TokenType.Word:
                    _interpreter.Push(tok.SValue[0]);
                    break;

                default:
                    throw new Exception("A name expected.");
            }

            return 1;
        }

        // CONSTANT word-name
        // (n -- )
        private int ConstantAction()
        {
            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.GetWordName(), _interpreter.Pop()));
            _interpreter.EndNewWordCompilation();

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
            _interpreter.WordBeingDefined = new CreatedWord(_interpreter, _interpreter.GetWordName(), _interpreter.State.Heap.Top + 1);
            _interpreter.AddWord(_interpreter.WordBeingDefined);
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // ( -- n)
        private int DepthAction()
        {
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

        // (n --)
        private int DropAction()
        {
            _interpreter.Drop();

            return 1;
        }

        // (n -- n n)
        private int DupAction()
        {
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
                // Get the index past where ELSE will be.
                var indexFolowingElse = _interpreter.WordBeingDefined.NextWordIndex + 1;

                // Instantiate the ELSE runtime code passing the index following ELSE.
                // Push execute address of ELSE word onto control flow stack.
                _interpreter.RPush(
                    _interpreter.WordBeingDefined.AddWord(
                        new ElseControlWord(_interpreter, indexFolowingElse)));

                // Inform the if control word of this index as well
                ((IfControlWord)ifControlWord).SetElseIndex(indexFolowingElse);
            }
            else
            {
                throw new Exception("ELSE requires a previous IF.");
            }

            return 1;
        }

        // (n --)
        private int EmitAction()
        {
            _interpreter.Output.Write("{0}", (char)_interpreter.Pop());

            return 1;
        }

        // (xt --)
        private int ExecuteAction()
        {
            return _interpreter.State.WordsList.GetWord(_interpreter.Pop()).Action();
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

        // (d n1 -- n2 n3)
        private int FMSlashModAction()
        {
            // TODO: Fix to produce floored quotient.

            var n1 = (long)_interpreter.Pop();
            var d = DPop();
            _interpreter.Push((int)(d % n1));  // n2
            _interpreter.Push((int)(d / n1));  // n3

            return 1;
        }

        // ( -- addr)
        private int HereAction()
        {
            _interpreter.Push(_interpreter.State.Heap.Top);

            return 1;
        }

        // ( -- n)
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
            Function((a) => ~a);

            return 1;
        }

        // ( -- n)
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
            var bits = _interpreter.Pop();
            _interpreter.Push(_interpreter.Pop() << bits);

            return 1;
        }

        // (n1 n2 -- d)
        private int MStarAction()
        {
            DPush((long)_interpreter.Pop() * (long)_interpreter.Pop());

            return 1;
        }

        // (n1 n2 -- n3)
        private int MaxAction()
        {
            Function((n1, n2) => (n1 > n2) ? n1 : n2);

            return 1;
        }

        // (n1 n2 -- n3)
        private int MinAction()
        {
            Function((n1, n2) => (n1 < n2) ? n1 : n2);

            return 1;
        }

        // (n1 n2 -- n3)
        private int ModAction()
        {
            Function((n1, n2) => n1 % n2);

            return 1;
        }

        // (n1 -- n2)
        private int NegateAction()
        {
            Function((n1) => -n1);

            return 1;
        }

        // (n1 n2 -- n3)
        private int OrAction()
        {
            Function((n1, n2) => n1 | n2);

            return 1;
        }

        // (a b -- a b a)
        private int OverAction()
        {
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

            _interpreter.WordBeingDefined.AddWord(_interpreter.GetWord(_interpreter.GetWordName()));

            return 1;
        }

        // ( -- )
        private int QuitAction()
        {
            _interpreter.State.ReturnStack.Clear();
            _interpreter.BreakExecution();

            return 1;
        }

        // ( -- a) [a - ]
        private int RFromAction()
        {
            _interpreter.Push(_interpreter.RPop());

            return 1;
        }

        // ( -- a) [a - a]
        private int RFetchAction()
        {
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

            // LEAVE word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(new RuntimeWord(_interpreter, _interpreter.WordBeingDefined.Name));

            return 1;
        }

        // [ index-of-a-word-folowing-BEGIN -- ]
        private int RepeatAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("REPEAT outside a new word definition.");
            }

            // REPEAT word doesn't have a runtime behavior.

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
            _interpreter.Rot();

            return 1;
        }

        // (n1 u -- n2)
        private int RShiftAction()
        {
            var bits = _interpreter.Pop();
            _interpreter.Push(_interpreter.Pop() >> bits);

            return 1;
        }

        // { -- s}
        private int SQuoteAction()
        {
            var sb = new StringBuilder();

            var c = _interpreter.NextChar();
            while (_interpreter.CurrentChar != 0)
            {
                if (_interpreter.CurrentChar == '"')
                {
                    _interpreter.NextChar();

                    break;
                }

                sb.Append(_interpreter.CurrentChar);

                c = _interpreter.NextChar();
            }

            if (c != '"')
            {
                throw new Exception("'\"' expected.");
            }

            c = _interpreter.CurrentChar;
            if (c != 0 && Tokenizer.IsWhite(c) == false)
            {
                throw new Exception("The EOF or an white character after a string literal expected.");
            }

            if (_interpreter.IsCompiling)
            {
                _interpreter.WordBeingDefined.AddWord(new StringLiteralWord(_interpreter, sb.ToString()));
            }
            else
            {
                _interpreter.OPush(sb.ToString());
            }

            return 1;
        }

        // (n -- d)
        private int SToDAction()
        {
            DPush(_interpreter.Pop());

            return 1;
        }

        // (d n1 -- n2 n3)
        private int SMSlashRemAction()
        {
            // TODO: Fix to produce symmetric quotient.

            var n1 = (long)_interpreter.Pop();
            var d = DPop();
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

        // (n1 n2 -- n2 n1)
        private int SwapAction()
        {
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

            // Get the index of the next free slot in the non-primitive word being defined.
            var thenIndex = _interpreter.WordBeingDefined.NextWordIndex;

            var controlWord = _interpreter.WordBeingDefined.GetWord(_interpreter.RPop());
            if (controlWord is ElseControlWord)
            {
                // We had a previous else 
                ((ElseControlWord)controlWord).SetThenIndexIncrement(thenIndex);

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
            _interpreter.Output.Write(_interpreter.OPop().ToString());

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

        // [ index-of-a-word-folowing-BEGIN -- ]
        private int UntilAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("UNTIL outside a new word definition.");
            }

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
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.GetWordName(), _interpreter.State.Heap.Alloc(1)));
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

            _interpreter.RPush(
                _interpreter.WordBeingDefined.AddWord(
                    new WhileControlWord(_interpreter, _interpreter.WordBeingDefined.NextWordIndex)));

            return 1;
        }

        // (n1 n2 -- n3)
        private int XorAction()
        {
            Function((n1, n2) => n1 ^ n2);

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

            _interpreter.WordBeingDefined.AddWord(new SingleCellIntegerLiteralWord(_interpreter, _interpreter.GetWord(_interpreter.GetWordName()).ExecutionToken));

            return 1;
        }

        // ( -- n)
        private int BracketCharAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("[CHAR] outside a new word definition.");
            }

             _interpreter.WordBeingDefined.AddWord(new SingleCellIntegerLiteralWord(_interpreter, _interpreter.GetWordName()[0]));

            return 1;
        }
    }
}
