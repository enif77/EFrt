/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Stacks
{
    public class DataStack : AStackBase<int>
    {
        public DataStack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}