/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;
    using System.Text;

    using EFrt.Words;


    public class StringLib : IWordsLIbrary
    {
        private IInterpreter _interpreter;


        public StringLib(IInterpreter efrt)
        {
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S+", false, AddAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S\"", true, ParseStringAction));
        }

        // {a b -- result}
        private int AddAction()
        {
            _interpreter.Function((a, b) => a.ToString() + b.ToString());

            return 1;
        }

        // { -- s}
        private int ParseStringAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("S\" outside a new word definition.");
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

            // TODO: Expect a white char.

            _interpreter.WordBeingDefined.AddWord(new StringWord(_interpreter, sb.ToString()));

            return 1;
        }
    }
}
