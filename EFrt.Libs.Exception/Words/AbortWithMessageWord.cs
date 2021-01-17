/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Exception.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word keeping a message and aborting program execution.
    /// </summary>
    public class AbortWithMessageWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
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
                    Interpreter.Throw(-2, _message);
                }
                
                return 1;
            };

            _message = message;
        }


        private string _message;
    }
}
