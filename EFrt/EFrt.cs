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


        public EfrtExecutor(int stackCapacity = 32, int returnStackCapacity = 32)
        {
            Stack = new DataStack(stackCapacity);
            ReturnStack = new ReturnStack(returnStackCapacity);

            _wordsList = new WordsList();
            _baseLib = new BaseLib(this);

            Reset();
        }


        public void Reset()
        {
            Stack.Init(new EfrtValue(0));
            ReturnStack.Init(0);

            _wordsList.Clear();
            _baseLib.DefineWords();
        }


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
            _wordsList.RegisterWord(word);
        }


        public void RemoveWord(string wordName)
        {
            _wordsList.RemoveWord(wordName);
        }

       

        #region stack

        public DataStack Stack { get; private set; }

        #endregion


        #region return stack

        public ReturnStack ReturnStack { get; private set; }

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
                            CurrentWord.Action();
                        }
                        else
                        {
                            throw new Exception($"Unknown word '{tok}' cant be executed.");
                        }
                        break;

                    case TokenType.Integer:
                        Stack.Items[++Stack.Top].Int = tok.IValue;
                        break;

                    default:
                        throw new Exception($"Unknown token in a word execution.");
                }

                tok = Tokenizer.NextTok();
            }
        }


        private Tokenizer Tokenizer { get; set; }

        public int BranchLevel { get; set; }
        
        private WordsList _wordsList;
        private BaseLib _baseLib;



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
    }
}
