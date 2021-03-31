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
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        /// <param name="name">A name of this constant.</param>
        /// <param name="value">The value of this constant.</param>
        public FloatingPointConstantWord(IInterpreter interpreter, string name, double value)
            : base(interpreter)
        {
            Name = name;
            IsControlWord = true;
            Action = () =>
            {
                Interpreter.FStackFree(1);

                Interpreter.FPush(_value);

                return 1;
            };

            _value = value;
        }


        private readonly double _value;
    }
}
