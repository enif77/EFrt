/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.CoreExt.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is defining the loop goto-beginning jump.
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
            Name = "AGAIN";
            IsControlWord = true;
            Action = () => _increment;

            _increment = increment;
        }


        private readonly int _increment;
    }
}
