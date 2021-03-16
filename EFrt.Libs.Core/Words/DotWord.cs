/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Prints the integer number on the top of the stack.
    /// (n -- )
    /// </summary>
    public class DotWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public DotWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = ".";
            Action = () =>
            {
                Interpreter.StackExpect(1);

                Interpreter.Output.Write("{0} ", Interpreter.Pop());

                return 1;
            };
        }
    }
}