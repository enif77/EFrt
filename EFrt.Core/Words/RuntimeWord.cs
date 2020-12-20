/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A word used at runtime.
    /// </summary>
    public class RuntimeWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A name of this word.</param>
        public RuntimeWord(IInterpreter interpreter, string name)
            : base(interpreter)
        {
            Name = name;
            Action = Execute;
        }


        /// <summary>
        /// Executes this words body.
        /// </summary>
        private int Execute()
        {
            // Get the actual word implementation and execute it.
            return Interpreter.GetWord(Name).Action();
        }
    }
}
