/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Multiplies n1 and n2 producing the double-cell result d. Divides d by n3 giving the single cell quotient n4.
    /// (n1 n2 n3 -- n4)
    /// </summary>
    public class StarSlashWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public StarSlashWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "*/";
            Action = () =>
            {
                Interpreter.StackExpect(3);

                var n3 = (long)Interpreter.Pop();
                Interpreter.Push((int)((long)Interpreter.Pop() * (long)Interpreter.Pop() / n3));  // n2 * n1 / n3 

                return 1;
            };
        }
    }
}