/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
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
        /// If this word is executed immediatelly.
        /// </summary>
        bool IsImmediate { get; }

        /// <summary>
        /// The action this word is doing.
        /// </summary>
        public Action Action { get; }

        /// <summary>
        /// The position of this word in the source.
        /// </summary>
        public int SourcePos { get; }
    }
}
