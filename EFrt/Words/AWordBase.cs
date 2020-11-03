/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
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
        /// <param name="interpreter">An instance of IInterpreter, that is executing this word.</param>
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

        public bool IsControlWord { get; protected set; }

        /// <summary>
        /// The body of this word.
        /// </summary>
        public Func<int> Action { get; protected set; }
               

        protected IInterpreter Interpreter { get; }
    }
}
