/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using System;
    
    using EFrt.Core;
    using EFrt.Core.Words;

    
    /// <summary>
    /// Parse ccc delimited by ) (right parenthesis). ( is an immediate word.
    /// The number of characters in ccc may be zero to the number of characters in the parse area.
    /// ( -- )
    /// </summary>
    public class ParenWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public ParenWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "(";
            IsImmediate = true;
            Action = () =>
            {
                var c = Interpreter.NextChar();
                while (Interpreter.CurrentChar() != 0)
                {
                    if (Interpreter.CurrentChar() == ')')
                    {
                        Interpreter.NextChar();

                        break;
                    }

                    c = Interpreter.NextChar();
                }

                if (c != ')')
                {
                    throw new Exception("')' expected.");
                }

                return 1;
            };
        }
    }
}

/*

\ Example:

123 ( This is a comment) 3 * .

-> 369 
  
 */