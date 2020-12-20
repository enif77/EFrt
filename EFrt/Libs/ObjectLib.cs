/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Words;
    

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
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OALLOT", AllotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OHERE", HereAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OVARIABLE", VariableCompilationAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "O!", StoreToVariableAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "O@", FetchFromVariableAction));

            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ODUP", DupAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ODROP", DropAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OSWAP", SwapAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OOVER", OverAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OPICK", PickAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OROLL", RollAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OOROT", RotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "-OROT", RotBackAction));
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

        // (n -- addr)
        private int AllotAction()
        {
            _interpreter.Push(_interpreter.ObjectHeap.Alloc(_interpreter.Pop()));

            return 1;
        }

        // ( -- addr)
        private int HereAction()
        {
            _interpreter.Push(_interpreter.ObjectHeap.Top);

            return 1;
        }

        // OVARIABLE word-name
        // ( -- )
        private int VariableCompilationAction()
        {
            _interpreter.AddWord(new ConstantWord(_interpreter, _interpreter.BeginNewWordCompilation(), _interpreter.ObjectHeap.Alloc(1)));
            _interpreter.EndNewWordCompilation();

            return 1;
        }

        // (addr -- ) {o -- }
        private int StoreToVariableAction()
        {
            var addr = _interpreter.Pop();
            _interpreter.ObjectHeap.Items[addr] = _interpreter.OPop();

            return 1;
        }

        // (addr -- ) { -- o}
        private int FetchFromVariableAction()
        {
            _interpreter.OPush(_interpreter.ObjectHeap.Items[_interpreter.Pop()]);

            return 1;
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

        // (index -- ) { -- o}
        private int PickAction()
        {
            _interpreter.OPush(_interpreter.OPick(_interpreter.Pop()));

            return 1;
        }

        // (index -- )
        private int RollAction()
        {
            _interpreter.ORoll(_interpreter.Pop());

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
