/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word keeping two integer values.
    /// </summary>
    public class DoubleConstantWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A name of this constant.</param>
        /// <param name="n1">The first value.</param>
        /// <param name="n2">The second value.</param>
        public DoubleConstantWord(IInterpreter interpreter, string name, int n1, int n2)
            : base(interpreter)
        {
            Name = name;
            IsControlWord = true;
            Action = Execute;

            _n1 = n1;
            _n2 = n2;
        }


        private int Execute()
        {
            Interpreter.Push(_n1);
            Interpreter.Push(_n2);

            return 1;
        }


        private int _n1;
        private int _n2;
    }
}
