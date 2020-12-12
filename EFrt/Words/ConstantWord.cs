/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    /// <summary>
    /// A word keeping an integer value.
    /// </summary>
    public class ConstantWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A name of this constant.</param>
        /// <param name="n">A value.</param>
        public ConstantWord(IInterpreter interpreter, string name, int n)
            : base(interpreter)
        {
            Name = name;
            IsControlWord = true;
            Action = Execute;

            _n = n;
        }


        private int Execute()
        {
            Interpreter.Push(_n);

            return 1;
        }


        private int _n;
    }
}
