/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    /// <summary>
    /// Defines a library of words.
    /// </summary>
    public interface IWordsLIbrary
    {
        /// <summary>
        /// A name of a words library.
        /// </summary>
        string Name { get; }


        /// <summary>
        /// Defines words from this library.
        /// </summary>
        void DefineWords();
    }
}
