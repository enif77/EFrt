/* EFrt - (C) 2021 Premysl Fara  */

using EFrt.Core.Extensions;

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Multiplies n1 and n2 and leaves the product on the stack.
    /// (n1 n2 -- n3)
    /// </summary>
    public class StarWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public StarWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "*";
            Action = () =>
            {
                Interpreter.StackExpect(2);
                // StackFree() is not necessary here - we will pop 2 items, before we push one.

                Interpreter.Function((n1, n2) => n1 * n2);

                return 1; 
            };
        }
    }
}