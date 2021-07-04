/* EFrt - (C) 2020 - 2021 Premysl Fara  */

using EFrt.Core.Extensions;

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word keeping an integer value or an address/index of a single or double cell variable.
    /// </summary>
    public class ConstantWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        /// <param name="name">A name of this constant.</param>
        /// <param name="value">A value or an address.</param>
        public ConstantWord(IInterpreter interpreter, string name, int value)
            : base(interpreter)
        {
            Name = name;
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.StackFree(1);

                Interpreter.Push(_value);

                return 1;
            };

            _value = value;
        }


        private readonly int _value;
    }
}
