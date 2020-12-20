/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    public class ObjectStack : AStackBase<object>
    {
        public ObjectStack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}