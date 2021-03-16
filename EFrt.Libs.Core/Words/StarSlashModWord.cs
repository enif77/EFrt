/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Multiplies n1 and n2 producing the double-cell result d. Divides d by n3 giving the single cell remainder n4 and single cell quotient n5.
    /// (n1 n2 n3 -- n4 n5)
    /// </summary>
    public class StarSlashModWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public StarSlashModWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "*/MOD";
            Action = () =>
            {
                Interpreter.StackExpect(3);

                var n3 = (long)Interpreter.Pop();
                var d = (long)Interpreter.Pop() * (long)Interpreter.Pop();
                Interpreter.Push((int)(d % n3));  // n4 = d % n3
                Interpreter.Push((int)(d / n3));  // n5 = d / n3

                return 1;
            };
        }
    }
}