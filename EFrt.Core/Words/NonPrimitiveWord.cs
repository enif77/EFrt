﻿/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    using System.Collections.Generic;
    
    using EFrt.Core.Extensions;


    /// <summary>
    /// A compound word.
    /// </summary>
    public class NonPrimitiveWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        /// <param name="name">A  words name.</param>
        public NonPrimitiveWord(IInterpreter interpreter, string name)
            : base(interpreter)
        {
            Name = name;
            Action = Execute;

            _words = new List<IWord>();
        }


        /// <summary>
        /// Returns the index of the next word, that will be inserted into this word.
        /// </summary>
        public int NextWordIndex => _words.Count;

        /// <summary>
        /// Marks this word as immediate.
        /// </summary>
        public void SetImmediate()
        {
            IsImmediate = true;
        }

        /// <summary>
        /// Gets the number of words defined in this word.
        /// </summary>
        public int WordsCount => _words.Count;

        /// <summary>
        /// Returns a word defined at index.
        /// </summary>
        /// <param name="index">An zero based index of a word defined as part of this word body.</param>
        /// <returns>A word defined at index.</returns>
        public IWord GetWord(int index)
        {
            return _words[index];
        }

        /// <summary>
        /// Adds a word to this word definition.
        /// </summary>
        /// <param name="word"></param>
        /// <returns>Index of the added word.</returns>
        public int AddWord(IWord word)
        {
            _words.Add(word);

            return _words.Count - 1;
        }

        /// <summary>
        /// Executes this words body.
        /// </summary>
        protected int Execute()
        {
            _executionBreaked = false;
            var index = 0;
            while (index <= _words.Count)
            {
                if (Interpreter.IsExecutionTerminated)
                {
                    break;
                }

                var word = _words[index];
                index += Interpreter.Execute(word.IsControlWord 
                    ? word                                       // Control and value words are defined by themselves.
                    : Interpreter.GetWord(_words[index].Name));  // Get the actual word implementation and execute it.

                // Used by the DoesWord.
                if (_executionBreaked)
                {
                    // Recursive words calls need this "kill switch" to be used just once.
                    _executionBreaked = false;

                    break;
                }
            }

            return 1;
        }

        /// <summary>
        /// If called, no more words from this word are executed.
        /// </summary>
        public void BreakExecution()
        {
            _executionBreaked = true;
        }


        /// <summary>
        /// A list of words defining this word.
        /// </summary>
        private readonly IList<IWord> _words;

        /// <summary>
        /// If true, no more words are executed.
        /// </summary>
        private bool _executionBreaked;
    }
}
