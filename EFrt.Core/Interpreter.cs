/* EFrt - (C) 2020 - 2021 Premysl Fara  */

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

        private IWord CurrentWord { get; set; }


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
                                        this.Push(t.IValue);
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

                                        this.Push(v.A);
                                        this.Push(v.B);
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

                                        this.Push(v.A);
                                        this.Push(v.B);
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
