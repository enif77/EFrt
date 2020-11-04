﻿/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;

    using EFrt.Words;

    //using static EFrt.Token;
    

    public class BaseLib
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


            // DO I J LEAVE LOOP +LOOP 

            // FORGET CONSTANT VARIABLE ! @ ARRAY WORDS WORDSD IF THEN ELSE 
            // ' EXECUTE INT FLOAT STRING

            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "DO", false, DoAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "?DO", false, IfDoAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "LOOP", false, LoopAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "I", false, IndexAction));
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


        // IF ... ELSE .. THEN
        // ( flag -- )
        private int IfAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("IF outside a new word definition.");
            }

            var ifcw = new IfControlWord(_interpreter, _interpreter.WordBeingDefined.NextWordIndex);
            _interpreter.WordBeingDefined.AddWord(ifcw);
            _interpreter.CPush(ifcw);

            return 1;
        }


        private int ElseAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("ELSE outside a new word definition.");
            }

            var ifControlWord = _interpreter.CPeek();
            if (ifControlWord is IfControlWord) 
			{
                // Get the index past where ELSE will be.
                var indexFolowingElse = _interpreter.WordBeingDefined.NextWordIndex + 1;

                // Instantiate the ELSE runtime code passing the index following ELSE.
                var ecw = new ElseControlWord(_interpreter, indexFolowingElse);
                _interpreter.WordBeingDefined.AddWord(ecw);

                // Push execute address of ELSE word onto control flow stack.
                _interpreter.CPush(ecw);

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

            var controlWord = _interpreter.CPop();
            if (controlWord is ElseControlWord) 
			{
                // We had a previous else 
                ((ElseControlWord)controlWord).SetThenIndexIncrement(thenIndex);

                // Pop control stack again to find IF.
                controlWord = _interpreter.CPop();
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


        private int BeginAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("BEGIN outside a new word definition.");
            }

            // BEGIN word doesn't have runtime behavior.

            _interpreter.CPush(new BeginControlWord(_interpreter, _interpreter.WordBeingDefined.NextWordIndex));

            return 1;
        }


        private int RepeatAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("REPEAT outside a new word definition.");
            }

            // REPEAT word doesn't have runtime behavior.

            var controlWord = _interpreter.CPop();
            if (controlWord is BeginControlWord)
            {
                // We had a previous BEGIN. 
                _interpreter.WordBeingDefined.AddWord(
                    new RepeatControlWord(
                        _interpreter, 
                        ((BeginControlWord)controlWord).IndexFollowingBegin - _interpreter.WordBeingDefined.NextWordIndex));
            }
            else
            {
                throw new Exception("REPEAT requires a previous BEGIN.");
            }

            return 1;
        }
               


        // (counter-end counter-start -- counter-end, -- loop-start counter)
        private int DoAction()
        {
            throw new NotImplementedException();

            //_interpreter.RPush(_interpreter.SourcePos);
            //_interpreter.RPush(_interpreter.Pop().Int);
        }

        // (counter-end counter-start -- counter-end, -- loop-start counter)
        private int IfDoAction()
        {
            throw new NotImplementedException();
        }

        private int LoopAction()
        {
            throw new NotImplementedException();

            //// Get the current counter value and increase it's value.
            //var counter = _interpreter.RPop() + 1;

            //// Do we reached the end of the loop?
            //if (counter > _interpreter.Peek().Int)
            //{
            //    // Drop counter-end.
            //    _interpreter.Drop();

            //    // Drop loop-start.
            //    _interpreter.RDrop();
            //}
            //else
            //{
            //    // Save actual counter to the return stack.
            //    _interpreter.RPush(counter);

            //    // Go to the first word behind the DO word.
            //    _interpreter.GoTo(_interpreter.RGet(1));
            //}
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
