/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.IO
{
    using System;
    using System.Globalization;
    using System.Text;

    using EFrt.Core;
    using EFrt.Core.Values;
    using EFrt.Core.Words;
   

    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "IO";

        private IInterpreter _interpreter;
       

        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void DefineWords()
        {
            
            _interpreter.AddWord(new ImmediateWord(_interpreter, ".\"", PrintImmediateStringAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".", PrintAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "?", PrintIndirectAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "D.", PrintLongAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F.", PrintFloatAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S.", PrintStringAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".O", PrintObjectStackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".S", PrintStackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CR", WriteLineAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "SPACES", WriteSpacesAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "SPACE", WriteSpaceAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "EMIT", EmitAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "WORDS", WordsAction));
        }
                

        private int PrintImmediateStringAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception(".\" outside a new word definition.");
            }

            var sb = new StringBuilder();

            var c = _interpreter.NextChar();
            while (_interpreter.CurrentChar != 0)
            {
                if (_interpreter.CurrentChar == '"')
                {
                    _interpreter.NextChar();

                    break;
                }

                sb.Append(_interpreter.CurrentChar);

                c = _interpreter.NextChar();
            }

            if (c != '"')
            {
                throw new Exception("'\"' expected.");
            }

            _interpreter.WordBeingDefined.AddWord(new PrintStringWord(_interpreter, sb.ToString()));

            return 1;
        }

        // (n --)
        private int PrintAction()
        {
            _interpreter.Output.Write("{0} ", _interpreter.Pop());

            return 1;
        }

        // (addr --)
        private int PrintIndirectAction()
        {
            _interpreter.Output.Write("{0} ", _interpreter.State.Heap.Items[_interpreter.Pop()]);

            return 1;
        }

        // (d --)
        private int PrintLongAction()
        {
            _interpreter.Output.Write("{0} ", new LongVal()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D.ToString(CultureInfo.InvariantCulture));

            return 1;
        }

        // (f --)
        private int PrintFloatAction()
        {
            _interpreter.Output.Write("{0} ", new DoubleVal()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D.ToString(CultureInfo.InvariantCulture));

            return 1;
        }

        // {o --}
        private int PrintStringAction()
        {
            _interpreter.Output.Write(_interpreter.OPop().ToString());

            return 1;
        }


        private int PrintStackAction()
        {
            _interpreter.Output.Write("Stack: ");
            var stackItems = _interpreter.State.Stack.Items;
            for (var i = 0; i <= _interpreter.State.Stack.Top; i++)
            {
                _interpreter.Output.Write(stackItems[i].ToString(CultureInfo.InvariantCulture));
                _interpreter.Output.Write(" ");
            }
            
            return 1;
        }

        private int PrintObjectStackAction()
        {
            _interpreter.Output.WriteLine("Object stack: ");
            var stackItems = _interpreter.State.ObjectStack.Items;
            for (var i = _interpreter.State.ObjectStack.Top; i >= 0; i--)
            {
                _interpreter.Output.WriteLine($"  { stackItems[i] }");
            }

            return 1;
        }


        private int WriteLineAction()
        {
            _interpreter.Output.WriteLine();

            return 1;
        }


        private int WriteSpacesAction()
        {
            var count = _interpreter.Pop();
            if (count > 0)
            {
                var sb = new StringBuilder(count);
                for (var i = 0; i < count; i++)
                {
                    sb.Append(' ');
                }

                _interpreter.Output.Write(sb.ToString());
            }

            return 1;
        }


        private int WriteSpaceAction()
        {
            _interpreter.Output.Write(" ");
     
            return 1;
        }

        // (n --)
        private int EmitAction()
        {
            _interpreter.Output.Write("{0}", (char)_interpreter.Pop());

            return 1;
        }


        private int WordsAction()
        {
            _interpreter.Output.Write("{0} ", _interpreter.State.WordsList.ToString());

            return 1;
        }
    }
}
