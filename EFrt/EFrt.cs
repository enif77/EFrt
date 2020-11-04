/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using System;

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


    TODO

        - kompilace
        - řetězce jako v C/C#
        - double (separátní zásobník?)

    ?DO (a b --, -- r b)

    10 10 Do I . CR LOOP   \ Vypíše 10 - smyčka proběhne jednou.
    10 10 ?DO I . CR LOOP  \ Nevypíše nic - smyčka vůbec neproběhne.

    */



    public class EfrtExecutor : IInterpreter
    {
        public bool IsCompiling { get; private set; }

        public bool IsExecutionTerminated { get; private set; }


        public EfrtExecutor(int controlFlowStackCapacity = 32, int stackCapacity = 32, int returnStackCapacity = 32)
        {
            ControlFlowStack = new ControlFlowStack(controlFlowStackCapacity);
            Stack = new DataStack(stackCapacity);
            ReturnStack = new ReturnStack(returnStackCapacity);

            _wordsList = new WordsList();
            _baseLib = new BaseLib(this);
            _integerLib = new IntegerLib(this);
            _floatLib = new FloatLib(this);
            _ioLib = new IoLib(this, new ConsoleWriter());

            Reset();
        }


        public void Reset()
        {
            ControlFlowStack.Init(null);
            Stack.Init(new EfrtValue(0));
            ReturnStack.Init(0);

            _wordsList.Clear();
            _baseLib.DefineWords();
            _integerLib.DefineWords();
            _floatLib.DefineWords();
            _ioLib.DefineWords();
        }


        public void TerminateExecution()
        {
            IsExecutionTerminated = true;
        }


        #region words

        public IWord CurrentWord { get; private set; }


        public bool IsWordDefined(string wordName)
        {
            return _wordsList.IsWordDefined(wordName);
        }


        public IWord GetWord(string wordName)
        {
            return _wordsList.GetWord(wordName);
        }


        public void AddWord(IWord word)
        {
            // Old word definition removed.
            RemoveWord(word.Name);

            // New one added.
            _wordsList.RegisterWord(word);
        }


        public void RemoveWord(string wordName)
        {
            _wordsList.RemoveWord(wordName);
        }

        #endregion


        #region stacks

        private ControlFlowStack ControlFlowStack { get; set; }

        private DataStack Stack { get; set; }

        private ReturnStack ReturnStack { get; set; }


        public IWord CGet(int index)
        {
            return ControlFlowStack.Get(index);
        }


        public IWord CPeek()
        {
            return ControlFlowStack.Peek();
        }


        public IWord CPop()
        {
            return ControlFlowStack.Pop();
        }


        public void CPush(IWord word)
        {
            ControlFlowStack.Push(word);
        }



        public EfrtValue Get(int index)
        {
            return Stack.Get(index);
        }


        public EfrtValue Peek()
        {
            return Stack.Peek();
        }


        public int Peeki()
        {
            return Peek().Int;
        }


        public float Peekf()
        {
            return Peek().Float;
        }


        public EfrtValue Pop()
        {
            return Stack.Pop();
        }


        public int Popi()
        {
            return Pop().Int;
        }


        public float Popf()
        {
            return Pop().Float;
        }


        public void Push(EfrtValue value)
        {
            Stack.Push(value);
        }


        public void Pushi(int i)
        {
            Stack.Push(i);
        }


        public void Pushf(float d)
        {
            Stack.Push(d);
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


        public void Function(Func<EfrtValue, EfrtValue> func)
        {
            var top = Stack.Top;
            Stack.Items[Stack.Top] = func(Stack.Items[top]);
        }


        public void Function(Func<EfrtValue, EfrtValue, EfrtValue> func)
        {
            var top = Stack.Top;
            Stack.Items[--Stack.Top] = func(Stack.Items[top - 1], Stack.Items[top]);
        }


        public void GoTo(int pos)
        {
            Tokenizer.SourcePos = pos;

            Tokenizer.NextChar();
        }


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
                case TokenType.Integer:
                    throw new Exception($"A name of a new word expected.");

                // Start the new word definition compilation.
                case TokenType.Word:
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
            Tokenizer = new Tokenizer(src);

            // Reset the interpreter state.
            Reset();

            Tokenizer.NextChar();

            var tok = Tokenizer.NextTok();
            while (tok.Code >= 0)
            {
                switch (tok.Code)
                {
                    case TokenType.Word:
                        if (_wordsList.IsWordDefined(tok.SValue))
                        {
                            CurrentWord = _wordsList.GetWord(tok.SValue);

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
                            // End this word compiling.
                            IsCompiling = false;

                            throw new Exception($"Unknown word '{tok}' canot be executed.");
                        }
                        break;

                    case TokenType.Integer:
                        if (IsCompiling)
                        {
                            WordBeingDefined.AddWord(new ValueWord(this, new EfrtValue(tok.IValue)));
                        }
                        else
                        {
                            Pushi(tok.IValue);
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

        public int BranchLevel { get; set; }
        
        private WordsList _wordsList;
        private BaseLib _baseLib;
        private IntegerLib _integerLib;
        private FloatLib _floatLib;
        private IoLib _ioLib;
    }
}
