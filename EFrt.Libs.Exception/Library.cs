/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Exception
{
    using System;

    using EFrt.Core;

    using EFrt.Libs.Exception.Words;


    /// <summary>
    /// The EXCEPTION words library.
    /// </summary>
    public class Library : IWordsLIbrary
    {
        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name => "EXCEPTION";

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
            _interpreter.AddImmediateWord("CATCH", CatchAction);
            _interpreter.AddPrimitiveWord("THROW", ThrowAction);

            _interpreter.AddPrimitiveWord("ABORT", AbortAction);
            _interpreter.AddImmediateWord("ABORT\"", AbortWithMessageAction);
        }

        
        // (xt -- )
        private int CatchAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("CATCH outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(new CatchWord(_interpreter, _interpreter.WordBeingDefined, _interpreter.WordBeingDefined.NextWordIndex));

            return 1;
        }

        // (n -- )
        private int ThrowAction()
        {
            _interpreter.StackExpect(1);

            // Wont return (throws an exception) when n is not zero and the CATCH word was executed.
            _interpreter.Throw(_interpreter.Pop());

            return 1;
        }

        // EXT

        // ( -- )
        public int AbortAction()
        {
            _interpreter.Throw(-1);

            return 1;
        }

        // (flag -- )
        private int AbortWithMessageAction()
        {
            if (_interpreter.IsCompiling == false)
            {
                throw new Exception("ABORT\" outside a new word definition.");
            }

            _interpreter.WordBeingDefined.AddWord(new AbortWithMessageWord(_interpreter, _interpreter.GetTerminatedString('"')));

            return 1;
        }
    }
}
