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
        public ValueWord(IInterpreter interpreter, EfrtValue value, int sourcePos = -1)
            : base(interpreter)
        {
            Name = "value";
            IsImmediate = false;
            Action = Execute;
            SourcePos = sourcePos;

            _value = value;
        }


        private void Execute()
        {
            Interpreter.Push(_value);
        }


        private EfrtValue _value;
    }
}
