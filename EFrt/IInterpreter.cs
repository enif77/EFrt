/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using System;

    using EFrt.Words;
    

    public interface IInterpreter
    {
        /// <summary>
        /// True, if this interpreter is actually compilling a new word.
        /// </summary>
        public bool IsCompiling { get; }

        /// <summary>
        /// Returns true, if the interpreter should terminate the current script execution.
        /// </summary>
        bool IsExecutionTerminated { get; }


        /// <summary>
        /// Asks the interpreter to terminate the current script execution.
        /// </summary>
        void TerminateExecution();


        /// <summary>
        /// Does a stack operation with a single parameter. The operation result is stored at the top of the stack.
        /// </summary>
        /// <param name="func">A function.</param>
        void Function(Func<EfrtValue, EfrtValue> func);

        /// <summary>
        /// Does a stack operation with two parameters. The operation result is stored at the top of the stack.
        /// </summary>
        /// <param name="func">A function.</param>
        void Function(Func<EfrtValue, EfrtValue, EfrtValue> func);


        #region tokenizer

        char CurrentChar { get; }

        int SourcePos { get; }


        /// <summary>
        /// Reads and returns the next character from the source.
        /// </summary>
        /// <returns>A character.</returns>
        char NextChar();

        /// <summary>
        /// Extracts and returns the next token from the source.
        /// </summary>
        /// <returns>A token.</returns>
        Token NextTok();

        #endregion


        #region word compilation

        /// <summary>
        /// Returns a word, that we are actually compileing.
        /// </summary>
        NonPrimitiveWord WordBeingDefined { get; }


        /// <summary>
        /// Begins a new word compilation.
        /// </summary>
        void BeginNewWordCompilation();

        /// <summary>
        /// End a new word compilation and adds it into the known words list.
        /// </summary>
        void EndNewWordCompilation();

        #endregion


        #region stacks

        /// <summary>
        /// Returns a word from the control flow stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A word from the control flow stack.</returns>
        IWord CGet(int index);

        /// <summary>
        /// Returns a word from the top of the control flow stack.
        /// </summary>
        /// <returns>A word from the top of the control flow stack.</returns>
        IWord CPeek();

        /// <summary>
        /// Removes a word from the top of the control flow stack and returns it.
        /// </summary>
        /// <returns>A word from the top of the control flow stack.</returns>
        IWord CPop();

        /// <summary>
        /// Inserts a word to the control flow stack.
        /// </summary>
        /// <param name="word">A word.</param>
        void CPush(IWord word);


        /// <summary>
        /// Returns a value from the stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the stack.</returns>
        EfrtValue Get(int index);

        /// <summary>
        /// Returns a value from the top of the stack.
        /// </summary>
        /// <returns>A value from the top of the stack.</returns>
        EfrtValue Peek();

        /// <summary>
        /// Returns an integer value from the top of the stack.
        /// </summary>
        /// <returns>An integer value from the top of the stack.</returns>
        int Peeki();

        /// <summary>
        /// Returns a float value from the top of the stack.
        /// </summary>
        /// <returns>An float value from the top of the stack.</returns>
        float Peekf();

        /// <summary>
        /// Removes a value from the top of the stack and returns it.
        /// </summary>
        /// <returns>A value from the top of the stack.</returns>
        EfrtValue Pop();

        /// <summary>
        /// Removes an integer value from the top of the stack and returns it.
        /// </summary>
        /// <returns>An integer value from the top of the stack.</returns>
        int Popi();

        /// <summary>
        /// Removes a float value from the top of the stack and returns it.
        /// </summary>
        /// <returns>A vfloat alue from the top of the stack.</returns>
        float Popf();

        /// <summary>
        /// Inserts a value to the stack.
        /// </summary>
        /// <param name="value">A value.</param>
        void Push(EfrtValue value);

        /// <summary>
        /// Inserts an integer value to the stack.
        /// </summary>
        /// <param name="value">An integer value.</param>
        void Pushi(int i);

        /// <summary>
        /// Inserts a float value to the stack.
        /// </summary>
        /// <param name="value">A float value.</param>
        void Pushf(float d);

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
        /// Returns a value from the return stack at certain index.
        /// </summary>
        /// <param name="index">A value index.</param>
        /// <returns>A value from the return stack.</returns>
        int RGet(int index);

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
        /// Drops N values from the return stack.
        /// </summary>
        /// <param name="count">The number of values to be dropped from the return stack.</param>
        void RDrop(int count = 1);

        /// <summary>
        /// Duplicates the top value on the return stack.
        /// </summary>
        void RDup();

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
        /// Removes a word from the list of words.
        /// Throws an exception, if no such word is defined.
        /// </summary>
        /// <param name="wordName">A word name.</param>
        void RemoveWord(string wordName);

        #endregion
    }
}
