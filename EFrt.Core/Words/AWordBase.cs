/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Words
{
    using System;


    /// <summary>
    /// Defines an executable word.
    /// </summary>
    public abstract class AWordBase : IWord
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance, that is executing this word.</param>
        protected AWordBase(IInterpreter interpreter)
        {
            if (interpreter == null) throw new ArgumentNullException(nameof(interpreter));

            Interpreter = interpreter;
            IsImmediate = false;
            IsControlWord = false;
        }


        /// <summary>
        /// A name of this word.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// If this word should be executed immediatelly.
        /// </summary>
        public bool IsImmediate { get; protected set; }

        /// <summary>
        /// A control word is defined by itself. No need for finding it in the words list.
        /// Used by the nonPrimitiveWord.Execute() method.
        /// </summary>
        public bool IsControlWord { get; protected set; }

        /// <summary>
        /// An execution token. Used by the EXECUTE word to find a words definition for execution.
        /// </summary>
        public int ExecutionToken { get; set; }

        /// <summary>
        /// The body of this word.
        /// </summary>
        public Func<int> Action { get; protected set; }

        /// <summary>
        /// An interpreter executing this word.
        /// </summary>
        protected IInterpreter Interpreter { get; }
    }
}
