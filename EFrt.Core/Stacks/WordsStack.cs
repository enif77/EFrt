/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    using EFrt.Core.Words;


    public class WordsStack : AStackBase<IWord>
    {
        public WordsStack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}