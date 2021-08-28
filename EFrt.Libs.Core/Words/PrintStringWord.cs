/* EFrt - (C) 2020 - 2021 Premysl Fara  */

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
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="value">A value.</param>
        public PrintStringWord(IInterpreter interpreter, string value)
            : base(interpreter)
        {
            Name = "S.";
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.Output.Write(_value);

                return 1;
            };

            _value = value;
        }


        private readonly string _value;
    }
}
