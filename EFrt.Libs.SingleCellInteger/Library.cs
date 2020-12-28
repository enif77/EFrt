/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.SingleCellInteger
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Words;


    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "INT";

        private IInterpreter _interpreter;


        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void DefineWords()
        {
            
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2+", AddTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2-", SubTwoAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "<=", IsLtEAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, ">=", IsGtEAction));
                        
            
        }


        private void Function(Func<int, int> func)
        {
            var stack = _interpreter.State.Stack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top]);
        }


        private void Function(Func<int, int, int> func)
        {
            var stack = _interpreter.State.Stack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);
        }


        // (n1 -- n2)
        private int AddTwoAction()
        {
            Function((a) => a + 2);

            return 1;
        }

        // (n1 -- n2)
        private int SubTwoAction()
        {
            Function((a) => a - 2);

            return 1;
        }

        // (n1 n2 -- flag)
        private int IsLtEAction()
        {
            Function((a, b) => (a <= b) ? -1 : 0);

            return 1;
        }

        // (n1 n2 -- flag)
        private int IsGtEAction()
        {
            Function((a, b) => (a >= b) ? -1 : 0);

            return 1;
        }
    }
}
