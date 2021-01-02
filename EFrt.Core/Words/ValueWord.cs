/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word keeping an integer value.
    /// </summary>
    public class ValueWord : AWordBase
    {
        /// <summary>
        /// A value. Can be updated by the TO word.
        /// </summary>
        public int Value { get; set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A name of this value.</param>
        /// <param name="n">A value or an address.</param>
        public ValueWord(IInterpreter interpreter, string name, int n)
            : base(interpreter)
        {
            Name = name;
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.Push(Value);

                return 1;
            };

            Value = n;
        }
    }
}
