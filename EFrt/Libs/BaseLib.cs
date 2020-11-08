/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;

    using EFrt.Words;
    using static EFrt.Token;

    public class BaseLib : IWordsLIbrary
    {
        private IInterpreter _interpreter;


        public BaseLib(IInterpreter efrt)
        {
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "(", true, CommentAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "\\", true, CommentLineAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, ":", false, BeginNewWordCompilationAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ";", true, EndNewWordCompilationAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DUP", false, DupAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2DUP", false, DupTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "?DUP", false, DupPosAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DROP", false, DropAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2DROP", false, DropTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "SWAP", false, SwapAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2SWAP", false, SwapTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OVER", false, OverAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2OVER", false, OverTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ROT", false, RotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2ROT", false, RotTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "-ROT", false, RotBackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DEPTH", false, DepthAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, ">R", false, ToReturnStackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "R>", false, FromReturnStackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "R@", false, FetchReturnStackAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FALSE", false, FalseAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "TRUE", false, TrueAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "IF", true, IfAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ELSE", true, ElseAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "THEN", true, ThenAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "BEGIN", true, BeginAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "REPEAT", true, RepeatAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "BYE", false, ByeAction));


            // I J LEAVE

            // CONSTANT VARIABLE ! @ ARRAY WORDSD IF THEN ELSE 
            // ' EXECUTE INT FLOAT STRING

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DO", true, DoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "?DO", true, IfDoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "LOOP", true, LoopAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "+LOOP", true, PlusLoopAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "I", false, IndexAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FORGET", false, ForgetAction));
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

        // (a -- a [result])
        private int DupPosAction()
        {
            if (_interpreter.Peek().Int != 0)
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
            _interpreter.Pushi(_interpreter.Stack.Count);

            return 1;
        }

        // (a -- ) [ - a ]
        private int ToReturnStackAction()
        {
            _interpreter.RPush(_interpreter.Popi());

            return 1;
        }

        // ( -- a) [a - ]
        private int FromReturnStackAction()
        {
            _interpreter.Pushi(_interpreter.RPop());

            return 1;
        }

        // ( -- a) [a - a]
        private int FetchReturnStackAction()
        {
            _interpreter.Pushi(_interpreter.RPeek());

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
            _interpreter.BeginNewWordCompilation();

            return 1;
        }

        // : word-name body ;
        private int EndNewWordCompilationAction()
        {
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // ( -- a)
        private int FalseAction()
        {
            _interpreter.Pushi(0);

            return 1;
        }

        // ( -- a)
        private int TrueAction()
        {
            _interpreter.Pushi(-1);

            return 1;
        }

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

            // Get the name of the new word.
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

        // [ index-of-a-word-folowing-BEGIN -- ]
        private int RepeatAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("REPEAT outside a new word definition.");
            }

            // REPEAT word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(
                new RepeatControlWord(
                    _interpreter,
                    _interpreter.RPop() - _interpreter.WordBeingDefined.NextWordIndex));

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

        // (-- counter, --)
        private int IndexAction()
        {
            throw new NotImplementedException();

            //_interpreter.Pushi(_interpreter.RPeek());
        }

        //// (--, -- current-src-pos)
        //private void ExecuteWordAction()
        //{
        //    _interpreter.RPush(_interpreter.SourcePos);
        //    _interpreter.GoTo(_interpreter.CurrentWord.SourcePos);
        //}


        //private void ReturnAction()
        //{
        //    _interpreter.GoTo(_interpreter.RPop());
        //}
    }
}
