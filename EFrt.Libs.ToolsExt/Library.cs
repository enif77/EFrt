/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.ToolsExt
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// The CORE words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "TOOLS-EXT";

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
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "BYE", ByeAction));
            _interpreter.AddWord(new PrimitiveWord(_interpreter, "FORGET", ForgetAction));
        }

        // ( -- )
        private int ByeAction()
        {
            _interpreter.TerminateExecution();

            return 1;
        }

        // ( -- )
        private int ForgetAction()
        {
            // Cannot forget a word, when a new word is currently compiled.
            if (_interpreter.IsCompiling)
            {
                throw new Exception("A word compilation is running.");
            }

            _interpreter.ForgetWord(_interpreter.GetWordName());

            return 1;
        }
    }
}
