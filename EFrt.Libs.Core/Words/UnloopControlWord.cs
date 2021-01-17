/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is discarding loop control parameters.
    /// </summary>
    public class UnloopControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public UnloopControlWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "UNLOOP";
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.ReturnStackExpect(2);

                // Remove the limit and the index.
                Interpreter.RDrop(2);

                return 1;
            };
        }
    }
}
