/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    using EFrt.Core.Values;


    /// <summary>
    /// A word keeping a double cell integer value.
    /// </summary>
    public class DoubleCellIntegerLiteralWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">A value.</param>
        public DoubleCellIntegerLiteralWord(IInterpreter interpreter, long value)
            : base(interpreter)
        {
            Name = "DLITERAL";
            IsControlWord = true;
            Action = Execute;

            _value = new LongVal() { D = value };
        }


        private int Execute()
        {
            Interpreter.Push(_value.A);
            Interpreter.Push(_value.B);

            return 1;
        }


        private LongVal _value;
    }
}
