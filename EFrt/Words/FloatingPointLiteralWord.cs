﻿/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    using EFrt.Values;


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

            _value = new DoubleVal()
            {
                D = value
            };
        }


        private int Execute()
        {
            Interpreter.Push(_value.A);
            Interpreter.Push(_value.B);

            return 1;
        }


        private DoubleVal _value;
    }
}