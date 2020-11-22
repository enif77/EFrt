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
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S+", AddAction));
            _interpreter.AddWord(new ImmediateWord(_interpreter, "S\"", LiteralAction));
        }


        private void Function(Func<string, string> func)
        {
            var stack = _interpreter.ObjectStack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top].ToString());
        }


        private void Function(Func<string, string, string> func)
        {
            var stack = _interpreter.ObjectStack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1].ToString(), stack.Items[top].ToString());
        }


        // {a b -- result}
        private int AddAction()
        {
            Function((a, b) => a.ToString() + b.ToString());

            return 1;
        }

        // { -- s}
        private int LiteralAction()
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

            _interpreter.WordBeingDefined.AddWord(new StringLiteralWord(_interpreter, sb.ToString()));

            return 1;
        }
    }
}
