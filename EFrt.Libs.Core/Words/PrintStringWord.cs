/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word keeping and printing a string value.
    /// </summary>
    public class PrintStringWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">A value.</param>
        public PrintStringWord(IInterpreter interpreter, string value)
            : base(interpreter)
        {
            Name = "string";
            IsControlWord = true;
            Action = Execute;

            _value = value;
        }


        private int Execute()
        {
            Interpreter.Output.Write(_value);

            return 1;
        }


        private string _value;
    }
}
