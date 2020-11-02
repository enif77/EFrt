/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;

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

            // .S F. S. SPACES
        }

        // (a --)
        private void WriteAction()
        {
            Console.Write(_interpreter.Pop().Int);
        }


        private void WriteStringAction()
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
        }


        private void WriteLineAction()
        {
            Console.WriteLine();
        }
    }
}
