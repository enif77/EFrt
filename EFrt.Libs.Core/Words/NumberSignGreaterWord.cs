/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Drops xd and saves the PICTURED number to the object stack as a string.
    /// (xd -- ) { -- s}
    /// </summary>
    public class NumberSignGreaterWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public NumberSignGreaterWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "#>";
            Action = () =>
            {
                Interpreter.ObjectStackFree(1);

                // TODO: Check, that we are following the <# word.
                
                Interpreter.Drop(2);
                Interpreter.OPush(Interpreter.State.Picture);

                return 1;
            };
        }
    }
}