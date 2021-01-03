/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is definig the loop goto-begining jump.
    /// </summary>
    public class UntilControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="increment">The index increment to jump to the beginning of a loop.</param>
        public UntilControlWord(IInterpreter interpreter, int increment)
            : base(interpreter)
        {
            Name = "UntilControlWord";
            IsControlWord = true;
            Action = Execute;

            _increment = increment;
        }


        private int _increment;

        private int Execute()
        {
            // (flag -- )
            if (Interpreter.Pop() == 0)
            {
                // The flag is FALSE - repeat the loop.
                return _increment;
            }

            // The flag is TRUE, end the loop.
            return 1;
        }
    }
}
