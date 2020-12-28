/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.DoubleExt
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// The CORE words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "DOUBLE-EXT";

        private IInterpreter _interpreter;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter"></param>
        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        /// <summary>
        /// Definas words from this library.
        /// </summary>
        public void DefineWords()
        {
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "2ROT", TwoRotAction));
        }


        // (n1 n2 n3 n4 n5 n6 -- n3 n4 n5 n6 n1 n2)
        private int TwoRotAction()
        {
            var n6 = _interpreter.Pop();
            var n5 = _interpreter.Pop();
            var n4 = _interpreter.Pop();
            var n3 = _interpreter.Pop();
            var n2 = _interpreter.Pop();
            var n1 = _interpreter.Pop();

            _interpreter.Push(n3);
            _interpreter.Push(n4);
            _interpreter.Push(n5);
            _interpreter.Push(n6);
            _interpreter.Push(n1);
            _interpreter.Push(n2);

            return 1;
        }
    }
}
