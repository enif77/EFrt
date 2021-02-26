/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;

    using EFrt.Core.Words;


    /// <summary>
    /// Extension methods for the IInterpreter interface.
    /// </summary>
    public static class InterpreterExtensions
    {
        #region words list

        /// <summary>
        /// Returns true, if a word is defined;
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="wordName">A word name.</param>
        /// <returns></returns>
        public static bool IsWordDefined(this IInterpreter interpreter, string wordName)
        {
            if (string.IsNullOrEmpty(wordName)) throw new ArgumentException("A word name expected.");
            
            return interpreter.State.WordsList.IsWordDefined(wordName.ToUpperInvariant());
        }

        /// <summary>
        /// Gets a defined word.
        /// Throws an exception, if no such word is defined.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="wordName">A word name.</param>
        /// <returns>A word.</returns>
        public static IWord GetWord(this IInterpreter interpreter, string wordName)
        {
            return interpreter.State.WordsList.GetWord(wordName.ToUpperInvariant());
        }

        /// <summary>
        /// Adds a word to the words list.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="word">A word.</param>
        public static void AddWord(this IInterpreter interpreter, IWord word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));

            interpreter.State.WordsList.AddWord(word);
        }

        /// <summary>
        /// Forgets a word and all word defined after it.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="wordName">A word name.</param>
        public static void ForgetWord(this IInterpreter interpreter, string wordName)
        {
            if (string.IsNullOrEmpty(wordName)) throw new ArgumentException("A word name expected.");
            
            interpreter.State.WordsList.Forget(wordName.ToUpperInvariant());
        }

        /// <summary>
        /// Removes a word from the list of words.
        /// Throws an exception, if no such word is defined.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="wordName">A word name.</param>
        public static void RemoveWord(this IInterpreter interpreter, string wordName)
        {
            if (string.IsNullOrEmpty(wordName)) throw new ArgumentException("A word name expected.");
            
            interpreter.State.WordsList.RemoveWord(wordName.ToUpperInvariant());
        }

        /// <summary>
        /// Removes all words from the list of words.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void RemoveAllWords(this IInterpreter interpreter)
        {
            interpreter.State.WordsList.Clear();
        }
        
        /// <summary>
        /// Adds a primitive word to the words list.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="name">A new word name.</param>
        /// <param name="action">An action, this word is doing.</param>
        public static void AddPrimitiveWord(this IInterpreter interpreter, string name, Func<int> action)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A word name expected.");
            if (action == null) throw new ArgumentNullException(nameof(action));

            interpreter.AddWord(new PrimitiveWord(interpreter, name, action));
        }

        /// <summary>
        /// Adds an immediate word to the words list.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="name">A new word name.</param>
        /// <param name="action">An action, this word is doing.</param>
        public static void AddImmediateWord(this IInterpreter interpreter, string name, Func<int> action)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A word name expected.");
            if (action == null) throw new ArgumentNullException(nameof(action));

            interpreter.AddWord(new ImmediateWord(interpreter, name, action));
        }

        /// <summary>
        /// Adds a constant word to the words list.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="name">A new word name.</param>
        /// <param name="n">A value.</param>
        public static void AddConstantWord(this IInterpreter interpreter, string name, int n)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A word name expected.");

            interpreter.AddWord(new ConstantWord(interpreter, name, n));
        }

        #endregion


        #region parsing

        /// <summary>
        /// Reads the next character from the input.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <returns>The next character from the input or 0 at the end of the source.</returns>
        public static char CurrentChar(this IInterpreter interpreter)
            => interpreter.InputSource.CurrentChar;
        
        /// <summary>
        /// Reads the next character from the input.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <returns>The next character from the input or 0 at the end of the source.</returns>
        public static char NextChar(this IInterpreter interpreter)
            => interpreter.InputSource.NextChar();

        /// <summary>
        /// Returns a word name following in the input stream.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="toUpperCase">If true (the default), returned word is converted to UPPERCASE.</param>
        /// <returns>A word name.</returns>
        public static string ParseWord(this IInterpreter interpreter, bool toUpperCase = true)
            => interpreter.InputSource.ParseWord(toUpperCase);

        /// <summary>
        /// Gets a string terminated by a terminator char.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="terminator">A character terminating the string.</param>
        /// <param name="allowSpecialChars">If true, '\' escaped special chars are supported.</param>
        /// <param name="skipLeadingTerminators">If true, leading terminator chars are skipped.</param>
        /// <returns>A string.</returns>
        public static string ParseTerminatedString(this IInterpreter interpreter, char terminator, bool allowSpecialChars = false, bool skipLeadingTerminators = false)
            => interpreter.InputSource.ParseTerminatedString(terminator, allowSpecialChars, skipLeadingTerminators);

        /// <summary>
        /// Parses an integer number from a string.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="s">A string containing a number.</param>
        /// <param name="success">True, if parsing succeeded.</param>
        /// <returns>A parsed number.</returns>
        public static long ParseIntegerNumber(this IInterpreter interpreter, string s, out bool success)
            => interpreter.InputSource.ParseIntegerNumber(s, out success);

        /// <summary>
        /// Parses a floating point number from a string.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="s">A string containing a number.</param>
        /// <param name="success">True, if parsing succeeded.</param>
        /// <returns>A parsed number.</returns>
        public static double ParseFloatingPointNumber(this IInterpreter interpreter, string s, out bool success)
            => interpreter.InputSource.ParseFloatingPointNumber(s, out success);
        
        #endregion
        
        
        #region execution

        /// <summary>
        /// Interprets a string as a FORTH program.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="src">A FORTH program source.</param>
        public static void Evaluate(this IInterpreter interpreter, string src)
        {
            interpreter.Evaluate(new StringSourceReader(src));
        }

        #endregion
    }
}
