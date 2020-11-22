/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;

    using EFrt.Words;
    

    /// <summary>
    /// Object stack related words.
    /// </summary>
    public class ObjectLib : IWordsLIbrary
    {
        private IInterpreter _interpreter;


        public ObjectLib(IInterpreter efrt)
        {
            _interpreter = efrt;
        }


        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ODUP", DupAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ODROP", DropAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OSWAP", SwapAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OOVER", OverAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OROT", RotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "O-ROT", RotBackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ODEPTH", DepthAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OCLEAR", ClearAction));
        }


        private void Function(Func<object, object> func)
        {
            var stack = _interpreter.ObjectStack;
            var top = stack.Top;
            stack.Items[stack.Top] = func(stack.Items[top]);
        }


        private void Function(Func<object, object, object> func)
        {
            var stack = _interpreter.ObjectStack;
            var top = stack.Top;
            stack.Items[--stack.Top] = func(stack.Items[top - 1], stack.Items[top]);
        }


        // {o -- o o}
        private int DupAction()
        {
            _interpreter.ODup();

            return 1;
        }

        // {o --}
        private int DropAction()
        {
            _interpreter.ODrop();

            return 1;
        }

        // (a b -- b a)
        private int SwapAction()
        {
            _interpreter.OSwap();

            return 1;
        }

        // (a b -- a b a)
        private int OverAction()
        {
            _interpreter.OOver();

            return 1;
        }

        // (a b c -- b c a)
        private int RotAction()
        {
            _interpreter.ORot();

            return 1;
        }

        // (a b c -- c a b)
        private int RotBackAction()
        {
            var v3 = _interpreter.OPop();
            var v2 = _interpreter.OPop();
            var v1 = _interpreter.OPop();

            _interpreter.OPush(v3);
            _interpreter.OPush(v1);
            _interpreter.OPush(v2);

            return 1;
        }

        // ( -- a)
        private int DepthAction()
        {
            _interpreter.Push(_interpreter.ObjectStack.Count);

            return 1;
        }

        // ( -- )
        private int ClearAction()
        {
            _interpreter.ObjectStack.Clear();

            return 1;
        }
    }
}
