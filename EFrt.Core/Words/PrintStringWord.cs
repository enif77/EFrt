/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word keeping and printing a string value.
    /// </summary>
    public class PrintStringWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">A value.</param>
        public PrintStringWord(IInterpreter interpreter, IOutputWriter outputWriter, string value)
            : base(interpreter)
        {
            Name = "string";
            IsControlWord = true;
            Action = Execute;

            _outputWriter = outputWriter;
            _value = value;
        }


        private int Execute()
        {
            _outputWriter.Write(_value);

            return 1;
        }


        private IOutputWriter _outputWriter;
        private string _value;
    }
}
