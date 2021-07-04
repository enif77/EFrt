/* EFrt - (C) 2020 - 2021 Premysl Fara  */

using EFrt.Core.Extensions;

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word, that is defining the do loop beginning.
    /// </summary>
    public class DoControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public DoControlWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "DO";
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.StackExpect(2);
                Interpreter.ReturnStackFree(2);

                var index = Interpreter.Pop();

                Interpreter.RPush(Interpreter.Pop());  // limit
                Interpreter.RPush(index);              // index

                return 1;
            };
        }
    }
}
