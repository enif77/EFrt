/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.String
{
    using System;
    using System.Text;

    using EFrt.Core;
    using EFrt.Core.Words;


    public class Library : IWordsLIbrary
    {
        private IInterpreter _interpreter;


        public Library(IInterpreter efrt)
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
            var stack = _interpreter.State.ObjectStack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top].ToString());
        }


        private void Function(Func<string, string, string> func)
        {
            var stack = _interpreter.State.ObjectStack;
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

            c = _interpreter.CurrentChar;
            if (c != 0 && Tokenizer.IsWhite(c) == false)
            {
                throw new Exception("The EOF or an white character after a string literal expected.");
            }

            if (_interpreter.IsCompiling)
            {
                _interpreter.WordBeingDefined.AddWord(new StringLiteralWord(_interpreter, sb.ToString()));
            }
            else
            {
                _interpreter.OPush(sb.ToString());
            }

            return 1;
        }
    }
}
