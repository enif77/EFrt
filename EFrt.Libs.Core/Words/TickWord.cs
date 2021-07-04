/* EFrt - (C) 2021 Premysl Fara  */

using EFrt.Core.Extensions;

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Places the execution token of the following word on the stack.
    /// ( -- xt)
    /// </summary>
    public class TickWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public TickWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "'";
            Action = () =>
            {
                Interpreter.StackFree(1);

                var wordName = Interpreter.ParseWord();
                if (Interpreter.State.WordsList.IsDefined(wordName))
                {
                    Interpreter.Push(Interpreter.State.WordsList.Get(wordName).ExecutionToken);
                }
                else
                {
                    Interpreter.Throw(-2, $"The '{wordName}' word is not defined.");
                }

                return 1;                
            };
        }
    }
}