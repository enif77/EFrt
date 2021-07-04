/* EFrt - (C) 2020 - 2021 Premysl Fara  */

using EFrt.Core.Extensions;

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

                Interpreter.DPush(_value);

                return 1;
            };

            _value = value;
        }


        private readonly long _value;
    }
}
