/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using EFrt.Words;


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
        public IEnumerable<IWord> WordsHistory
        {
            get
            {
                return new List<IWord>(_wordsHistory);
            }
        }

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
            var wIndex = _wordsHistory.LastIndexOf(GetWord(wordName));
            var wList = new List<IWord>();
            for (var i = wIndex; i < _wordsHistory.Count; i++)
            {
                wList.Add(_wordsHistory[i]);
            }

            foreach (var w in wList)
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
        /// Checks, if a word is defined.
        /// </summary>
        /// <param name="wordName">A name of a word.</param>
        /// <returns>True, if a word with a name wordName is defined.</returns>
        public bool IsWordDefined(string wordName)
        {
            return _wordsDic.ContainsKey(wordName);
        }

        

        /// <summary>
        /// Registers a new word definition.
        /// </summary>
        /// <param name="word">A Word.</param>
        public void RegisterWord(IWord word)
        {
            if (IsWordDefined(word.Name) == false)
            {
                _wordsDic.Add(word.Name, new List<IWord>());
            }

            _wordsDic[word.Name].Add(word);
            _wordsHistory.Add(word);
        }

        /// <summary>
        /// Remove the latest definition of a word from this list of words.
        /// </summary>
        /// <param name="wordName">A name of a word.</param>
        public void RemoveWord(string wordName)
        {
            var wordDefinitionsList = _wordsDic[wordName];
            _wordsHistory.Remove(wordDefinitionsList.Last());
            wordDefinitionsList.RemoveAt(wordDefinitionsList.Count - 1);
            if (wordDefinitionsList.Count == 0)
            {
                _wordsDic.Remove(wordName);
            }
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
            var wdic = new Dictionary<string, int>(_wordsDic.Count);
            for (var i = _wordsHistory.Count - 1; i >= 0; i--)
            {
                var w = _wordsHistory[i];
                if (wdic.ContainsKey(w.Name))
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

                sb.Append(w.Name);
                wdic.Add(w.Name, i);
            }

            return sb.ToString();
        }


        private Dictionary<string, IList<IWord>> _wordsDic;
        private List<IWord> _wordsHistory;
    }
}
