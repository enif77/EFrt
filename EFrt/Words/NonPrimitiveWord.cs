/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Words
{
    using System.Collections.Generic;


    /// <summary>
    /// A compound word.
    /// </summary>
    public class NonPrimitiveWord : AWordBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">A  words name.</param>
        public NonPrimitiveWord(IInterpreter interpreter, string name, int sourcePos = -1)
            : base(interpreter)
        {
            Name = name;
            IsImmediate = true;
            Action = Execute;
            SourcePos = sourcePos;

            _words = new List<IWord>();
        }


        /// <summary>
        /// Adds a word to this words definition.
        /// </summary>
        /// <param name="word"></param>
        public void AddWord(IWord word)
        {
            _words.Add(word);
        }

        /// <summary>
        /// Executes this words body.
        /// </summary>
        private void Execute()
        {
            foreach (var word in _words)
            {
                // Get the actual word implementation.
                var wordImpl = Interpreter.GetWord(word.Name);

                wordImpl.Action();
            }
        }


        /// <summary>
        /// A list of words defining this word.
        /// </summary>
        private List<IWord> _words;
    }
}
