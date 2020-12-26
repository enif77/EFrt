/* EFrt - (C) 2020 Premysl Fara  */

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
        

        #region tokenizer

        char CurrentChar { get; }

        int SourcePos { get; }


        /// <summary>
        /// Reads and returns the next character from the source.
        /// </summary>
        /// <returns>A character.</returns>
        char NextChar();


        bool IsWhite();

        bool IsDigit();


        /// <summary>
        /// Skips all white characters in the source from the current position.
        /// </summary>
        void SkipWhite();

        /// <summary>
        /// Extracts and returns the next token from the source.
        /// </summary>
        /// <returns>A token.</returns>
        Token NextTok();

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
        /// <returns>A new word name, that followed the word, which started the new word definition.</returns>
        string BeginNewWordCompilation();

        /// <summary>
        /// End a new word compilation and adds it into the known words list.
        /// </summary>
        void EndNewWordCompilation();

        #endregion


        #region stacks

        // Data stack

        /// <summary>
        /// Returns a value from the stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the stack.</returns>
        int Pick(int index);

        /// <summary>
        /// Returns a value from the top of the stack.
        /// </summary>
        /// <returns>A value from the top of the stack.</returns>
        int Peek();

        /// <summary>
        /// Removes a value from the top of the stack and returns it.
        /// </summary>
        /// <returns>A value from the top of the stack.</returns>
        int Pop();

        /// <summary>
        /// Inserts a value to the stack.
        /// </summary>
        /// <param name="value">A value.</param>
        void Push(int value);

        /// <summary>
        /// Drops N values from the stack.
        /// </summary>
        /// <param name="count">The number of values to be dropped from the stack.</param>
        void Drop(int count = 1);

        /// <summary>
        /// Duplicates the top value on the stack.
        /// ( a -- a a ) 
        /// </summary>
        void Dup();

        /// <summary>
        /// Swaps two values on the top of the stack.
        /// ( a b -- b a )
        /// </summary>
        void Swap();

        /// <summary>
        /// Gets a value below the top of the stack and pushes it to the stack.
        /// (a b -- a b a)
        /// </summary>
        void Over();

        /// <summary>
        /// Rotates the top three stack values.
        /// (a b c -- b c a)
        /// </summary>
        void Rot();

        /// <summary>
        /// Rotates indexth item to the top.
        /// </summary>
        /// <param name="index">A stack item index, where 0 = stack top, 1 = first below top, etc.</param>
        void Roll(int index);

        // Object stack

        /// <summary>
        /// Returns a value from the object stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the object stack.</returns>
        object OPick(int index);

        /// <summary>
        /// Returns a value from the top of the object stack.
        /// </summary>
        /// <returns>A value from the top of the object stack.</returns>
        object OPeek();

        /// <summary>
        /// Removes a value from the top of the object stack and returns it.
        /// </summary>
        /// <returns>A value from the top of the object stack.</returns>
        object OPop();

        /// <summary>
        /// Inserts a value to the object stack.
        /// </summary>
        /// <param name="value">A value.</param>
        void OPush(object value);

        /// <summary>
        /// Drops N values from the top of the object stack.
        /// </summary>
        /// <param name="count">The number of values to be dropped from the top of the object stack.</param>
        void ODrop(int count = 1);

        /// <summary>
        /// Duplicates the top value on the object stack.
        /// </summary>
        void ODup();

        /// <summary>
        /// Swaps two values on the top of the object stack.
        /// ( a b -- b a )
        /// </summary>
        void OSwap();

        /// <summary>
        /// Gets a value below the top of the stack and pushes it to the object stack.
        /// (a b -- a b a)
        /// </summary>
        void OOver();

        /// <summary>
        /// Rotates the top three object stack values.
        /// (a b c -- b c a)
        /// </summary>
        void ORot();

        /// <summary>
        /// Rotates indexth item on the object stack to the top of the object stack.
        /// </summary>
        /// <param name="index">A stack item index, where 0 = stack top, 1 = first below top, etc.</param>
        void ORoll(int index);

        // Return stack

        /// <summary>
        /// Returns a value from the return stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the return stack.</returns>
        int RPick(int index);

        /// <summary>
        /// Returns a value from the top of the return stack.
        /// </summary>
        /// <returns>A value from the top of the return stack.</returns>
        int RPeek();

        /// <summary>
        /// Removes a value from the top of the return stack and returns it.
        /// </summary>
        /// <returns>A value from the top of the return stack.</returns>
        int RPop();

        /// <summary>
        /// Inserts a value to the return stack.
        /// </summary>
        /// <param name="value">A value.</param>
        void RPush(int value);

        /// <summary>
        /// Drops N values from the top of the return stack.
        /// </summary>
        /// <param name="count">The number of values to be dropped from the top of the return stack.</param>
        void RDrop(int count = 1);

        /// <summary>
        /// Duplicates the top value on the return stack.
        /// </summary>
        void RDup();

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
