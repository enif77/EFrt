/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Exception.Words
{
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;


    /// <summary>
    /// A word catching an exception.
    /// </summary>
    public class CatchWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="parentWord">A word, that is executing this CATCH word.</param>
        /// <param name="nextWordIndex">An index of a word following this CATCH word..</param>
        public CatchWord(IInterpreter interpreter, IWord parentWord, int nextWordIndex)
            : base(interpreter)
        {
            Name = "CATCH";
            IsControlWord = true;
            Action = () => 
            {
                // Exception stack free.
                if ((interpreter.State.ExceptionStack.Count + 1) >= Interpreter.State.ExceptionStack.Items.Length)
                {
                    throw new InterpreterException(-3, "Exception stack overflow.");
                }

                Interpreter.StackExpect(1);

                var exceptionFrame = new ExceptionFrame()
                {
                    StackTop = Interpreter.State.Stack.Top,
                    ObjectStackTop = Interpreter.State.ObjectStack.Top,
                    ReturnStackTop = Interpreter.State.ReturnStack.Top,
                    InputSourceStackTop = Interpreter.State.InputSourceStack.Top,
                    ExecutingWord = _parentWord,
                    NextWordIndex = _nextWordIndex
                };

                Interpreter.State.ExceptionStack.Push(exceptionFrame);

                try
                {
                    // Execute the word.
                    var index = Interpreter.Execute(Interpreter.State.WordsList.Get(Interpreter.Pop()));

                    // Remove the unused exception frame (nothing failed here).
                    Interpreter.State.ExceptionStack.Pop();

                    // Return the OK status.
                    Interpreter.Push(0);

                    return index;
                }
                catch (System.Exception)
                {
                    // Clean up the mess, if needed.
                    if (Interpreter.State.ExceptionStack.IsEmpty == false && Interpreter.State.ExceptionStack.Peek() == exceptionFrame)
                    {
                        Interpreter.State.ExceptionStack.Pop();
                    }

                    // TODO: What to do with the exception?

                    // Go to the word behind us.
                    return 1;
                }
            };

            _parentWord = parentWord;
            _nextWordIndex = nextWordIndex;
        }

        private readonly IWord _parentWord;
        private readonly int _nextWordIndex;
    }
}
