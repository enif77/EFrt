/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    /// <summary>
    /// Defines a library of words.
    /// </summary>
    public interface IWordsLibrary
    {
        /// <summary>
        /// A name of a words library.
        /// </summary>
        string Name { get; }


        /// <summary>
        /// Initializes this library and defines words from this library.
        /// </summary>
        void Initialize();
    }
}
