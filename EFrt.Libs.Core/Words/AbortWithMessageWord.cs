﻿/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;


    /// <summary>
    /// A word keeping a message and aborting program execution.
    /// </summary>
    public class AbortWithMessageWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="message">A message.</param>
        public AbortWithMessageWord(IInterpreter interpreter, string message)
            : base(interpreter)
        {
            Name = "ABORT";
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.StackExpect(1);

                if (Interpreter.Pop() != 0)
                {
                    Interpreter.Output.WriteLine(_message);

                    Interpreter.Abort();
                }

                return 1;
            };

            _message = message;
        }


        private readonly string _message;
    }
}
