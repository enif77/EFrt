﻿/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.ToolsExt
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Words;
    using static EFrt.Core.Token;


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

            // Get the name of a word.
            var tok = _interpreter.NextTok();
            switch (tok.Code)
            {
                case TokenType.Word:
                    _interpreter.ForgetWord(tok.SValue);
                    break;

                default:
                    throw new Exception($"A name of a word expected.");
            }

            return 1;
        }
    }
}