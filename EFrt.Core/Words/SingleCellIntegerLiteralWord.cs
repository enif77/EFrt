/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
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
            Action = () =>
            {
                Interpreter.StackFree(1);

                Interpreter.Push(_value);

                return 1;
            };

            _value = value;
        }


        private int _value;
    }
}
