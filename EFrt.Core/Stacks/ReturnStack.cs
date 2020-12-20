/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    public class ReturnStack : AStackBase<int>
    {
        public ReturnStack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}