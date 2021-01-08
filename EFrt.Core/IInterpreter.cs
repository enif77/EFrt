/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
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
        

        #region parsing

        /// <summary>
        /// The last character red from the source.
        /// </summary>
        char CurrentChar { get; }

        /// <summary>
        /// A position in the source in consumed characters.
        /// </summary>
        int SourcePos { get; }


        /// <summary>
        /// Reads and returns the next character from the source.
        /// </summary>
        /// <returns>A character.</returns>
        char NextChar();

        /// <summary>
        /// Returns a word name following in the input stream.
        /// </summary>
        /// <param name="toUpperCase">If true (the default), returned word is converted to UPPERCASE.</param>
        /// <returns>A word name.</returns>
        string GetWordName(bool toUpperCase = true);

        /// <summary>
        /// Gets a string terminated by a terminator char.
        /// </summary>
        /// <param name="terminator">A character terminating the string.</param>
        /// <returns>A string.</returns>
        string GetTerminatedString(char terminator);

        /// <summary>
        /// Parses an integer number from a string.
        /// </summary>
        /// <param name="s">A string containing a number.</param>
        /// <param name="success">True, if parsing succeeded.</param>
        /// <returns>A parsed number.</returns>
        long ParseNumber(string s, out bool success);

        /// <summary>
        /// Parses a floating point number from a string.
        /// </summary>
        /// <param name="s">A string containing a number.</param>
        /// <param name="success">True, if parsing succeeded.</param>
        /// <returns>A parsed number.</returns>
        double ParseFloatingPointNumber(string s, out bool success);

        #endregion


        #region words list

        /// <summary>
        /// Returns true, if a word is defined;
        /// </summary>
        /// <param name="wordName">A word name.</param>
        /// <returns></returns>
        bool IsWordDefined(string wordName);

        /// <summary>
        /// Gets a defined word.
        /// Throws an exception, if no such word is defined.
        /// </summary>
        /// <param name="wordName">A word name.</param>
        /// <returns>A word.</returns>
        IWord GetWord(string wordName);

        /// <summary>
        /// Adds a word to the words list.
        /// </summary>
        /// <param name="word">A word.</param>
        void AddWord(IWord word);

        /// <summary>
        /// Addss words from a words library.
        /// </summary>
        /// <param name="library">A library of words.</param>
        void AddWords(IWordsLIbrary library);

        /// <summary>
        /// Forgets a word and all word defined after it.
        /// </summary>
        /// <param name="wordName">A word name.</param>
        void ForgetWord(string wordName);

        /// <summary>
        /// Removes a word from the list of words.
        /// Throws an exception, if no such word is defined.
        /// </summary>
        /// <param name="wordName">A word name.</param>
        void RemoveWord(string wordName);

        /// <summary>
        /// Removes all words from the list of words.
        /// </summary>
        void RemoveAllWords();

        #endregion


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


        #region stack checks

        /// <summary>
        /// Expects N items on the stack.
        /// Wont return (throws an InterpreterException), if not enough items are on the stack.
        /// </summary>
        /// <param name="expectedItemsCount">The number of stack items expected on the stack.</param>
        void StackExpect(int expectedItemsCount);

        /// <summary>
        /// Expects N free items on the stack, so N items can be pushed to the stack.
        /// Wont return (throws an InterpreterException), if there is not enough free items on the stack.
        /// </summary>
        /// <param name="expectedFreeItemsCount">The number of free stack items expected.</param>
        void StackFree(int expectedFreeItemsCount);

        /// <summary>
        /// Expects N items on the object stack.
        /// Wont return (throws an InterpreterException), if not enough items are on the object stack.
        /// </summary>
        /// <param name="expectedItemsCount">The number of stack items expected on the object stack.</param>
        void ObjectStackExpect(int expectedItemsCount);

        /// <summary>
        /// Expects N free items on the object stack, so N items can be pushed to the object stack.
        /// Wont return (throws an InterpreterException), if there is not enough free items on the object stack.
        /// </summary>
        /// <param name="expectedFreeItemsCount">The number of free object stack items expected.</param>
        void ObjectStackFree(int expectedFreeItemsCount);

        /// <summary>
        /// Expects N items on the return stack.
        /// Wont return (throws an InterpreterException), if not enough items are on the return stack.
        /// </summary>
        /// <param name="expectedItemsCount">The number of stack items expected on the return stack.</param>
        void ReturnStackExpect(int expectedItemsCount);

        /// <summary>
        /// Expects N free items on the return stack, so N items can be pushed to the return stack.
        /// Wont return (throws an InterpreterException), if there is not enough free items on the return stack.
        /// </summary>
        /// <param name="expectedFreeItemsCount">The number of free return stack items expected.</param>
        void ReturnStackFree(int expectedFreeItemsCount);

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
        /// Executes a FORTH program using a source reader.
        /// </summary>
        /// <param name="sourceReader">A FORTH program source reader.</param>
        void Execute(ISourceReader sourceReader);

        /// <summary>
        /// Asks the interpreter to break the current script execution.
        /// </summary>
        void BreakExecution();

        /// <summary>
        /// Asks the interpreter to terminate the current script execution.
        /// </summary>
        void TerminateExecution();

        #endregion
    }
}
