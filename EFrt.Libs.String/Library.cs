/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.String
{
    using System;
    using System.Text;

    using EFrt.Core;
    using EFrt.Core.Words;


    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "STRING";

        private IInterpreter _interpreter;


        public Library(IInterpreter efrt)
        {
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S.", PrintStringAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "S+", AddAction));
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


        // {o --}
        private int PrintStringAction()
        {
            _interpreter.Output.Write(_interpreter.OPop().ToString());

            return 1;
        }

        // {a b -- result}
        private int AddAction()
        {
            Function((a, b) => a.ToString() + b.ToString());

            return 1;
        }
    }
}
