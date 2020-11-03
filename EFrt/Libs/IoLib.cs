/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;
    using System.Globalization;
    using EFrt.Words;
   

    public class IoLib
    {
        private IInterpreter _interpreter;


        public IoLib(IInterpreter efrt)
        {
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".", false, WriteAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ".(", true, WriteStringAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "CR", false, WriteLineAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "F.", false, WriteFloatAction));

            // .S F. S. SPACES
        }

        // (a --)
        private int WriteAction()
        {
            Console.Write("{0} ", _interpreter.Pop().Int);

            return 1;
        }

        // (a --)
        private int WriteFloatAction()
        {
            Console.Write("{0} ", _interpreter.Pop().Float.ToString(CultureInfo.InvariantCulture));

            return 1;
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

                Console.Write(_interpreter.CurrentChar);

                c = _interpreter.NextChar();
            }

            if (c != ')')
            {
                throw new Exception("')' expected.");
            }

            return 1;
        }


        private int WriteLineAction()
        {
            Console.WriteLine();

            return 1;
        }
    }
}
