/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.CoreExt
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Words;

    using EFrt.Libs.CoreEx.Words;


    /// <summary>
    /// The CORE-EXT words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "CORE-EXT";

        private IInterpreter _interpreter;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter"></param>
        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        /// <summary>
        /// Definas words from this library.
        /// </summary>
        public void DefineWords()
        {
            _interpreter.AddWord(new ImmediateWord(_interpreter, ".(", DotParenAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0<>", ZeroNotEqualsAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "0>", ZeroGreaterAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2>R", TwoToRAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2R>", TwoRFromAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2R@", TwoRFetchAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ":NONAME", NonameAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, ";", SemicolonAction));  // Extended version.
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "<>", NotEqualsAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "?DO", QuestionDoAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "AGAIN", AgainAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "NIP", NipAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "PICK", PickAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ROLL", RollAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "TO", ToAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "TUCK", TuckAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "VALUE", ValueAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "\\", BackslashAction));

            _interpreter.AddWord(new ConstantWord(_interpreter, "FALSE", 0));
            _interpreter.AddWord(new ConstantWord(_interpreter, "TRUE", -1));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "-ROLL", MinusRollAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "-ROT", MinusRotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2+", AddTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2-", SubTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2NIP", TwoNipAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2TUCK", TwoTuckAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "<=", IsLtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ">=", IsGtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CLEAR", ClearAction));
        }


        private int DotParenAction()
        {
            _interpreter.Output.Write(_interpreter.GetTerminatedString(')'));

            return 1;
        }

        // (n -- flag)
        private int ZeroNotEqualsAction()
        {
            _interpreter.Function((a) => (a != 0) ? -1 : 0);

            return 1;
        }

        // (n -- flag)
        private int ZeroGreaterAction()
        {
            _interpreter.Function((a) => (a > 0) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- ) [ -- n1 n2]
        private int TwoToRAction()
        {
            var n2 = _interpreter.Pop();
            _interpreter.RPush(_interpreter.Pop());
            _interpreter.RPush(n2);

            return 1;
        }

        // ( -- n1 n2) [n1 n2 -- ]
        private int TwoRFromAction()
        {
            var n2 = _interpreter.RPop();
            _interpreter.Push(_interpreter.RPop());
            _interpreter.Push(n2);

            return 1;
        }

        // ( -- n1 n2) [n1 n2 -- ]
        private int TwoRFetchAction()
        {
            _interpreter.Push(_interpreter.RPick(1));  // n1
            _interpreter.Push(_interpreter.RPick(0));  // n2

            return 1;
        }

        // :NONAME body ;
        private int NonameAction()
        {
            _interpreter.BeginNewWordCompilation();
            _interpreter.WordBeingDefined = new NonameWord(_interpreter);

            return 1;
        }

        // : word-name body ;
        // :NONAME body ;
        private int SemicolonAction()
        {
            // Each user defined word exits with the EXIT word.
            _interpreter.WordBeingDefined.AddWord(new ExitControlWord(_interpreter, _interpreter.WordBeingDefined));

            _interpreter.EndNewWordCompilation();

            // Compilation of NONAME words leaves their execution token on the stack.
            if (_interpreter.WordBeingDefined is NonameWord)
            {
                _interpreter.Push(_interpreter.WordBeingDefined.ExecutionToken);
            }

            return 1;
        }

        // (n1 n2 -- flag)
        private int NotEqualsAction()
        {
            _interpreter.Function((a, b) => (a != b) ? -1 : 0);

            return 1;
        }

        // (limit end -- )
        private int QuestionDoAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("?DO outside a new word definition.");
            }

            _interpreter.RPush(
                _interpreter.WordBeingDefined.AddWord(
                    new IfDoControlWord(_interpreter, _interpreter.WordBeingDefined.NextWordIndex)));

            return 1;
        }

        // [ index-of-a-word-folowing-BEGIN -- ]
        private int AgainAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("AGAIN outside a new word definition.");
            }

            // AGAIN word doesn't have a runtime behavior.

            _interpreter.WordBeingDefined.AddWord(
                new AgainControlWord(
                    _interpreter,
                    _interpreter.RPop() - _interpreter.WordBeingDefined.NextWordIndex));

            return 1;
        }

        // (n1 n2 -- n2)
        private int NipAction()
        {
            var n2 = _interpreter.Pop();
            _interpreter.Drop();     // n1
            _interpreter.Push(n2);

            return 1;
        }

        // (index -- n)
        private int PickAction()
        {
            _interpreter.Push(_interpreter.Pick(_interpreter.Pop()));

            return 1;
        }

        // (index -- )
        private int RollAction()
        {
            _interpreter.Roll(_interpreter.Pop());

            return 1;
        }

        // (n -- )
        private int ToAction()
        {
            var valueWord = _interpreter.GetWord(_interpreter.GetWordName());
            if (valueWord is ValueWord)
            {
                ((ValueWord)valueWord).Value = _interpreter.Pop();

                return 1;
            }

            throw new Exception("A VALUE created word expected.");            
        }

        // (n1 n2 -- n2 n1 n2)
        private int TuckAction()
        {
            var n2 = _interpreter.Peek();
            _interpreter.Swap();    // n2 n1
            _interpreter.Push(n2);  // n2 n1 n2

            return 1;
        }

        // VALUE word-name
        // (n -- )
        private int ValueAction()
        {
            _interpreter.BeginNewWordCompilation();
            _interpreter.AddWord(new ValueWord(_interpreter, _interpreter.GetWordName(), _interpreter.Pop()));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // ( -- )
        private int BackslashAction()
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

            return 1;
        }


        // Extra

        // (index -- n)
        private int MinusRollAction()
        {
            var index = _interpreter.Pop();
            var items = _interpreter.State.Stack.Items;
            var top = _interpreter.State.Stack.Top;

            var item = items[top];
            for (var i = top - 1; i >= top - index; i--)
            {
                items[i + 1] = items[i];
            }

            items[top - index] = item;

            return 1;
        }

        // (n1 -- n2)
        private int AddTwoAction()
        {
            _interpreter.Function((a) => a + 2);

            return 1;
        }

        // (n1 -- n2)
        private int SubTwoAction()
        {
            _interpreter.Function((a) => a - 2);

            return 1;
        }

        // (n1 n2 n3 n4 -- n3 n4)
        private int TwoNipAction()
        {
            var n4 = _interpreter.Pop();
            var n3 = _interpreter.Pop();
            _interpreter.Drop(2);
            _interpreter.Push(n3);
            _interpreter.Push(n4);

            return 1;
        }

        // (n1 n2 n3 n4 -- n3 n4 n1 n2 n3 n4)
        private int TwoTuckAction()
        {
            var n4 = _interpreter.Pop();
            var n3 = _interpreter.Pop();
            var n2 = _interpreter.Pop();
            var n1 = _interpreter.Pop();
            _interpreter.Push(n3);
            _interpreter.Push(n4);
            _interpreter.Push(n1);
            _interpreter.Push(n2);
            _interpreter.Push(n3);
            _interpreter.Push(n4);

            return 1;
        }

        // (n1 n2 -- flag)
        private int IsLtEAction()
        {
            _interpreter.Function((a, b) => (a <= b) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int IsGtEAction()
        {
            _interpreter.Function((a, b) => (a >= b) ? -1 : 0);

            return 1;
        }

        // (a b c -- c a b)
        private int MinusRotAction()
        {
            var v3 = _interpreter.Pop();
            var v2 = _interpreter.Pop();
            var v1 = _interpreter.Pop();

            _interpreter.Push(v3);
            _interpreter.Push(v1);
            _interpreter.Push(v2);

            return 1;
        }

        // ( -- )
        private int ClearAction()
        {
            _interpreter.State.Stack.Clear();

            return 1;
        }
    }
}
