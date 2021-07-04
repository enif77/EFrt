/* EFrt - (C) 2021 Premysl Fara  */

using EFrt.Core.Extensions;

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Add char to the beginning of the pictured numeric output string.
    /// (char -- )
    /// </summary>
    public class HoldWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public HoldWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "HOLD";
            Action = () =>
            {
                Interpreter.StackExpect(1);

                // TODO: Check, that we are inside <# and #> words.

                Interpreter.State.Picture = $"{(char) Interpreter.Pop()}{{Interpreter.State.Picture}}";

                return 1;
            };
        }
    }
}