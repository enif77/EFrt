/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using System;
    using System.Collections.Generic;

    using EFrt.Libs;
    using EFrt.Stacks;
    using EFrt.Words;

    using static EFrt.Token;


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
        /// <summary>
        /// The main stack for user data.
        /// </summary>
        public DataStack Stack { get; }

        /// <summary>
        /// Optional stack for user data.
        /// </summary>
        public ObjectStack ObjectStack { get; }

        /// <summary>
        /// The support stack for internal interpreters use.
        /// </summary>
        public ReturnStack ReturnStack { get; }

        /// <summary>
        /// The list of known words.
        /// </summary>
        public IWordsList WordsList { get; }

        /// <summary>
        /// Thrue, if this interpreter is currently compiling a new word.
        /// </summary>
        public bool IsCompiling { get; private set; }

        /// <summary>
        /// A flag signaling, that this program execution is termineted.
        /// </summary>
        public bool IsExecutionTerminated { get; private set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordsList">A IWordsList instance.</param>
        /// <param name="stackCapacity">A mains stack capacity. 32 by default.</param>
        /// <param name="returnStackCapacity">A returns stack capacity. 32 by default.</param>
        public Interpreter(IWordsList wordsList, int stackCapacity = 32, int returnStackCapacity = 32)
        {
            Stack = new DataStack(stackCapacity);
            ObjectStack = new ObjectStack(stackCapacity);
            ReturnStack = new ReturnStack(returnStackCapacity);

            WordsList = wordsList;

            Reset();
        }
        

        public void Reset(IEnumerable<IWordsLIbrary> libraries = null)
        {
            Stack.Init(0);
            ReturnStack.Init(0);
            
            if (libraries != null)
            {
                WordsList.Clear();

                foreach (var library in libraries)
                {
                    library.DefineWords();
                }
            }
        }


        public void TerminateExecution()
        {
            IsExecutionTerminated = true;
        }


        #region words

        public IWord CurrentWord { get; private set; }


        public bool IsWordDefined(string wordName)
        {
            return WordsList.IsWordDefined(wordName);
        }


        public IWord GetWord(string wordName)
        {
            return WordsList.GetWord(wordName);
        }


        public void AddWord(IWord word)
        {
            WordsList.RegisterWord(word);
        }


        public void RemoveWord(string wordName)
        {
            WordsList.RemoveWord(wordName);
        }


        public void ForgetWord(string wordName)
        {
            WordsList.Forget(wordName);
        }


        public void DefineWords(IEnumerable<IWordsLIbrary> libraries, bool removeExistingWords = false)
        {
            if (libraries == null) throw new ArgumentNullException(nameof(libraries));

            if (removeExistingWords)
            {
                WordsList.Clear();
            }

            foreach (var library in libraries)
            {
                library.DefineWords();
            }
        }


        public void DefineWords(IWordsLIbrary library, bool removeExistingWords = false)
        {
            if (library == null) throw new ArgumentNullException(nameof(library));

            if (removeExistingWords)
            {
                WordsList.Clear();
            }

            library.DefineWords();
        }

        #endregion


        #region stacks

        // Data stack.

        public int Get(int index)
        {
            return Stack.Get(index);
        }


        public int Peek()
        {
            return Stack.Peek();
        }


        public int Pop()
        {
            return Stack.Pop();
        }


        public void Push(int value)
        {
            Stack.Push(value);
        }


        public void Drop(int count = 1)
        {
            Stack.Drop();
        }


        public void Dup()
        {
            Stack.Dup();
        }


        public void Swap()
        {
            Stack.Swap();
        }


        public void Over()
        {
            Stack.Over();
        }


        public void Rot()
        {
            Stack.Rot();
        }


        public void Function(Func<int, int> func)
        {
            var top = Stack.Top;
            Stack.Items[Stack.Top] = func(Stack.Items[top]);
        }


        public void Function(Func<int, int, int> func)
        {
            var top = Stack.Top;
            Stack.Items[--Stack.Top] = func(Stack.Items[top - 1], Stack.Items[top]);
        }


        // Object stack.

        public object OGet(int index)
        {
            return ObjectStack.Get(index);
        }


        public object OPeek()
        {
            return ObjectStack.Peek();
        }


        public object OPop()
        {
            return ObjectStack.Pop();
        }


        public void OPush(object value)
        {
            ObjectStack.Push(value);
        }


        public void ODrop(int count = 1)
        {
            ObjectStack.Drop();
        }


        public void ODup()
        {
            ObjectStack.Dup();
        }


        public void OSwap()
        {
            ObjectStack.Swap();
        }


        public void OOver()
        {
            ObjectStack.Over();
        }


        public void ORot()
        {
            ObjectStack.Rot();
        }


        public void Function(Func<object, object> func)
        {
            var top = ObjectStack.Top;
            ObjectStack.Items[ObjectStack.Top] = func(ObjectStack.Items[top]);
        }


        public void Function(Func<object, object, object> func)
        {
            var top = ObjectStack.Top;
            ObjectStack.Items[--ObjectStack.Top] = func(ObjectStack.Items[top - 1], ObjectStack.Items[top]);
        }


        // Return stack.

        public int RGet(int index)
        {
            return ReturnStack.Get(index);
        }


        public int RPeek()
        {
            return ReturnStack.Peek();
        }


        public int RPop()
        {
            return ReturnStack.Pop();
        }


        public void RPush(int value)
        {
            ReturnStack.Push(value);
        }


        public void RDrop(int count = 1)
        {
            ReturnStack.Drop();
        }


        public void RDup()
        {
            ReturnStack.Dup();
        }

        #endregion


        #region tokenizer

        public char CurrentChar => Tokenizer.CurrentChar;
        public int SourcePos => Tokenizer.SourcePos;


        public char NextChar()
        {
            return Tokenizer.NextChar();
        }

        public Token NextTok()
        {
            return Tokenizer.NextTok();
        }

        #endregion


        /// <summary>
        /// Returns a word, that we are actually compileing.
        /// </summary>
        public NonPrimitiveWord WordBeingDefined { get; private set; }


        /// <summary>
        /// Begins a new word compilation.
        /// </summary>
        public void BeginNewWordCompilation()
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
                    if (Tokenizer.ParseNumber(tok.SValue).Code != TokenType.Word)
                    {
                        // This word can be parser to a number...
                        throw new Exception($"A name of a new word expected.");
                    }

                    IsCompiling = true;
                    WordBeingDefined = new NonPrimitiveWord(this, tok.SValue);
                    break;

                default:
                    throw new Exception($"Unknown token type ({tok}) in a new word definition.");
            }
        }

        /// <summary>
        /// End a new word compilation and adds it into the known words list.
        /// </summary>
        public void EndNewWordCompilation()
        {
            // Cannot end a new word compilation, if not compiling.
            if (IsCompiling == false)
            {
                throw new Exception("Not in a new word compilation.");
            }

            // Now add the new word to the dictionary
            AddWord(WordBeingDefined);

            // Finish this word compilation.
            IsCompiling = false;
            WordBeingDefined = null;
        }


        public void Execute(string src)
        {
            Execute(new StringSourceReader(src));
        }


        public void Execute(ISourceReader sourceReader)
        {
            Tokenizer = new Tokenizer(sourceReader);
            Tokenizer.NextChar();

            var tok = Tokenizer.NextTok();
            while (tok.Code >= 0)
            {
                switch (tok.Code)
                {
                    // A word can be known or a number.
                    case TokenType.Word:
                        if (WordsList.IsWordDefined(tok.SValue))
                        {
                            CurrentWord = WordsList.GetWord(tok.SValue);

                            if (IsCompiling && CurrentWord.IsImmediate == false)
                            {
                                WordBeingDefined.AddWord(new RuntimeWord(this, tok.SValue));
                            }
                            else
                            {
                                CurrentWord.Action();
                            }
                        }
                        else
                        {
                            // An unknown word can be a number.
                            var t = Tokenizer.ParseNumber(tok.SValue);
                            switch (t.Code)
                            {
                                case TokenType.Integer:
                                    if (IsCompiling)
                                    {
                                        WordBeingDefined.AddWord(new ValueWord(this, t.IValue));
                                    }
                                    else
                                    {
                                        Push(t.IValue);
                                    }
                                    break;

                                //case TokenType.Float:
                                //    if (IsCompiling)
                                //    {
                                //        WordBeingDefined.AddWord(new ValueWord(this, new StackValue(t.FValue)));
                                //    }
                                //    else
                                //    {
                                //        Pushf(t.FValue);
                                //    }
                                //    break;

                                // No, it is some unknown word.
                                default:
                                    // End this word compiling.
                                    IsCompiling = false;

                                    throw new Exception($"Unknown word '{tok.SValue}' canot be executed.");
                            }
                        }
                        break;

                    default:
                        throw new Exception($"Unknown token in a word execution.");
                }

                // Finish program execution, when requested.
                if (IsExecutionTerminated) break;

                tok = Tokenizer.NextTok();
            }
        }


        private Tokenizer Tokenizer { get; set; }
    }
}
