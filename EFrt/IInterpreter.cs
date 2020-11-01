/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using EFrt.Words;


    public interface IInterpreter
    {
        /// <summary>
        /// True, if this interpreter is actually compilling a new word.
        /// </summary>
        public bool IsCompiling { get; }


        /// <summary>
        /// Returns true, if a word is defined;
        /// </summary>
        /// <param name="wordName">A word name.</param>
        /// <returns></returns>
        bool IsWordDefined(string wordName);

        /// <summary>
        /// Gets a defined word.
        /// Throws an exception, if no such word is defined.
        /// </summary>
        /// <param name="wordName">A word name.</param>
        /// <returns>A word.</returns>
        IWord GetWord(string wordName);

        /// <summary>
        /// Adds a word to the words list.
        /// </summary>
        /// <param name="word">A word.</param>
        void AddWord(IWord word);

        /// <summary>
        /// Removes a word from the list of words.
        /// Throws an exception, if no such word is defined.
        /// </summary>
        /// <param name="wordName">A word name.</param>
        void RemoveWord(string wordName);
    }
}
