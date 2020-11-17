/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    using EFrt.Stacks;


    /// <summary>
    /// A word keeping a value.
    /// </summary>
    public class ValueWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">A value.</param>
        public ValueWord(IInterpreter interpreter, StackValue value)
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


        private StackValue _value;
    }
}
