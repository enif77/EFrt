/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word keeping an integer value or an address/index of a single or double cell variable.
    /// </summary>
    public class ConstantWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A name of this constant.</param>
        /// <param name="n">A value or an address.</param>
        public ConstantWord(IInterpreter interpreter, string name, int n)
            : base(interpreter)
        {
            Name = name;
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.StackFree(1);

                Interpreter.Push(_n);

                return 1;
            };

            _n = n;
        }


        private int _n;
    }
}
