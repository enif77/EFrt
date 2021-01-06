/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Libs.Exception.Words
{
    using EFrt.Core;
    using EFrt.Core.Words;


    /// <summary>
    /// A word catching an exception.
    /// </summary>
    public class CatchWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A name of this value.</param>
        /// <param name="n">A value or an address.</param>
        public CatchWord(IInterpreter interpreter, IWord parentWord, int nextWordIndex)
            : base(interpreter)
        {
            Name = "catch";
            IsControlWord = true;
            Action = () => 
            {
                var exceptionFrame = new ExceptionFrame()
                {
                    StackTop = Interpreter.State.Stack.Top,
                    ObjectStackTop = Interpreter.State.ObjectStack.Top,
                    ReturnStackTop = Interpreter.State.ReturnStack.Top,
                    ExecutingWord = _parentWord,
                    NextWordIndex = _nextWordIndex
                };

                Interpreter.State.ExceptionStack.Push(exceptionFrame);

                try
                {
                    var index = Interpreter.State.WordsList.GetWord(Interpreter.Pop()).Action();
                    Interpreter.State.ExceptionStack.Pop();
                    Interpreter.Push(0);

                    return index;
                }
                catch (System.Exception ex)
                {
                    if (Interpreter.State.ExceptionStack.IsEmpty == false && Interpreter.State.ExceptionStack.Peek() == exceptionFrame)
                    {
                        Interpreter.State.ExceptionStack.Pop();
                    }

                    // TODO: What to do with ex?
                    Interpreter.Output.WriteLine(ex.Message);

                    // Go to the word behind us.
                    return 1;
                }
            };

            _parentWord = parentWord;
            _nextWordIndex = nextWordIndex;
        }

        private IWord _parentWord;
        private int _nextWordIndex;
    }
}
