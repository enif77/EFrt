/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;

    using EFrt.Words;

    using static EFrt.Token;
    

    public class BaseLib
    {
        private EfrtExecutor _efrt;


        public BaseLib(EfrtExecutor efrt)
        {
            _efrt = efrt;
        }

        public void DefineWords()
        {
            _efrt.AddWord(new PrimitiveWord(_efrt, ":", true, DefineAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, ";", true, ReturnAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "+", true, AddAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "-", true, SubAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "*", true, MulAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "/", true, DivAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "MOD", true, ModAction));

            _efrt.AddWord(new PrimitiveWord(_efrt, "=", true, IsEqAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "<>", true, IsNeqAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "<", true, IsLtAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, ">", true, IsGtAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "0=", true, IsZeroAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "0<", true, IsNegAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "0>", true, IsPosAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "AND", true, AndAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "OR", true, OrAction));

            _efrt.AddWord(new PrimitiveWord(_efrt, "DUP", true, DupAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "?DUP", true, DupPosAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "DROP", true, DropAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "SWAP", true, SwapAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "OVER", true, OverAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "ROT", true, RotAction));

            _efrt.AddWord(new PrimitiveWord(_efrt, ".", true, WriteAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, ".(", true, WriteStringAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "CR", true, WriteLineAction));

            _efrt.AddWord(new PrimitiveWord(_efrt, "(", true, CommentAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "\\", true, CommentLineAction));

            _efrt.AddWord(new PrimitiveWord(_efrt, "IF", true, IfAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "ELSE", true, ElseAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "THEN", true, ThenAction));

            _efrt.AddWord(new PrimitiveWord(_efrt, "DO", true, DoAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "?DO", true, IfDoAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "LOOP", true, LoopAction));
            _efrt.AddWord(new PrimitiveWord(_efrt, "I", true, IndexAction));
        }

        // (a b -- result)
        private void AddAction()
        {
            _efrt.Function((a, b) => new EfrtValue(a.Int + b.Int));
        }

        // (a b -- result)
        private void SubAction()
        {
            _efrt.Function((a, b) => new EfrtValue(a.Int - b.Int));
        }

        // (a b -- result)
        private void MulAction()
        {
            _efrt.Function((a, b) => new EfrtValue(a.Int * b.Int));
        }

        // (a b -- result)
        private void DivAction()
        {
            _efrt.Function((a, b) => new EfrtValue(a.Int / b.Int));
        }

        // (a b -- result)
        private void ModAction()
        {
            _efrt.Function((a, b) => new EfrtValue(a.Int % b.Int));
        }

        // (a b -- result)
        private void IsEqAction()
        {
            _efrt.Function((a, b) => new EfrtValue((a.Int == b.Int) ? -1 : 0));
        }

        // (a b -- result)
        private void IsNeqAction()
        {
            _efrt.Function((a, b) => new EfrtValue((a.Int != b.Int) ? -1 : 0));
        }

        // (a b -- result)
        private void IsLtAction()
        {
            _efrt.Function((a, b) => new EfrtValue((a.Int < b.Int) ? -1 : 0));
        }

        // (a b -- result)
        private void IsGtAction()
        {
            _efrt.Function((a, b) => new EfrtValue((a.Int > b.Int) ? -1 : 0));
        }

        // (a -- result)
        private void IsZeroAction()
        {
            _efrt.Function((a) => new EfrtValue((a.Int == 0) ? -1 : 0));
        }

        // (a -- result)
        private void IsNegAction()
        {
            _efrt.Function((a) => new EfrtValue((a.Int < 0) ? -1 : 0));
        }

        // (a b -- result)
        private void AndAction()
        {
            _efrt.Function((a, b) => new EfrtValue((a.Int != 0 && b.Int != 0) ? -1 : 0));
        }

        // (a b -- result)
        private void OrAction()
        {
            _efrt.Function((a, b) => new EfrtValue((a.Int != 0 || b.Int != 0) ? -1 : 0));
        }

        // (a -- result)
        private void IsPosAction()
        {
            _efrt.Function((a) => new EfrtValue((a.Int > 0) ? -1 : 0));
        }

        // (a -- a a)
        private void DupAction()
        {
            _efrt.Stack.Dup();
        }

        // (a -- a [result])
        private void DupPosAction()
        {
            if (_efrt.Stack.Get().Int != 0)
            {
                _efrt.Stack.Dup();
            }
        }

        // (a --)
        private void DropAction()
        {
            _efrt.Stack.Drop();
        }

        // (a b -- b a)
        private void SwapAction()
        {
            _efrt.Stack.Swap();
        }

        // (a b -- a b a)
        private void OverAction()
        {
            _efrt.Stack.Over();
        }

        // (a b c -- b c a)
        private void RotAction()
        {
            _efrt.Stack.Rot();
        }

        // (a --)
        private void WriteAction()
        {
            Console.Write(_efrt.Stack.Pop().Int);
        }

        private void WriteStringAction()
        {
            var c = _efrt.NextChar();
            while (_efrt.CurrentChar != 0)
            {
                if (_efrt.CurrentChar == ')')
                {
                    _efrt.NextChar();

                    break;
                }

                Console.Write(_efrt.CurrentChar);

                c = _efrt.NextChar();
            }

            if (c != ')')
            {
                throw new Exception("')' expected.");
            }
        }

        private void WriteLineAction()
        {
            Console.WriteLine();
        }

        private void CommentAction()
        {
            var c = _efrt.NextChar();
            while (_efrt.CurrentChar != 0)
            {
                if (_efrt.CurrentChar == ')')
                {
                    _efrt.NextChar();

                    break;
                }

                c = _efrt.NextChar();
            }

            if (c != ')')
            {
                throw new Exception("')' expected.");
            }
        }

        private void CommentLineAction()
        {
            _efrt.NextChar();
            while (_efrt.CurrentChar != 0)
            {
                if (_efrt.CurrentChar == '\n')
                {
                    _efrt.NextChar();

                    break;
                }

                _efrt.NextChar();
            }
        }

        // : word-name body ;
        private void DefineAction()
        {
            var tok = _efrt.NextTok();
            switch (tok.Code)
            {
                case TokenType.Eof: throw new Exception($"A new word-name expected.");
                case TokenType.Word:
                    // Old word definition removed.
                    _efrt.RemoveWord(tok.SValue);

                    // New word definition started.
                    tok = Token.CreateWordToken(tok.SValue);
                    break;
                case TokenType.Integer: throw new Exception($"The new word-name '{tok.IValue}' is a number.");
                default:
                    throw new Exception($"Unknown token type ({tok}) in a word definition.");
            }

            var word = tok.SValue;
            var wordStart = _efrt.SourcePos;

            var level = 1;
            var c = _efrt.CurrentChar;
            while (_efrt.CurrentChar != 0)
            {
                if (_efrt.CurrentChar == ':')
                {
                    level++;
                }

                if (_efrt.CurrentChar == ';')
                {
                    _efrt.NextChar();

                    level--;
                    if (level == 0)
                    {
                        break;
                    }
                }

                c = _efrt.NextChar();
            }

            if (c != ';' || level != 0)
            {
                throw new Exception("';' expected.");
            }

            _efrt.AddWord(new PrimitiveWord(_efrt, word, true, ExecuteWordAction, wordStart));
        }

        // IF ... ELSE .. THEN
        // ( flag -- )
        private void IfAction()
        {
            // if top = 0 skip to then
            // if top = 0 skip to else and exetute till then

            // if top != 0 execute till then
            // if top != 0 execute till else and skip till then

            _efrt.BranchLevel++;
            var thisBranchLevel = _efrt.BranchLevel;
            if (_efrt.Stack.Pop().Int == 0)
            {
                //  Skip to THEN or ELSE.
                var tok = _efrt.NextTok();
                while (tok.Code >= 0)
                {
                    if (tok.Code == TokenType.Word)
                    {
                        if (tok.SValue == "IF")
                        {
                            // Entering a nested IF-THEN block.
                            _efrt.BranchLevel++;
                        }
                        else if (tok.SValue == "ELSE")
                        {
                            // Nested ELSE blocks are skipped. 
                            // Is this is "our" level ELSE?
                            if (_efrt.BranchLevel == thisBranchLevel)
                            {
                                // IF part skipped, continue execution with this ELSE part.
                                break;
                            }
                        }
                        else if (tok.SValue == "THEN")
                        {
                            // Leaving a IF-THEN block.
                            _efrt.BranchLevel--;
                            if (_efrt.BranchLevel < thisBranchLevel)
                            {
                                // We left "our" IF-THEN block, continuing with execution.
                                break;
                            }
                        }
                    }

                    tok = _efrt.NextTok();
                }

                if (tok.Code != TokenType.Word && (tok.SValue != "THEN" && tok.SValue != "ELSE"))
                {
                    throw new Exception("ELSE or THEN expected.");
                }
            }

            // If stack[top] was true, we are executing the IF part.
        }

        private void ElseAction()
        {
            // Skip till THEN.
            var thisBranchLevel = _efrt.BranchLevel;
            var tok = _efrt.NextTok();
            while (tok.Code >= 0)
            {
                if (tok.Code == TokenType.Word)
                {
                    if (tok.SValue == "IF")
                    {
                        // Entering a nested IF-THEN block.
                        _efrt.BranchLevel++;
                    }
                    else if (tok.SValue == "THEN")
                    {
                        // Leaving a IF-THEN block.
                        _efrt.BranchLevel--;
                        if (_efrt.BranchLevel < thisBranchLevel)
                        {
                            // We left "our" IF-THEN block, continuing with execution.
                            break;
                        }
                    }
                }

                tok = _efrt.NextTok();
            }

            if (tok.Code != TokenType.Word && tok.SValue != "THEN")
            {
                throw new Exception("THEN expected.");
            }
        }

        private void ThenAction()
        {
            // Exit the current IF-THEN block.
            _efrt.BranchLevel--;
            if (_efrt.BranchLevel < 0)
            {
                throw new Exception("IF expected.");
            }
        }


        // (counter-end counter-start -- counter-end, -- loop-start counter)
        private void DoAction()
        {
            _efrt.ReturnStack.Push(_efrt.SourcePos);
            _efrt.ReturnStack.Push(_efrt.Stack.Pop().Int);
        }

        // (counter-end counter-start -- counter-end, -- loop-start counter)
        private void IfDoAction()
        {
            throw new NotImplementedException();
        }

        private void LoopAction()
        {
            // Get the current counter value and increase it's value.
            var counter = _efrt.ReturnStack.Pop() + 1;

            // Do we reached the end of the loop?
            if (counter > _efrt.Stack.Get().Int)
            {
                // Drop counter-end.
                _efrt.Stack.Drop();

                // Drop loop-start.
                _efrt.ReturnStack.Drop();
            }
            else
            {
                // Save actual counter to the return stack.
                _efrt.ReturnStack.Push(counter);

                // Go to the first word behind the DO word.
                _efrt.GoTo(_efrt.ReturnStack.Get(1));
            }
        }

        // (-- counter, --)
        private void IndexAction()
        {
            _efrt.Stack.Push(_efrt.ReturnStack.Get());
        }

        // (--, -- current-src-pos)
        private void ExecuteWordAction()
        {
            _efrt.ReturnStack.Push(_efrt.SourcePos);
            _efrt.GoTo(_efrt.CurrentWord.SourcePos);
        }


        private void ReturnAction()
        {
            _efrt.GoTo(_efrt.ReturnStack.Pop());
        }
    }
}
