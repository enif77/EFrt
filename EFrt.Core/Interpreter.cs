/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    using System;

    using EFrt.Core.Values;
    using EFrt.Core.Words;

    using static EFrt.Core.Token;


    /*

    https://www.root.cz/serialy/programovaci-jazyk-forth/
    https://www.forth.com/starting-forth/
    https://en.wikipedia.org/wiki/Forth_(programming_language)
    https://www.fourmilab.ch/atlast/atlast.html
    http://users.ece.cmu.edu/~koopman/stack_computers/

    https://csharppedia.com/en/tutorial/5626/how-to-use-csharp-structs-to-create-a-union-type-similar-to-c-unions-

    */


    public class Interpreter : IInterpreter
    {
        public InterpreterStateCode InterpreterState { get; private set; }

        public IInterpreterState State { get; }

        public bool IsCompiling => InterpreterState == InterpreterStateCode.Compiling;

        public bool IsExecutionTerminated => InterpreterState == InterpreterStateCode.Breaking || InterpreterState == InterpreterStateCode.Terminating;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="state">A IInterpreterState instance.</param>
        public Interpreter(IInterpreterState state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));

            State = state;

            Reset();
        }


        #region tokenizer

        private Tokenizer _tokenizer;
        public char CurrentChar => _tokenizer.CurrentChar;
        public int SourcePos => _tokenizer.SourcePos;


        public char NextChar()
        {
            return _tokenizer.NextChar();
        }


        public bool IsWhite()
        {
            return Tokenizer.IsWhite(_tokenizer.CurrentChar);
        }


        public bool IsDigit()
        {
            return Tokenizer.IsDigit(_tokenizer.CurrentChar);
        }


        public void SkipWhite()
        {
            _tokenizer.SkipWhite();
        }


        public Token NextTok()
        {
            return _tokenizer.NextTok();
        }

        #endregion


        #region words list

        public IWord CurrentWord { get; private set; }


        public bool IsWordDefined(string wordName)
        {
            return State.WordsList.IsWordDefined(wordName.ToUpperInvariant());
        }


        public IWord GetWord(string wordName)
        {
            return State.WordsList.GetWord(wordName.ToUpperInvariant());
        }


        public void AddWord(IWord word)
        {
            State.WordsList.RegisterWord(word);
        }


        public void AddWords(IWordsLIbrary library)
        {
            if (library == null) throw new ArgumentNullException(nameof(library));

            library.DefineWords();
        }


        public void ForgetWord(string wordName)
        {
            State.WordsList.Forget(wordName.ToUpperInvariant());
        }


        public void RemoveWord(string wordName)
        {
            State.WordsList.RemoveWord(wordName.ToUpperInvariant());
        }


        public void RemoveAllWords()
        {
            State.WordsList.Clear();
        }

        #endregion


        #region word compilation

        public NonPrimitiveWord WordBeingDefined { get; set; }


        public string BeginNewWordCompilation()
        {
            // Cannot start a compilation, when already compiling.
            if (IsCompiling)
            {
                throw new Exception("A word compilation is already running.");
            }

            // Get the name of the new word.
            var tok = NextTok();
            switch (tok.Code)
            {
                case TokenType.Eof:
                    throw new Exception($"A name of a new word expected.");

                // Start the new word definition compilation.
                case TokenType.Word:
                    InterpreterState = InterpreterStateCode.Compiling;
                    return tok.SValue.ToUpperInvariant();

                default:
                    throw new Exception($"Unknown token type ({tok}) in a new word definition.");
            }
        }


        public void EndNewWordCompilation()
        {
            // Cannot end a new word compilation, if not compiling.
            if (IsCompiling == false)
            {
                throw new Exception("Not in a new word compilation.");
            }

            // If a new word has a non-primitive body.
            if (WordBeingDefined != null)
            {
                // Now add the new word to the dictionary
                AddWord(WordBeingDefined);
                WordBeingDefined = null;
            }

            // Finish this word compilation.
            InterpreterState = InterpreterStateCode.Interpreting;
        }

        #endregion


        #region stacks

        // Data stack.

        public int Pick(int index)
        {
            return State.Stack.Pick(index);
        }


        public int Peek()
        {
            return State.Stack.Peek();
        }


        public int Pop()
        {
            return State.Stack.Pop();
        }


        public void Push(int value)
        {
            State.Stack.Push(value);
        }


        public void Drop(int count = 1)
        {
            State.Stack.Drop(count);
        }


        public void Dup()
        {
            State.Stack.Dup();
        }


        public void Swap()
        {
            State.Stack.Swap();
        }


        public void Over()
        {
            State.Stack.Over();
        }


        public void Rot()
        {
            State.Stack.Rot();
        }


        public void Roll(int index)
        {
            State.Stack.Roll(index);
        }

        // Object stack.

        public object OPick(int index)
        {
            return State.ObjectStack.Pick(index);
        }


        public object OPeek()
        {
            return State.ObjectStack.Peek();
        }


        public object OPop()
        {
            return State.ObjectStack.Pop();
        }


        public void OPush(object value)
        {
            State.ObjectStack.Push(value);
        }


        public void ODrop(int count = 1)
        {
            State.ObjectStack.Drop();
        }


        public void ODup()
        {
            State.ObjectStack.Dup();
        }


        public void OSwap()
        {
            State.ObjectStack.Swap();
        }


        public void OOver()
        {
            State.ObjectStack.Over();
        }


        public void ORot()
        {
            State.ObjectStack.Rot();
        }

        public void ORoll(int index)
        {
            State.ObjectStack.Roll(index);
        }

        // Return stack.

        public int RPick(int index)
        {
            return State.ReturnStack.Pick(index);
        }


        public int RPeek()
        {
            return State.ReturnStack.Peek();
        }


        public int RPop()
        {
            return State.ReturnStack.Pop();
        }


        public void RPush(int value)
        {
            State.ReturnStack.Push(value);
        }


        public void RDrop(int count = 1)
        {
            State.ReturnStack.Drop();
        }


        public void RDup()
        {
            State.ReturnStack.Dup();
        }

        #endregion


        #region execution

        public void Reset()
        {
            State.Reset();

            InterpreterState = InterpreterStateCode.Interpreting;
        }


        public void Execute(string src)
        {
            Execute(new StringSourceReader(src));
        }


        public void Execute(ISourceReader sourceReader)
        {
            _tokenizer = new Tokenizer(sourceReader);
            _tokenizer.NextChar();

            var tok = _tokenizer.NextTok();
            while (tok.Code >= 0)
            {
                switch (tok.Code)
                {
                    // A word can be known or a number.
                    case TokenType.Word:
                        var wordName = tok.SValue.ToUpperInvariant();
                        if (State.WordsList.IsWordDefined(wordName))
                        {
                            CurrentWord = State.WordsList.GetWord(wordName);

                            if (IsCompiling && CurrentWord.IsImmediate == false)
                            {
                                WordBeingDefined.AddWord(new RuntimeWord(this, wordName));
                            }
                            else
                            {
                                CurrentWord.Action();
                            }
                        }
                        else
                        {
                            // An unknown word can be a number.
                            var t = _tokenizer.ParseNumber(tok.SValue);
                            switch (t.Code)
                            {
                                case TokenType.SingleCellInteger:
                                    if (IsCompiling)
                                    {
                                        WordBeingDefined.AddWord(new SingleCellIntegerLiteralWord(this, t.IValue));
                                    }
                                    else
                                    {
                                        Push(t.IValue);
                                    }
                                    break;

                                case TokenType.DoubleCellInteger:
                                    if (IsCompiling)
                                    {
                                        WordBeingDefined.AddWord(new DoubleCellIntegerLiteralWord(this, t.LValue));
                                    }
                                    else
                                    {
                                        var v = new LongVal() { D = t.LValue };

                                        Push(v.A);
                                        Push(v.B);
                                    }
                                    break;

                                case TokenType.Float:
                                    if (IsCompiling)
                                    {
                                        WordBeingDefined.AddWord(new FloatingPointLiteralWord(this, t.FValue));
                                    }
                                    else
                                    {
                                        var v = new DoubleVal() { D = t.FValue };

                                        Push(v.A);
                                        Push(v.B);
                                    }
                                    break;

                                // No, it is some unknown word.
                                default:
                                    // End this word compiling.
                                    InterpreterState = InterpreterStateCode.Interpreting;   // TODO: Just break here.

                                    throw new Exception($"Unknown word '{tok.SValue}' canot be executed.");
                            }
                        }
                        break;

                    default:
                        throw new Exception($"Unknown token in a word execution.");
                }

                // Finish program execution, when requested.
                if (IsExecutionTerminated) break;

                tok = _tokenizer.NextTok();
            }
        }


        public void BreakExecution()
        {
            InterpreterState = InterpreterStateCode.Breaking;
        }


        public void TerminateExecution()
        {
            InterpreterState = InterpreterStateCode.Terminating;
        }

        #endregion
    }
}
