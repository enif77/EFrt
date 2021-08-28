/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Subtracts n2 from n1 and leaves the difference on the stack.
    /// (n1 n2 -- n3)
    /// </summary>
    public class MinusWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public MinusWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "-";
            Action = () =>
            {
                Interpreter.StackExpect(2);

                Interpreter.Function((n1, n2) => n1 - n2);

                return 1;
            };
        }
    }
}