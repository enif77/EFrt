/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    using System;
    using System.Text;

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

        private IOutputWriter _outputWriter;

        public IOutputWriter Output
        {
            get
            {
                return _outputWriter;
            }

            set
            {
                _outputWriter = value ?? new NullWriter();
            }
        }

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
            Output = new NullWriter();

            Reset();
        }


        #region parsing

        private Tokenizer _tokenizer;
        public char CurrentChar => _tokenizer.CurrentChar;
        public int SourcePos => _tokenizer.SourcePos;


        public char NextChar()
        {
            return _tokenizer.NextChar();
        }


        public string GetWordName(bool toUpperCase = true)
        {
            // Get the name of the new word.
            var tok = _tokenizer.NextTok();
            switch (tok.Code)
            {
                case TokenType.Eof:
                    throw new Exception("A name of a word expected.");

                case TokenType.Word:
                    return tok.SValue.ToUpperInvariant();

                default:
                    throw new Exception($"Unexpected token type ({tok}) instead of a word name.");
            }
        }


        public string GetTerminatedString(char terminator)
        {
            var sb = new StringBuilder();

            var c = NextChar();
            while (CurrentChar != 0)
            {
                if (CurrentChar == terminator)
                {
                    NextChar();

                    break;
                }

                sb.Append(CurrentChar);

                c = NextChar();
            }

            if (c != terminator)
            {
                throw new Exception($"'{terminator}' expected.");
            }

            if (CurrentChar != 0 && Tokenizer.IsWhite(_tokenizer.CurrentChar) == false)
            {
                throw new Exception("The EOF or an white character after a string terminator expected.");
            }

            return sb.ToString();
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
            State.WordsList.AddWord(word);
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


        public void BeginNewWordCompilation()
        {
            // Cannot start a compilation, when already compiling.
            if (IsCompiling)
            {
                throw new Exception("A word compilation is already running.");
            }

            InterpreterState = InterpreterStateCode.Compiling;
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


        public long DPick(int index)
        {
            return new DoubleCellIntegerValue()
            {
                A = Pick(index * 2),
                B = Pick(index * 2 + 2),
            }.D;
        }


        public long DPeek()
        {
            return new DoubleCellIntegerValue()
            {
                B = Pick(1),
                A = Peek(),
            }.D;
        }


        public long DPop()
        {
            return new DoubleCellIntegerValue()
            {
                B = Pop(),
                A = Pop(),
            }.D;
        }


        public void DPush(long value)
        {
            var v = new DoubleCellIntegerValue()
            {
                D = value
            };

            Push(v.A);
            Push(v.B);
        }


        public double FPick(int index)
        {
            return new FloatingPointValue()
            {
                A = Pick(index * 2),
                B = Pick(index * 2 + 2),
            }.D;
        }


        public double FPeek()
        {
            return new FloatingPointValue()
            {
                B = Pick(1),
                A = Peek(),
            }.D;
        }


        public double FPop()
        {
            return new FloatingPointValue()
            {
                B = Pop(),
                A = Pop(),
            }.D;
        }


        public void FPush(double value)
        {
            var v = new FloatingPointValue()
            {
                D = value
            };

            Push(v.A);
            Push(v.B);
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


        #region stack functions

        public void Function(Func<int, int> func)
        {
            var stack = State.Stack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top]);
        }


        public void Function(Func<int, int, int> func)
        {
            var stack = State.Stack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);
        }


        public void DFunction(Func<long, int> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[stack.Top] = func(stack.Items[top]);

            Push(func(DPop()));
        }


        public void DFunction(Func<long, long> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[stack.Top] = func(stack.Items[top]);

            DPush(func(DPop()));
        }


        public void DFunction(Func<long, long, int> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);

            var b = DPop();
            Push(func(DPop(), b));
        }


        public void DFunction(Func<long, long, long> func)
        {
            //var stack = _interpreter.Stack;
            //var top = stack.Top;
            //stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);

            var b = DPop();
            DPush(func(DPop(), b));
        }


        public void FFunction(Func<double, double> func)
        {
            //var stack = _interpreter.FloatingPointStack;
            //var top = stack.Top;
            //stack.Items[stack.Top] = func(stack.Items[top]);

            FPush(func(FPop()));
        }


        public void FFunction(Func<double, double, double> func)
        {
            //var stack = _interpreter.FloatingPointStack;
            //var top = stack.Top;
            //stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);

            var b = FPop();
            FPush(func(FPop(), b));
        }


        public void SFunction(Func<string, string> func)
        {
            var stack = State.ObjectStack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top].ToString());
        }


        public void SFunction(Func<string, string, string> func)
        {
            var stack = State.ObjectStack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1].ToString(), stack.Items[top].ToString());
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
                        else if (IsCompiling && string.Compare(WordBeingDefined.Name, wordName, true) == 0)
                        {
                            // Recursive call of the currently compiled word.
                            WordBeingDefined.AddWord(new RuntimeWord(this, wordName));
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
                                        var v = new DoubleCellIntegerValue() { D = t.LValue };

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
                                        var v = new FloatingPointValue() { D = t.FValue };

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
