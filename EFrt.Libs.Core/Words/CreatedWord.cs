/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Libs.Core.Words
{
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;


    /// <summary>
    /// A CREATEd word.
    /// </summary>
    public class CreatedWord : NonPrimitiveWord
    {
        /// <summary>
        /// An address/index of the first cell of this word's data field on the heap.
        /// </summary>
        public int DataFieldIndex { get; private set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="name">A name of this word.</param>
        /// <param name="addr">An address of the first cell of this word data field in heap.</param>
        public CreatedWord(IInterpreter interpreter, string name, int addr)
            : base(interpreter, name)
        {
            IsControlWord = true;
            Action = () => 
            {
                Interpreter.StackFree(1);

                Interpreter.Push(DataFieldIndex);

                return 1;
            };

            DataFieldIndex = addr;
        }

        /// <summary>
        /// Activates words defined as the body of this word for execution.
        /// Used by the DoesWord.
        /// </summary>
        public void ActivateDefinedWords()
        {
            Action = base.Execute;
        }
    }
}
