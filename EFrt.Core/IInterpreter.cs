/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;

    using EFrt.Core.Words;

/*

https://www.root.cz/serialy/programovaci-jazyk-forth/
https://www.forth.com/starting-forth/
https://en.wikipedia.org/wiki/Forth_(programming_language)
https://www.fourmilab.ch/atlast/atlast.html
http://users.ece.cmu.edu/~koopman/stack_computers/

https://csharppedia.com/en/tutorial/5626/how-to-use-csharp-structs-to-create-a-union-type-similar-to-c-unions-

*/

    /// <summary>
    /// Defines a FORTH interpreter.
    /// </summary>
    public interface IInterpreter
    {
        /// <summary>
        /// Runtime data of this interpreter instance.
        /// </summary>
        IInterpreterState State { get; }

        /// <summary>
        /// The current input source.
        /// </summary>
        IInputSource InputSource { get; }

        /// <summary>
        /// An output writer for IO operations.
        /// </summary>
        IOutputWriter Output { get; set; }

        /// <summary>
        /// True, if this interpreter is compiling a new word, variable or constant.
        /// </summary>
        bool IsCompiling { get; }

        /// <summary>
        /// True, if this program execution is currently termineted.
        /// </summary>
        bool IsExecutionTerminated { get; }

        /// <summary>
        /// The state, in which is this interpreter.
        /// </summary>
        InterpreterStateCode InterpreterState { get; }

        /// <summary>
        /// An event fired, when a word is being executed.
        /// </summary>
        event EventHandler<InterpreterEventArgs> ExecutingWord;

        /// <summary>
        /// An event fired, when a word was executed.
        /// </summary>
        event EventHandler<InterpreterEventArgs> WordExecuted;


        #region word compilation

        /// <summary>
        /// Returns a word, that is actually compiled (if IsCompiling is true), or the last user defined word (if IsCompiling is false).
        /// </summary>
        NonPrimitiveWord WordBeingDefined { get; set; }


        /// <summary>
        /// Begins a new word compilation.
        /// </summary>
        void BeginNewWordCompilation();

        /// <summary>
        /// Suspends a new word compilation.
        /// Call the ResumeNewWordCompilation() method to return to the Compiling state.
        /// </summary>
        void SuspendNewWordCompilation();

        /// <summary>
        /// Resumes a new word compilation.
        /// </summary>
        void ResumeNewWordCompilation();

        /// <summary>
        /// End a new word compilation and adds it into the known words list.
        /// </summary>
        void EndNewWordCompilation();

        #endregion


        #region execution

        /// <summary>
        /// Cleans up the internal interpreters state.
        /// </summary>
        void Reset();

        /// <summary>
        /// Clears the stack and the object stack and calls the Quit() method.
        /// </summary>
        void Abort();

        /// <summary>
        /// The return stack is cleared and control is returned to the interpreter. The stack and the object stack are not disturbed.
        /// </summary>
        void Quit();

        /// <summary>
        /// Throws an system exception based on the exception code.
        /// </summary>
        /// <param name="exceptionCode">An exception code.</param>
        /// <param name="message">An optional exception message.</param>
        void Throw(int exceptionCode, string message = null);

        /// <summary>
        /// Interprets a FORTH program using a source reader.
        /// </summary>
        /// <param name="sourceReader">A FORTH program source reader.</param>
        void Evaluate(ISourceReader sourceReader);

        /// <summary>
        /// Executes a word. 
        /// Call this for a each word execution.
        /// </summary>
        /// <param name="word">A word to be executed.</param>
        /// <returns>A next word index.</returns>
        int Execute(IWord word);

        /// <summary>
        /// Asks the interpreter to terminate the current script execution.
        /// </summary>
        void TerminateExecution();

        #endregion
    }
}
