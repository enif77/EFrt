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
        /// Executes a string as a FORTH program.
        /// </summary>
        /// <param name="src">A FORTH program source.</param>
        void Execute(string src);

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
