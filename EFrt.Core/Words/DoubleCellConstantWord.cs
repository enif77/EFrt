/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    using EFrt.Core.Extensions;
    
    /// <summary>
    /// A word keeping two integer values.
    /// </summary>
    public class DoubleCellConstantWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        /// <param name="name">A name of this constant.</param>
        /// <param name="n1">The first value.</param>
        /// <param name="n2">The second value.</param>
        public DoubleCellConstantWord(IInterpreter interpreter, string name, int n1, int n2)
            : base(interpreter)
        {
            Name = name;
            IsControlWord = true;
            Action = () =>
            {
                Interpreter.StackFree(2);

                Interpreter.Push(_n1);
                Interpreter.Push(_n2);

                return 1;
            };

            _n1 = n1;
            _n2 = n2;
        }


        private readonly int _n1;
        private readonly int _n2;
    }
}
