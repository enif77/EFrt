/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// If n is negative, add a minus sign to the beginning of the pictured numeric output string.
    /// (n -- )
    /// </summary>
    public class SignWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public SignWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "SIGN";
            Action = () =>
            {
                Interpreter.StackExpect(1);

                // TODO: Check, that we are inside <# and #> words.

                if (Interpreter.Pop() < 0)
                {
                    Interpreter.State.Picture = $"-{Interpreter.State.Picture}";
                }

                return 1;
            };
        }
    }
}