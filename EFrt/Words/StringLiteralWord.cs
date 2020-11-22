/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    /// <summary>
    /// A word keeping a string value.
    /// </summary>
    public class StringLiteralWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">A value.</param>
        public StringLiteralWord(IInterpreter interpreter, string value)
            : base(interpreter)
        {
            Name = "string";
            IsControlWord = true;
            Action = Execute;

            _value = value;
        }


        private int Execute()
        {
            Interpreter.OPush(_value);

            return 1;
        }


        private string _value;
    }
}
