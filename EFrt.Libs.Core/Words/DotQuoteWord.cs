/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using System;
    
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Prints the string that follows in the input stream.
    /// ( -- )
    /// </summary>
    public class DotQuoteWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public DotQuoteWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = ".\"";
            IsImmediate = true;
            Action = () =>
            {
                if (Interpreter.IsCompiling == false)
                {
                    throw new Exception(".\" outside a new word definition.");
                }

                Interpreter.WordBeingDefined.AddWord(new PrintStringWord(Interpreter, Interpreter.ParseTerminatedString('"')));

                return 1;
            };
        }
    }
}

/*

\ Example:

( A simplified hello-world! )
: greet  ." Hello, I speak Forth!" CR ;

-> Hello, I speak Forth!
  
 */