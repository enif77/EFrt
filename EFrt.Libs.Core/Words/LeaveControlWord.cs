/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word that is leaving a loop.
    /// </summary>
    public class LeaveControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public LeaveControlWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "LEAVE";
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.ReturnStackExpect(2);

                // Remove the current index...
                _ = Interpreter.RPop();

                // and replace it with the limit, which effectively marks the end of the loop.
                Interpreter.RDup();

                return 1;
            };
        }
    }
}
