/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs
{
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
        }

        // {a b -- result}
        private int AddAction()
        {
            _interpreter.Function((a, b) => a.ToString() + b.ToString());

            return 1;
        }
    }
}
