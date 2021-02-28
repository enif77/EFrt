/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    using EFrt.Core;


    /// <summary>
    /// A word keeping an integer value.
    /// </summary>
    public class SingleCellValueWord : AWordBase
    {
        /// <summary>
        /// A value. Can be updated by the TO word.
        /// </summary>
        public int Value { get; set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        /// <param name="name">A name of this value.</param>
        /// <param name="n">A value or an address.</param>
        public SingleCellValueWord(IInterpreter interpreter, string name, int n)
            : base(interpreter)
        {
            Name = name;
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.StackFree(1);

                Interpreter.Push(Value);

                return 1;
            };

            Value = n;
        }
    }
}
