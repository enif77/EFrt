/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Stacks
{
    public class Stack : AStackBase<int>
    {
        public Stack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}