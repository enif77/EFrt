/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    using EFrt.Core.Values;


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
                Interpreter.Push(_value.A);
                Interpreter.Push(_value.B);

                return 1;
            };

            _value = new FloatingPointValue()
            {
                D = value
            };
        }


        private FloatingPointValue _value;
    }
}
