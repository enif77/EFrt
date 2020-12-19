/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    /// <summary>
    /// A word keeping an integer value.
    /// </summary>
    public class SingleCellIntegerLiteralWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">A value.</param>
        public SingleCellIntegerLiteralWord(IInterpreter interpreter, int value)
            : base(interpreter)
        {
            Name = "LITERAL";
            IsControlWord = true;
            Action = Execute;

            _value = value;
        }


        private int Execute()
        {
            Interpreter.Push(_value);

            return 1;
        }


        private int _value;
    }
}
