/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Words
{
    /// <summary>
    /// A compound word.
    /// </summary>
    public class NonPrimitiveWord : AWordBase
    {
        private const int WordsListChunkSize = 4;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A  words name.</param>
        public NonPrimitiveWord(IInterpreter interpreter, string name)
            : base(interpreter)
        {
            Name = name;
            Action = Execute;

            _words = new IWord[WordsListChunkSize];
            _lastWordIndex = -1;
        }


        /// <summary>
        /// Returns the index of the next word, that will be inserted into this word.
        /// </summary>
        public int NextWordIndex => _lastWordIndex + 1;

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
        public int WordsCount => _lastWordIndex + 1;

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
            _lastWordIndex++;
            if (_lastWordIndex >= _words.Length)
            {
                var oldWords = _words;
                _words = new IWord[oldWords.Length + WordsListChunkSize];

                for (var i = 0; i < oldWords.Length; i++)
                {
                    _words[i] = oldWords[i];
                }
            }

            _words[_lastWordIndex] = word;

            return _lastWordIndex;
        }

        /// <summary>
        /// Executes this words body.
        /// </summary>
        protected int Execute()
        {
            _executionBreaked = false;
            var index = 0;
            while (index <= _lastWordIndex)
            {
                if (Interpreter.IsExecutionTerminated)
                {
                    break;
                }

                var word = _words[index];
                index += word.IsControlWord 
                    ? word.Action()                                      // Control and value words are defined by themselves.
                    : Interpreter.GetWord(_words[index].Name).Action();  // Get the actual word implementation and execute it.

                // Used by the DoesWord.
                if (_executionBreaked)
                {
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
        private IWord[] _words;

        /// <summary>
        /// The index of the last word inserted to this word definition.
        /// </summary>
        private int _lastWordIndex;

        /// <summary>
        /// If true, no more words are executed.
        /// </summary>
        private bool _executionBreaked;
    }
}
