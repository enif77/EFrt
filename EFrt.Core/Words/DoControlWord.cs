/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word that is definig do loop begining.
    /// </summary>
    public class DoControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public DoControlWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "DoControlWord";
            IsControlWord = true;
            Action = Execute;
        }


        private int Execute()
        {
            var index = Interpreter.Pop();
            var limit = Interpreter.Pop();

            Interpreter.RPush(limit);
            Interpreter.RPush(index);

            return 1;
        }
    }
}
