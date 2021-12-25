/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    using System;


    /// <summary>
    /// A word definition.
    /// </summary>
    public interface IWord
    {
        /// <summary>
        /// A name of this word.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// If this word is executed immediately.
        /// </summary>
        bool IsImmediate { get; }

        /// <summary>
        /// Control words have actions, but are not in the words list.
        /// </summary>
        bool IsControlWord { get; }

        /// <summary>
        /// An execution token. Used by the EXECUTE word to find a words definition for execution.
        /// Its set by the IWordsList.AddWord() and the IWordsList.RemoveWord() methods.
        /// </summary>
        int ExecutionToken { get; set; }

        /// <summary>
        /// The action this word is doing.
        /// </summary>
        Func<int> Action { get; }
    }
}
