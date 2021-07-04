/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.ToolsExt
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// The CORE words library.
    /// </summary>
    public class Library : IWordsLibrary
    {
        public string Name => "TOOLS-EXT";

        private readonly IInterpreter _interpreter;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public Library(IInterpreter interpreter)
        {
            _interpreter = interpreter;
        }


        public void Initialize()
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

            _interpreter.ForgetWord(_interpreter.ParseWord());

            return 1;
        }
    }
}
