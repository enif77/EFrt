/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is defining the loop goto-beginning jump.
    /// </summary>
    public class RepeatControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="increment">The index increment to jump to the beginning of a loop.</param>
        public RepeatControlWord(IInterpreter interpreter, int increment)
            : base(interpreter)
        {
            Name = "REPEAT";
            IsControlWord = true;
            Action = () => _increment;

            _increment = increment;
        }


        private int _increment;
    }
}
