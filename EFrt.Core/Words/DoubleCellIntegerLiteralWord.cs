/* EFrt - (C) 2020 - 2021 Premysl Fara  */

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
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        /// <param name="value">A value.</param>
        public DoubleCellIntegerLiteralWord(IInterpreter interpreter, long value)
            : base(interpreter)
        {
            Name = "DLITERAL";
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.StackFree(2);

                Interpreter.Push(_value.A);
                Interpreter.Push(_value.B);

                return 1;
            };

            _value = new DoubleCellIntegerValue() { D = value };
        }


        private readonly DoubleCellIntegerValue _value;
    }
}
