/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word keeping an floating point value.
    /// </summary>
    public class FloatingPointLiteralWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">A value.</param>
        public FloatingPointLiteralWord(IInterpreter interpreter, double value)
            : base(interpreter)
        {
            Name = "FLITERAL";
            IsControlWord = true;
            Action = () =>
            {
                Interpreter.FStackFree(1);

                Interpreter.FPush(_value);

                return 1;
            };

            _value = value;
        }


        private double _value;
    }
}
