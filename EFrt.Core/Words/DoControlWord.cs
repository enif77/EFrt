/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word, that is definig the do loop begining.
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
            Action = () => 
            {
                var index = Interpreter.Pop();

                Interpreter.RPush(Interpreter.Pop());  // limit
                Interpreter.RPush(index);              // index

                return 1;
            };
        }
    }
}
