/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    public class Stack : AStackBase<int>
    {
        public Stack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}