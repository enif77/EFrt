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
        /// Adds a primitive word to the words list.
        /// </summary>
        /// <param name="interpreter">An interpreter.</param>
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
        /// <param name="interpreter">An interpreter.</param>
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
        /// <param name="interpreter">An interpreter.</param>
        /// <param name="name">A new word name.</param>
        /// <param name="n">A value.</param>
        public static void AddConstantWord(this IInterpreter interpreter, string name, int n)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("A word name expected.");

            interpreter.AddWord(new ConstantWord(interpreter, name, n));
        }

        #endregion


        #region execution

        /// <summary>
        /// Executes a string as a FORTH program.
        /// </summary>
        /// <param name="src">A FORTH program source.</param>
        public static void Execute(this IInterpreter interpreter, string src)
        {
            interpreter.Execute(new StringSourceReader(src));
        }

        #endregion
    }
}
