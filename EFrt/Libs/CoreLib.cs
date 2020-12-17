/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;

    using EFrt.Words;
    using static EFrt.Token;


    public class CoreLib : IWordsLIbrary
    {
        private IInterpreter _interpreter;


        public CoreLib(IInterpreter efrt)
        {
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new ImmediateWord(_interpreter, "(", CommentAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "\\", CommentLineAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, ":", BeginNewWordCompilationAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, ";", EndNewWordCompilationAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CONSTANT", ConstantCompilationAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2CONSTANT", DoubleConstantCompilationAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ALLOT", AllotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "HERE", HereAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "VARIABLE", VariableCompilationAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2VARIABLE", DoubleVariableCompilationAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "!", StoreToVariableAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2!", DoubleStoreToVariableAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "@", FetchFromVariableAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2@", DoubleFetchFromVariableAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DUP", DupAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2DUP", DupTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "?DUP", DupPosAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DROP", DropAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2DROP", DropTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "SWAP", SwapAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2SWAP", SwapTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OVER", OverAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2OVER", OverTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "PICK", PickAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ROLL", RollAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ROT", RotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2ROT", RotTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "-ROT", RotBackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DEPTH", DepthAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CLEAR", ClearAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, ">R", ToReturnStackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "R>", FromReturnStackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "R@", FetchReturnStackAction));

            _interpreter.AddWord(new ConstantWord(_interpreter, "BL", ' '));
            _interpreter.AddWord(new ConstantWord(_interpreter, "FALSE", 0));
            _interpreter.AddWord(new ConstantWord(_interpreter, "TRUE", -1));

            _interpreter.AddWord(new ImmediateWord(_interpreter, "IF", IfAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "ELSE", ElseAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "THEN", ThenAction));

            _interpreter.AddWord(new ImmediateWord(_interpreter, "DO", DoAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "?DO", IfDoAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "LOOP", LoopAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "+LOOP", PlusLoopAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "BEGIN", BeginAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "WHILE", WhileAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "REPEAT", RepeatAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "AGAIN", AgainAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "UNTIL", UntilAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "I", GetInnerIndexAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "J", GetOuterIndexAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "LEAVE", LeaveAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "BYE", ByeAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FORGET", ForgetAction));
        }


        // (a -- a a)
        private int DupAction()
        {
            _interpreter.Dup();

            return 1;
        }

        // (a b -- a b a b)
        private int DupTwoAction()
        {
            var b = _interpreter.Pop();
            var a = _interpreter.Peek();

            _interpreter.Push(b);
            _interpreter.Push(a);
            _interpreter.Push(b);

            return 1;
        }

        // (a -- a a) or (a -- 0)
        private int DupPosAction()
        {
            if (_interpreter.Peek() != 0)
            {
                _interpreter.Dup();
            }

            return 1;
        }

        // (a --)
        private int DropAction()
        {
            _interpreter.Drop();

            return 1;
        }

        // (a b --)
        private int DropTwoAction()
        {
            _interpreter.Drop(2);

            return 1;
        }

        // (a b -- b a)
        private int SwapAction()
        {
            _interpreter.Swap();

            return 1;
        }

        // (a b c d -- c d a b)
        private int SwapTwoAction()
        {
            var d = _interpreter.Pop();
            var c = _interpreter.Pop();
            var b = _interpreter.Pop();
            var a = _interpreter.Pop();

            _interpreter.Push(c);
            _interpreter.Push(d);
            _interpreter.Push(a);
            _interpreter.Push(b);

            return 1;
        }

        // (a b -- a b a)
        private int OverAction()
        {
            _interpreter.Over();

            return 1;
        }

        // (a b c d -- a b c d a b)
        private int OverTwoAction()
        {
            var d = _interpreter.Pop();
            var c = _interpreter.Pop();
            var b = _interpreter.Pop();
            var a = _interpreter.Peek();

            _interpreter.Push(b);
            _interpreter.Push(c);
            _interpreter.Push(d);
            _interpreter.Push(a);
            _interpreter.Push(b);

            return 1;
        }

        // (index -- n)
        private int PickAction()
        {
            _interpreter.Push(_interpreter.Pick(_interpreter.Pop()));

            return 1;
        }

        // (index -- )
        private int RollAction()
        {
            _interpreter.Roll(_interpreter.Pop());

            return 1;
        }

        // (a b c -- b c a)
        private int RotAction()
        {
            _interpreter.Rot();

            return 1;
        }

        // (a b c d e f -- c d e f a b)
        private int RotTwoAction()
        {
            var f = _interpreter.Pop();
            var e = _interpreter.Pop();
            var d = _interpreter.Pop();
            var c = _interpreter.Pop();
            var b = _interpreter.Pop();
            var a = _interpreter.Pop();

            _interpreter.Push(c);
            _interpreter.Push(d);
            _interpreter.Push(e);
            _interpreter.Push(f);
            _interpreter.Push(a);
            _interpreter.Push(b);

            return 1;
        }

        // (a b c -- c a b)
        private int RotBackAction()
        {
            var v3 = _interpreter.Pop();
            var v2 = _interpreter.Pop();
            var v1 = _interpreter.Pop();

            _interpreter.Push(v3);
            _interpreter.Push(v1);
            _interpreter.Push(v2);

            return 1;
        }

        // ( -- a)
        private int DepthAction()
        {
            _interpreter.Push(_interpreter.Stack.Count);

            return 1;
        }

        // ( -- )
        private int ClearAction()
        {
            _interpreter.Stack.Clear();

            return 1;
        }

        // (a -- ) [ - a ]
        private int ToReturnStackAction()
        {
            _interpreter.RPush(_interpreter.Pop());

            return 1;
        }

        // ( -- a) [a - ]
        private int FromReturnStackAction()
        {
            _interpreter.Push(_interpreter.RPop());

            return 1;
        }

        // ( -- a) [a - a]
        private int FetchReturnStackAction()
        {
            _interpreter.Push(_interpreter.RPeek());

            return 1;
        }


        private int CommentAction()
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


        private int CommentLineAction()
        {
            _interpreter.NextChar();
            while (_interpreter.CurrentChar != 0)
            {
                if (_interpreter.CurrentChar == '\n')
                {
                    _interpreter.NextChar();

                    break;
                }

                _interpreter.NextChar();
            }

            return 1;
        }


        // : word-name body ;
        private int BeginNewWordCompilationAction()
        {
            _interpreter.WordBeingDefined = new NonPrimitiveWord(_interpreter, _interpreter.BeginNewWordCompilation());

            return 1;
        }

        // : word-name body ;
        private int EndNewWordCompilationAction()
        {
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // CONSTANT word-name
        // (n1 -- )
        private int ConstantCompilationAction()
        {
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.BeginNewWordCompilation(), _interpreter.Pop()));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // 2CONSTANT word-name
        // (n1 n2 -- )
        private int DoubleConstantCompilationAction()
        {
            var n2 = _interpreter.Pop();
            _interpreter.AddWord(new DoubleConstantWord(_interpreter, _interpreter.BeginNewWordCompilation(), _interpreter.Pop(), n2));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (n -- addr)
        private int AllotAction()
        {
            _interpreter.Push(_interpreter.Heap.Alloc(_interpreter.Pop()));

            return 1;
        }

        // ( -- addr)
        private int HereAction()
        {
            _interpreter.Push(_interpreter.Heap.Top);

            return 1;
        }

        // VARIABLE word-name
        // ( -- )
        private int VariableCompilationAction()
        {
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.BeginNewWordCompilation(), _interpreter.Heap.Alloc(1)));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // 2VARIABLE word-name
        // ( -- )
        private int DoubleVariableCompilationAction()
        {
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.BeginNewWordCompilation(), _interpreter.Heap.Alloc(2)));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (n addr -- )
        private int StoreToVariableAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.Heap.Items[addr] = _interpreter.Pop();

            return 1;
        }

        // (n1 n2 addr -- )
        private int DoubleStoreToVariableAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.Heap.Items[addr + 1] = _interpreter.Pop();  // n2
            _interpreter.Heap.Items[addr] = _interpreter.Pop();      // n1

            return 1;
        }

        // (addr -- n)
        private int FetchFromVariableAction()
        {
            _interpreter.Push(_interpreter.Heap.Items[_interpreter.Pop()]);

            return 1;
        }

        // (addr -- n1 n2)
        private int DoubleFetchFromVariableAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.Push(_interpreter.Heap.Items[addr]);      // n1
            _interpreter.Push(_interpreter.Heap.Items[addr + 1]);  // n2

            return 1;
        }

        //// ( -- a)
        //private int FalseAction()
        //{
        //    _interpreter.Push(0);

        //    return 1;
        //}

        //// ( -- a)
        //private int TrueAction()
        //{
        //    _interpreter.Push(-1);

        //    return 1;
        //}

        // ( -- )
        private int ByeAction()
        {
            _interpreter.TerminateExecution();

            return 1;
        }

        // ( -- )
        private int ForgetAction()
        {
            // Cannot forget a word, when a new word is currently compiled.
            if (_interpreter.IsCompiling)
            {
                throw new Exception("A word compilation is running.");
            }

            // Get the name of a word.
            var tok = _interpreter.NextTok();
            switch (tok.Code)
            {
                case TokenType.Word:
                    _interpreter.ForgetWord(tok.SValue);
                    break;

                default:
                    throw new Exception($"A name of a word expected.");
            }

            return 1;
        }

        // IF ... ELSE .. THEN
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

        // BEGIN ... WHILE .. REPEAT
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

        // [ index-of-a-word-folowing-BEGIN -- ]
        private int AgainAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("AGAIN outside a new word definition.");
            }

            // AGAIN word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(
                new AgainControlWord(
                    _interpreter,
                    _interpreter.RPop() - _interpreter.WordBeingDefined.NextWordIndex));

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

        // (limit end -- )
        private int IfDoAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("?DO outside a new word definition.");
            }

            _interpreter.RPush(
                _interpreter.WordBeingDefined.AddWord(
                    new IfDoControlWord(_interpreter, _interpreter.WordBeingDefined.NextWordIndex)));

            return 1;
        }


        private int LoopAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("LOOP outside a new word definition.");
            }

            var cWordIndex = _interpreter.RPop();

            var loopIndex = _interpreter.WordBeingDefined.AddWord(
                new LoopControlWord(
                    _interpreter,
                    (cWordIndex + 1) - _interpreter.WordBeingDefined.NextWordIndex));  // c + 1 -> index of the word folowing DO/?DO.

            var cWord = _interpreter.WordBeingDefined.GetWord(cWordIndex);
            if (cWord is IfDoControlWord)
            {
                ((IfDoControlWord)cWord).SetLoopIndex(loopIndex);
            }
            
            return 1;
        }


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
            if (cWord is IfDoControlWord)
            {
                ((IfDoControlWord)cWord).SetLoopIndex(loopIndex);
            }

            return 1;
        }
    }
}
