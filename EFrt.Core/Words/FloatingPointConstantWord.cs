/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word keeping a floating point value.
    /// </summary>
    public class FloatingPointConstantWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A name of this constant.</param>
        /// <param name="n1">The first value.</param>
        /// <param name="n2">The second value.</param>
        public FloatingPointConstantWord(IInterpreter interpreter, string name, double f)
            : base(interpreter)
        {
            Name = name;
            IsControlWord = true;
            Action = () =>
            {
                Interpreter.FStackFree(1);

                Interpreter.FPush(_f);

                return 1;
            };

            _f = f;
        }


        private double _f;
    }
}
