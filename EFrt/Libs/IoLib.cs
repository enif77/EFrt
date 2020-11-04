/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;
    using System.Globalization;
    using System.Text;

    using EFrt.Words;
   

    public class IoLib
    {
        private IOutputWriter _outputWriter;
        private IInterpreter _interpreter;


        public IoLib(IInterpreter efrt, IOutputWriter outputWriter)
        {
            _outputWriter = outputWriter;
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".(", true, WriteStringAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".", false, WriteAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F.", false, WriteFloatAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CR", false, WriteLineAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "SPACES", false, WriteSpacesAction));

            

            // S.
        }

        private int WriteStringAction()
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

        // (a --)
        private int WriteAction()
        {
            _outputWriter.Write("{0} ", _interpreter.Pop().Int);

            return 1;
        }

        // (a --)
        private int WriteFloatAction()
        {
            _outputWriter.Write("{0} ", _interpreter.Pop().Float.ToString(CultureInfo.InvariantCulture));

            return 1;
        }


        private int WriteLineAction()
        {
            _outputWriter.WriteLine();

            return 1;
        }


        private int WriteSpacesAction()
        {
            var count = _interpreter.Popi();
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
    }
}
