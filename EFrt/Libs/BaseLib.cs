/* EFrt - (C) 2020 Premysl Fara  */

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
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "?DUP", false, DupPosAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "DROP", false, DropAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "SWAP", false, SwapAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OVER", false, OverAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ROT", false, RotAction));


            // FORGET CONSTANT VARIABLE ! @ ARRAY WORDS WORDSD IF THEN ELSE 
            // DO I J LEAVE LOOP +LOOP BEGIN END 2DROP 2DUP 2SWAP 2OVER 2ROT -ROT
            // DEPTH >R R> R@ ' EXECUTE TRUE FALSE INT FLOAT STRING BYE


            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "IF", false, IfAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "ELSE", false, ElseAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "THEN", false, ThenAction));

            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "DO", false, DoAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "?DO", false, IfDoAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "LOOP", false, LoopAction));
            //_interpreter.AddWord(new PrimitiveWord(_interpreter, "I", false, IndexAction));
        }

        // (a -- a a)
        private void DupAction()
        {
            _interpreter.Dup();
        }

        // (a -- a [result])
        private void DupPosAction()
        {
            if (_interpreter.Peek().Int != 0)
            {
                _interpreter.Dup();
            }
        }

        // (a --)
        private void DropAction()
        {
            _interpreter.Drop();
        }

        // (a b -- b a)
        private void SwapAction()
        {
            _interpreter.Swap();
        }

        // (a b -- a b a)
        private void OverAction()
        {
            _interpreter.Over();
        }

        // (a b c -- b c a)
        private void RotAction()
        {
            _interpreter.Rot();
        }


        private void CommentAction()
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
        }


        private void CommentLineAction()
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
        }


        // : word-name body ;
        private void BeginNewWordCompilationAction()
        {
            _interpreter.BeginNewWordCompilation();
        }


        // : word-name body ;
        private void EndNewWordCompilationAction()
        {
            _interpreter.EndNewWordCompilation();
        }


        // IF ... ELSE .. THEN
        // ( flag -- )
        private void IfAction()
        {
            throw new NotImplementedException();

            //// if top = 0 skip to then
            //// if top = 0 skip to else and exetute till then

            //// if top != 0 execute till then
            //// if top != 0 execute till else and skip till then

            //_interpreter.BranchLevel++;
            //var thisBranchLevel = _interpreter.BranchLevel;
            //if (_interpreter.Pop().Int == 0)
            //{
            //    //  Skip to THEN or ELSE.
            //    var tok = _interpreter.NextTok();
            //    while (tok.Code >= 0)
            //    {
            //        if (tok.Code == TokenType.Word)
            //        {
            //            if (tok.SValue == "IF")
            //            {
            //                // Entering a nested IF-THEN block.
            //                _interpreter.BranchLevel++;
            //            }
            //            else if (tok.SValue == "ELSE")
            //            {
            //                // Nested ELSE blocks are skipped. 
            //                // Is this is "our" level ELSE?
            //                if (_interpreter.BranchLevel == thisBranchLevel)
            //                {
            //                    // IF part skipped, continue execution with this ELSE part.
            //                    break;
            //                }
            //            }
            //            else if (tok.SValue == "THEN")
            //            {
            //                // Leaving a IF-THEN block.
            //                _interpreter.BranchLevel--;
            //                if (_interpreter.BranchLevel < thisBranchLevel)
            //                {
            //                    // We left "our" IF-THEN block, continuing with execution.
            //                    break;
            //                }
            //            }
            //        }

            //        tok = _interpreter.NextTok();
            //    }

            //    if (tok.Code != TokenType.Word && (tok.SValue != "THEN" && tok.SValue != "ELSE"))
            //    {
            //        throw new Exception("ELSE or THEN expected.");
            //    }
            //}

            //// If stack[top] was true, we are executing the IF part.
        }

        private void ElseAction()
        {
            throw new NotImplementedException();

            //// Skip till THEN.
            //var thisBranchLevel = _interpreter.BranchLevel;
            //var tok = _interpreter.NextTok();
            //while (tok.Code >= 0)
            //{
            //    if (tok.Code == TokenType.Word)
            //    {
            //        if (tok.SValue == "IF")
            //        {
            //            // Entering a nested IF-THEN block.
            //            _interpreter.BranchLevel++;
            //        }
            //        else if (tok.SValue == "THEN")
            //        {
            //            // Leaving a IF-THEN block.
            //            _interpreter.BranchLevel--;
            //            if (_interpreter.BranchLevel < thisBranchLevel)
            //            {
            //                // We left "our" IF-THEN block, continuing with execution.
            //                break;
            //            }
            //        }
            //    }

            //    tok = _interpreter.NextTok();
            //}

            //if (tok.Code != TokenType.Word && tok.SValue != "THEN")
            //{
            //    throw new Exception("THEN expected.");
            //}
        }

        private void ThenAction()
        {
            throw new NotImplementedException();

            //// Exit the current IF-THEN block.
            //_interpreter.BranchLevel--;
            //if (_interpreter.BranchLevel < 0)
            //{
            //    throw new Exception("IF expected.");
            //}
        }


        // (counter-end counter-start -- counter-end, -- loop-start counter)
        private void DoAction()
        {
            throw new NotImplementedException();

            //_interpreter.RPush(_interpreter.SourcePos);
            //_interpreter.RPush(_interpreter.Pop().Int);
        }

        // (counter-end counter-start -- counter-end, -- loop-start counter)
        private void IfDoAction()
        {
            throw new NotImplementedException();
        }

        private void LoopAction()
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
        private void IndexAction()
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
