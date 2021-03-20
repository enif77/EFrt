/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Initialize the pictured numeric output conversion process.
    /// </summary>
    public class LessNumberSignWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public LessNumberSignWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "<#";
            Action = () =>
            {
                // TODO: Check, that we are not inside <# and #> words.

                Interpreter.State.Picture = string.Empty;

                return 1;
            };
        }
    }
}