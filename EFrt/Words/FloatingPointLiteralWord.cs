/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    using EFrt.Stacks;


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
            Action = Execute;

            _value = value;
        }


        private int Execute()
        {
            Interpreter.FPush(_value);

            return 1;
        }


        private double _value;
    }
}
