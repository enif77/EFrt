/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    using EFrt.Core;


    /// <summary>
    /// A word keeping an integer value.
    /// </summary>
    public class FloatingPointValueWord : AWordBase
    {
        /// <summary>
        /// A value. Can be updated by the TO word.
        /// </summary>
        public double Value { get; set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        /// <param name="name">A name of this value.</param>
        /// <param name="f">A floating point value.</param>
        public FloatingPointValueWord(IInterpreter interpreter, string name, double f)
            : base(interpreter)
        {
            Name = name;
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.StackFree(1);

                Interpreter.FPush(Value);

                return 1;
            };

            Value = f;
        }
    }
}
