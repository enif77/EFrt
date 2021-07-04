/* EFrt - (C) 2021 Premysl Fara  */

using EFrt.Core.Extensions;

namespace EFrt.Libs.CoreExt.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;
    
    
    /// <summary>
    /// Checks, if n1 is between words n2 and n3.
    /// (n1 n2 n3 -- flag)
    /// </summary>
    public class WithinWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        public WithinWord(IInterpreter interpreter)
            : base(interpreter)
        {
            Name = "WITHIN";
            Action = () =>
            {
                Interpreter.StackExpect(3);

                var n3 = Interpreter.Pop();
                var n2 = Interpreter.Pop();

                if (n2 < n3)
                {
                    // Inside the interval.
                    var n1 = Interpreter.Pop();
                    Interpreter.Push((n2 <= n1 && n1 < n3) ? -1 : 0);
                }
                else if (n2 > n3)
                {
                    // Outside the interval.
                    var n1 = Interpreter.Pop();
                    Interpreter.Push((n2 <= n1 || n1 < n3) ? -1 : 0);
                }
                else
                {
                    Interpreter.Drop();   // n1
                    Interpreter.Push(0);  // false
                }

                return 1;
            };
        }
        
    }
}

/*
 
n1 n2 n3 -- flag

n2 < n3 && (n2 <= n1 && n1 < n3)  // n2 <= n1 < n3
  or
n2 > n3 && (n2 <= n1 || n1 < n3) 


Source: https://www.complang.tuwien.ac.at/forth/gforth/Docs-html/Numeric-comparison.html

u2 =< u1 < u3 
  or 
u3 =< u2 and u1 is not in [u3,u2)

This works for unsigned and signed numbers (but not a mixture). 
Another way to think about this word is to consider the numbers as 
a circle (wrapping around from max-u to 0 for unsigned, and from max-n 
to min-n for signed numbers); now consider the range from u2 towards 
increasing numbers up to and excluding u3 (giving an empty range if u2=u3); 
if u1 is in this range, within returns true.  
 
 */