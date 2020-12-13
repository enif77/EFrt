/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;
    using System.Globalization;
    using System.Text;

    using EFrt.Values;
    using EFrt.Words;
   

    public class IoLib : IWordsLIbrary
    {
        private IInterpreter _interpreter;
        private IOutputWriter _outputWriter;
        

        public IoLib(IInterpreter efrt, IOutputWriter outputWriter)
        {
            _interpreter = efrt;
            _outputWriter = outputWriter;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new ImmediateWord(_interpreter, ".(", WriteImmediateStringAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, ".\"", PrintImmediateStringAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".", PrintAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "?", PrintIndirectAction));
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


        private int WriteImmediateStringAction()
        {
            var c = _interpreter.NextChar();
            while (_interpreter.CurrentChar != 0)
            {
                if (_interpreter.CurrentChar == ')')
                {
                    _interpreter.NextChar();

                    break;
                }

                _outputWriter.Write("{0}", _interpreter.CurrentChar);

                c = _interpreter.NextChar();
            }

            if (c != ')')
            {
                throw new Exception("')' expected.");
            }

            return 1;
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

            _interpreter.WordBeingDefined.AddWord(new PrintStringWord(_interpreter, _outputWriter, sb.ToString()));

            return 1;
        }

        // (n --)
        private int PrintAction()
        {
            _outputWriter.Write("{0} ", _interpreter.Pop());

            return 1;
        }

        // (addr --)
        private int PrintIndirectAction()
        {
            _outputWriter.Write("{0} ", _interpreter.VariableStack.Items[_interpreter.Pop()]);

            return 1;
        }

        // (f --)
        private int PrintFloatAction()
        {
            _outputWriter.Write("{0} ", new DoubleVal()
            {
                B = _interpreter.Pop(),
                A = _interpreter.Pop(),
            }.D.ToString(CultureInfo.InvariantCulture));

            return 1;
        }

        // {o --}
        private int PrintStringAction()
        {
            _outputWriter.Write(_interpreter.OPop().ToString());

            return 1;
        }


        private int PrintStackAction()
        {
            _outputWriter.Write("Stack: ");
            var stackItems = _interpreter.Stack.Items;
            for (var i = 0; i <= _interpreter.Stack.Top; i++)
            {
                _outputWriter.Write(stackItems[i].ToString(CultureInfo.InvariantCulture));
                _outputWriter.Write(" ");
            }
            
            return 1;
        }

        private int PrintObjectStackAction()
        {
            _outputWriter.WriteLine("Object stack: ");
            var stackItems = _interpreter.ObjectStack.Items;
            for (var i = _interpreter.ObjectStack.Top; i >= 0; i--)
            {
                _outputWriter.WriteLine($"  { stackItems[i] }");
            }

            return 1;
        }


        private int WriteLineAction()
        {
            _outputWriter.WriteLine();

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

                _outputWriter.Write(sb.ToString());
            }

            return 1;
        }


        private int WriteSpaceAction()
        {
            _outputWriter.Write(" ");
     
            return 1;
        }

        // (n --)
        private int EmitAction()
        {
            _outputWriter.Write("{0} ", (char)_interpreter.Pop());

            return 1;
        }


        private int WordsAction()
        {
            _outputWriter.Write("{0} ", _interpreter.WordsList.ToString());

            return 1;
        }
    }
}
