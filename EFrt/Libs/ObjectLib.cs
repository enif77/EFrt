/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
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
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ODUP", false, DupAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ODROP", false, DropAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OSWAP", false, SwapAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OOVER", false, OverAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OROT", false, RotAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "O-ROT", false, RotBackAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "ODEPTH", false, DepthAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "OCLEAR", false, ClearAction));
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
            _interpreter.Pushi(_interpreter.ObjectStack.Count);

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
