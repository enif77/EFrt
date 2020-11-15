/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    /// <summary>
    /// A word that is definig the loop goto-begining jump.
    /// </summary>
    public class AgainControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="increment">The index increment to jump to the beginning of a loop.</param>
        public AgainControlWord(IInterpreter interpreter, int increment)
            : base(interpreter)
        {
            Name = "AgainControlWord";
            IsControlWord = true;
            Action = Execute;

            _increment = increment;
        }


        private int _increment;


        private int Execute()
        {
            return _increment;
        }
    }
}
