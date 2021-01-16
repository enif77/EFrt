/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    using EFrt.Core;


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
            Action = () =>
            {
                Interpreter.ObjectStackFree(1);

                Interpreter.OPush(_value);

                return 1;
            };

            _value = value;
        }


        private string _value;
    }
}
