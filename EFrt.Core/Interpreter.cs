/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;
    using System.Text;

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


        public long ParseNumber(string s, out bool success)
        {
            success = true;

            // An unknown word can be a number.
            var t = _tokenizer.ParseNumber(s, true, true, true);
            switch (t.Code)
            {
                case TokenType.SingleCellInteger: return t.IValue;
                case TokenType.DoubleCellInteger: return t.LValue;
                case TokenType.Float: return (long)t.FValue;

                // No, it is something else.
                default:
                    success = false;
                    return 0;
            }
        }


        public double ParseFloatingPointNumber(string s, out bool success)
        {
            success = true;

            // An unknown word can be a number.
            var t = _tokenizer.ParseNumber(s, true, true, true);
            switch (t.Code)
            {
                case TokenType.SingleCellInteger: return t.IValue;
                case TokenType.DoubleCellInteger: return t.LValue;
                case TokenType.Float: return t.FValue;

                // No, it is something else.
                default:
                    success = false;
                    return 0.0;
            }
        }

        #endregion


        #region words list

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


        public void SuspendNewWordCompilation()
        {
            // Cannot suspend a compilation, when not compiling.
            if (IsCompiling == false)
            {
                throw new Exception("Not in a new word compilation.");
            }

            InterpreterState = InterpreterStateCode.SuspendingCompilation;
        }


        public void ResumeNewWordCompilation()
        {
            // Cannot resume a compilation, when not suspended.
            if (InterpreterState != InterpreterStateCode.SuspendingCompilation)
            {
                throw new Exception("The new word compilation is not suspended.");
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


        #region execution

        /// <summary>
        /// The currently running word.
        /// </summary>
        private IWord CurrentWord { get; set; }


        public void Reset()
        {
            State.Reset();

            InterpreterState = InterpreterStateCode.Interpreting;
        }


        public void Abort()
        {
            State.Stack.Clear();
            State.ObjectStack.Clear();

            // TODO: Clear the heap?

            Quit();
        }


        public void Quit()
        {
            State.ReturnStack.Clear();
            BreakExecution();
        }


        public void Throw(int exceptionCode, string message = null)
        {
            if (exceptionCode == 0)
            {
                return;
            }

            if (State.ExceptionStack.IsEmpty)
            {
                switch (exceptionCode)
                {
                    case -1: break;
                    case -2: Output.WriteLine(message ?? "Execution aborted!"); break;
                    default:
                        Output.WriteLine($"Execution aborted: [{exceptionCode}] {message ?? string.Empty}");
                        break;
                }

                Abort();

                return;
            }

            // Restore the previous execution state.
            var exceptionFrame = State.ExceptionStack.Pop();

            State.Stack.Top = exceptionFrame.StackTop;
            State.ObjectStack.Top = exceptionFrame.ObjectStackTop;
            State.ReturnStack.Top = exceptionFrame.ReturnStackTop;
            CurrentWord = exceptionFrame.ExecutingWord;

            this.Push(exceptionCode);

            // Will be catched by the CATCH word.
            throw new Exception($"[{exceptionCode}] {message ?? string.Empty}");
        }


        public void Execute(ISourceReader sourceReader)
        {
            if (sourceReader == null) throw new ArgumentNullException(nameof(sourceReader));

            _tokenizer = new Tokenizer(sourceReader);
            _tokenizer.NextChar();

            var tok = _tokenizer.NextTok();
            while (tok.Code >= 0)
            {
                if (tok.Code == TokenType.Word)
                {
                    var wordName = tok.SValue.ToUpperInvariant();

                    if (IsCompiling)
                    {
                        if (State.WordsList.IsWordDefined(wordName))
                        {
                            var word = State.WordsList.GetWord(wordName);
                            if (word.IsImmediate)
                            {
                                // We are executing the current latest version of the foung word.
                                word.Action();
                            }
                            else
                            {
                                // We are adding the RuntimeWord here, because we want to use the latest word definition at runtime.
                                WordBeingDefined.AddWord(new RuntimeWord(this, wordName));
                            }
                        }
                        else if (string.Compare(WordBeingDefined.Name, wordName, true) == 0)
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
                                    WordBeingDefined.AddWord(new SingleCellIntegerLiteralWord(this, t.IValue));
                                    break;

                                case TokenType.DoubleCellInteger:
                                    WordBeingDefined.AddWord(new DoubleCellIntegerLiteralWord(this, t.LValue));
                                    break;

                                case TokenType.Float:
                                    WordBeingDefined.AddWord(new FloatingPointLiteralWord(this, t.FValue));
                                    break;

                                // No, it is some unknown word.
                                default:
                                    Throw(-13, $"The '{tok.SValue}' word is undefined.");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (State.WordsList.IsWordDefined(wordName))
                        {
                            CurrentWord = State.WordsList.GetWord(wordName);
                            CurrentWord.Action();
                        }
                        else
                        {
                            // An unknown word can be a number.
                            var t = _tokenizer.ParseNumber(tok.SValue);
                            switch (t.Code)
                            {
                                case TokenType.SingleCellInteger:
                                    this.Push(t.IValue);
                                    break;

                                case TokenType.DoubleCellInteger:
                                    this.DPush(t.LValue);
                                    break;

                                case TokenType.Float:
                                    this.FPush(t.FValue);
                                    break;

                                // No, it is some unknown word.
                                default:
                                    Throw(-13, $"The '{tok.SValue}' word is undefined.");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    //throw new Exception("Unknown token in a word execution.");
                    Throw(-100, $"Unknown token {tok.Code}.");
                }

                // Finish program execution, when requested.
                if (IsExecutionTerminated) break;

                // Extract the next token.
                tok = _tokenizer.NextTok();
            }

            // Execution breaked. Return to interpreting mode.
            if (InterpreterState == InterpreterStateCode.Breaking)
            {
                InterpreterState = InterpreterStateCode.Interpreting;
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
