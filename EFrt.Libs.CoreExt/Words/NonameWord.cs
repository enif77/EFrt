/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Libs.CoreExt.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A NONAME word.
    /// </summary>
    public class NonameWord : NonPrimitiveWord
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NonameWord(IInterpreter interpreter)
            : base(interpreter, " ")   // Users cannot define a word with a white character (SPACE) in its name.
        {
        }
    }
}
