﻿/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    using System.Collections.Generic;

    using EFrt.Core.Words;


    /// <summary>
    /// Kepps the list of known words.
    /// </summary>
    public interface IWordsList
    {
        /// <summary>
        /// Returns all currently defined words.
        /// </summary>
        IEnumerable<IWord> DefinedWords { get; }

        /// <summary>
        /// Returns the history of all defined words.
        /// </summary>
        IEnumerable<IWord> WordsHistory { get; }


        /// <summary>
        /// Removes all word definitions from this list.
        /// </summary>
        void Clear();

        /// <summary>
        /// Deletes the most recent definition of a word,
        /// along with all words declared more recently 
        /// than the named word.
        /// </summary>
        /// <param name="wordName">A name of a word.</param>
        void Forget(string wordName);

        /// <summary>
        /// Gets the latest definition of a word.
        /// </summary>
        /// <param name="wordName">A name of a word.</param>
        /// <returns>A word definition.</returns>
        IWord Get(string wordName);

        /// <summary>
        /// Gets a definition of a word by its execution token.
        /// </summary>
        /// <param name="executionToken">An execution token of a word.</param>
        /// <returns>A word definition.</returns>
        IWord Get(int executionToken);

        /// <summary>
        /// Checks, if a word is defined.
        /// </summary>
        /// <param name="wordName">A name of a word.</param>
        /// <returns>True, if a word with a name wordName is defined.</returns>
        bool IsDefined(string wordName);

        /// <summary>
        /// Checks, if a word is defined.
        /// </summary>
        /// <param name="executionToken">An execution token of a word.</param>
        /// <returns>True, if a word with an execution token is defined.</returns>
        bool IsDefined(int executionToken);

        /// <summary>
        /// Adds a new word definition.
        /// </summary>
        /// <param name="word">A Word.</param>
        void Add(IWord word);

        /// <summary>
        /// Remove the latest definition of a word from this list of words.
        /// </summary>
        /// <param name="wordName">A name of a word.</param>
        void Remove(string wordName);

        /// <summary>
        /// Returns the list of defined word names separated by SPACE.
        /// The last defined word is returned first.
        /// </summary>
        /// <returns>The list of defined word names separated by SPACE.</returns>
        string ToString();
    }
}