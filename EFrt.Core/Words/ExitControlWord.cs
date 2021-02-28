/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    using EFrt.Core;


    /// <summary>
    /// A word that is exiting a word definition.
    /// </summary>
    public class ExitControlWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word..</param>
        /// <param name="definitionWord">A word, in which this DOES is used.</param>
        public ExitControlWord(IInterpreter interpreter, NonPrimitiveWord definitionWord)
            : base(interpreter)
        {
            Name = "EXIT";
            IsControlWord = true;
            Action = () => 
            {
                // Do not execute remaining words from the currently running word.
                _definitionWord.BreakExecution();

                return 1;
            };

            _definitionWord = definitionWord;
        }

        private readonly NonPrimitiveWord _definitionWord;
    }
}
