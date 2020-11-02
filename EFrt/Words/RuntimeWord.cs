/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
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
        public RuntimeWord(IInterpreter interpreter, string name, int sourcePos = -1)
            : base(interpreter)
        {
            Name = name;
            IsImmediate = false;
            Action = Execute;
            SourcePos = sourcePos;
        }


        /// <summary>
        /// Executes this words body.
        /// </summary>
        private void Execute()
        {
            // Get the actual word implementation and execute it.
            Interpreter.GetWord(Name).Action();
        }
    }
}
