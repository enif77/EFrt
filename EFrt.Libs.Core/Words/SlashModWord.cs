/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Divide n1 by n2, giving the single-cell remainder n3 and the single-cell quotient n4.
    /// (n1 n2 -- n3 n4)
    /// </summary>
    public class SlashModWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public SlashModWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "/MOD";
            Action = () =>
            {
                Interpreter.StackExpect(2);

                var n2 = Interpreter.Pop();
                var n1 = Interpreter.Pop();
            
                Interpreter.Push(n1 % n2);
                Interpreter.Push(n1 / n2);

                return 1;
            };
        }
    }
}