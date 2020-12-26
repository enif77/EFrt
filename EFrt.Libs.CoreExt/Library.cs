/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.CoreExt
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Words;

    using static EFrt.Core.Token;


    /// <summary>
    /// The CORE-EXT words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "CORE-EXT";

        private IInterpreter _interpreter;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter"></param>
        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        /// <summary>
        /// Definas words from this library.
        /// </summary>
        public void DefineWords()
        { 
        }
    }
}
