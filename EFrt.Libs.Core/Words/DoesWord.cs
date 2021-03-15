/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// The runtime part of the DOES> word.
    /// </summary>
    public class DoesWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="definitionWord">A word, in which this DOES is used.</param>
        /// <param name="doesIndex">An index of the DOES word inside of the definitionWord. Used to find the first word, that belongs to the newly CREATEd word.</param>
        public DoesWord(IInterpreter interpreter, NonPrimitiveWord definitionWord, int doesIndex)
            : base(interpreter)
        {
            Name = "DOES";
            IsControlWord = true;
            Action = () => 
            {
                // The currently defined word should be the one thats CREATEd.
                var createdWord = Interpreter.WordBeingDefined as CreatedWord;
                if (createdWord == null)
                {
                    throw new Exception("A CREATEd word expected.");
                }

                // Add the "get-data-field-start" word.
                createdWord.AddWord(new SingleCellIntegerLiteralWord(Interpreter, createdWord.DataFieldIndex));

                // Copy runtime words to the CREATEd one.
                for (var i = _doesIndex + 1; i < _definitionWord.WordsCount; i++)
                {
                    // TODO: Clone definitions words!
                    createdWord.AddWord(_definitionWord.GetWord(i));
                }

                // And use them for the further execution.
                createdWord.ActivateDefinedWords();

                // Do not execute remaining words from the currently running word.
                _definitionWord.BreakExecution();

                return 1;
            };

            _definitionWord = definitionWord;
            _doesIndex = doesIndex;
        }


        readonly NonPrimitiveWord _definitionWord;
        private readonly int _doesIndex;
    }
}
