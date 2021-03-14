/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using EFrt.Core.Words;


    /// <summary>
    /// Kepps the list of known words.
    /// </summary>
    public class WordsList : IWordsList
    {
        /// <summary>
        /// Returns all currently defined words.
        /// </summary>
        public IEnumerable<IWord> DefinedWords
        {
            get
            {
                var wordsList = new List<IWord>();

                foreach (var w in _wordsDic.Keys)
                {
                    wordsList.Add(GetWord(w));
                }

                return wordsList;
            }
        }

        /// <summary>
        /// Returns the history of all defined words.
        /// </summary>
        public IEnumerable<IWord> WordsHistory => new List<IWord>(_wordsHistory);

        /// <summary>
        /// Constructor.
        /// </summary>
        public WordsList()
        {
            _wordsDic = new Dictionary<string, IList<IWord>>();
            _wordsHistory = new List<IWord>();
        }


        /// <summary>
        /// Removes all word definitions from this list.
        /// </summary>
        public void Clear()
        {
            _wordsDic.Clear();
            _wordsHistory.Clear();
        }

        /// <summary>
        /// Deletes the most recent definition of a word,
        /// along with all words declared more recently 
        /// than the named word.
        /// </summary>
        /// <param name="wordName">A name of a word.</param>
        public void Forget(string wordName)
        {
            // NOTE: A wordIndex is actually its execution token.
            var wordIndex = _wordsHistory.LastIndexOf(GetWord(wordName));
            var wordsToBeRemovedList = new List<IWord>();
            for (var i = wordIndex; i < _wordsHistory.Count; i++)
            {
                wordsToBeRemovedList.Add(_wordsHistory[i]);
            }

            foreach (var w in wordsToBeRemovedList)
            {
                RemoveWord(w.Name);
            }
        }

        /// <summary>
        /// Gets the latest definition of a word.
        /// </summary>
        /// <param name="wordName">A name of a word.</param>
        /// <returns>A word definition.</returns>
        public IWord GetWord(string wordName)
        {
            return _wordsDic[wordName].Last();
        }

        /// <summary>
        /// Gets a definition of a word by its execution token.
        /// </summary>
        /// <param name="executionToken">An execution token of a word.</param>
        /// <returns>A word definition.</returns>
        public IWord GetWord(int executionToken)
        {
            return _wordsHistory[executionToken];
        }

        /// <summary>
        /// Checks, if a word is defined.
        /// </summary>
        /// <param name="wordName">A name of a word.</param>
        /// <returns>True, if a word with a name wordName is defined.</returns>
        public bool IsWordDefined(string wordName)
        {
            return _wordsDic.ContainsKey(wordName);
        }

        /// <summary>
        /// Checks, if a word is defined.
        /// </summary>
        /// <param name="executionToken">An execution token of a word.</param>
        /// <returns>True, if a word with an execution token is defined.</returns>
        public bool IsWordDefined(int executionToken)
        {
            return executionToken >= 0 && executionToken < _wordsHistory.Count;
        }


        /// <summary>
        /// Registers a new word definition.
        /// </summary>
        /// <param name="word">A Word.</param>
        public void AddWord(IWord word)
        {
            if (IsWordDefined(word.Name) == false)
            {
                _wordsDic.Add(word.Name, new List<IWord>());
            }

            // Add the word to the list of words.
            _wordsDic[word.Name].Add(word);
            _wordsHistory.Add(word);

            // Set the execution token to the word.
            word.ExecutionToken = _wordsHistory.Count - 1;
        }

        /// <summary>
        /// Remove the latest definition of a word from this list of words.
        /// </summary>
        /// <param name="wordName">A name of a word.</param>
        public void RemoveWord(string wordName)
        {
            var wordDefinitionsList = _wordsDic[wordName];
            var wordTobeRemoved = wordDefinitionsList.Last();
            _wordsHistory.Remove(wordTobeRemoved);
            wordDefinitionsList.RemoveAt(wordDefinitionsList.Count - 1);
            if (wordDefinitionsList.Count == 0)
            {
                _wordsDic.Remove(wordName);
            }
            wordTobeRemoved.ExecutionToken = -1;
        }
        
        /// <summary>
        /// Returns the list of defined word names separated by SPACE.
        /// The last defined word is returned first.
        /// </summary>
        /// <returns>The list of defined word names separated by SPACE.</returns>
        public override string ToString()
        {
            var nextWord = false;
            var sb = new StringBuilder();
            var knownWords = new Dictionary<string, int>(_wordsDic.Count);
            for (var i = _wordsHistory.Count - 1; i >= 0; i--)
            {
                var word = _wordsHistory[i];
                if (knownWords.ContainsKey(word.Name))
                {
                    continue;
                }

                if (nextWord)
                {
                    sb.Append(" ");
                }
                else
                {
                    nextWord = true;
                }

                sb.Append(word.Name);
                knownWords.Add(word.Name, i);
            }

            return sb.ToString();
        }


        private readonly Dictionary<string, IList<IWord>> _wordsDic;
        private readonly List<IWord> _wordsHistory;
    }
}
