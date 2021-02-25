﻿/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;

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
        public IInputSource InputSource => _inputSource;
        private IOutputWriter _outputWriter;
        public IOutputWriter Output
        {
            get => _outputWriter;
            set => _outputWriter = value ?? new NullWriter();
        }
        public bool IsCompiling => InterpreterState == InterpreterStateCode.Compiling;
        public bool IsExecutionTerminated => InterpreterState == InterpreterStateCode.Breaking || InterpreterState == InterpreterStateCode.Terminating;

        public event EventHandler<InterpreterEventArgs> ExecutingWord;
        public event EventHandler<InterpreterEventArgs> WordExecuted;


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
            State.SetStateValue(true);
        }


        public void SuspendNewWordCompilation()
        {
            // Cannot suspend a compilation, when not compiling.
            if (IsCompiling == false)
            {
                throw new Exception("Not in a new word compilation.");
            }

            InterpreterState = InterpreterStateCode.SuspendingCompilation;
            State.SetStateValue(false);
        }


        public void ResumeNewWordCompilation()
        {
            // Cannot resume a compilation, when not suspended.
            if (InterpreterState != InterpreterStateCode.SuspendingCompilation)
            {
                throw new Exception("The new word compilation is not suspended.");
            }

            InterpreterState = InterpreterStateCode.Compiling;
            State.SetStateValue(true);
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
                this.AddWord(WordBeingDefined);
            }

            // Finish this word compilation.
            InterpreterState = InterpreterStateCode.Interpreting;
            State.SetStateValue(false);
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
            State.FloatingPointStack.Clear();
            State.ObjectStack.Clear();

            // TODO: Clear the heap?

            Quit();
        }


        public void Quit()
        {
            State.ReturnStack.Clear();

            InterpreterState = InterpreterStateCode.Breaking;
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

            // Will be caught by the CATCH word.
            throw new InterpreterException(exceptionCode, message);
        }


        private InputSource _inputSource = null;
        
        
        public void Execute(ISourceReader sourceReader)
        {
            if (sourceReader == null) throw new ArgumentNullException(nameof(sourceReader));

            _inputSource = new InputSource(sourceReader);
            _inputSource.NextChar();

            var tok = _inputSource.NextTok();
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
                                try
                                {
                                    // We are executing the current latest version of the found word.
                                    Execute(word);
                                }
                                catch (InterpreterException ex)
                                {
                                    Throw(ex.ExceptionCode, ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    Throw(-100, ex.Message);
                                }
                            }
                            else
                            {
                                // We are adding the RuntimeWord here, because we want to use the latest word definition at runtime.
                                WordBeingDefined.AddWord(word);
                            }
                        }
                        else if (string.Compare(WordBeingDefined.Name, wordName, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            // Recursive call of the currently compiled word.
                            WordBeingDefined.AddWord(WordBeingDefined);
                        }
                        else
                        {
                            // An unknown word can be a number.
                            var t = _inputSource.ParseNumber(tok.SValue);
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

                            try
                            {
                                // We are executing the current latest version of the found word.
                                Execute(CurrentWord);
                            }
                            catch (InterpreterException ex)
                            {
                                Throw(ex.ExceptionCode, ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Throw(-100, ex.Message);
                            }
                        }
                        else
                        {
                            // An unknown word can be a number.
                            var t = _inputSource.ParseNumber(tok.SValue);
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
                    Throw(-200, $"Unknown token {tok.Code}.");
                }

                // Finish program execution, when requested.
                if (IsExecutionTerminated) break;

                // Extract the next token.
                tok = _inputSource.NextTok();
            }

            // Execution broken. Return to interpreting mode.
            if (InterpreterState == InterpreterStateCode.Breaking)
            {
                InterpreterState = InterpreterStateCode.Interpreting;
                State.SetStateValue(false);
            }
        }


        public int Execute(IWord word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));

            ExecutingWord?.Invoke(this, new InterpreterEventArgs() { Word = word });

            try
            {
                return word.Action();
            }
            finally
            {
                WordExecuted?.Invoke(this, new InterpreterEventArgs() { Word = word });
            }
        }


        public void TerminateExecution()
        {
            InterpreterState = InterpreterStateCode.Terminating;
        }

        #endregion
    }
}
