/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    /// <summary>
    /// A word keeping a value.
    /// </summary>
    public class ValueWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">A value.</param>
        public ValueWord(IInterpreter interpreter, int value)
            : base(interpreter)
        {
            Name = "value";
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
