/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    public class FloatingPointStack : AStackBase<double>
    {
        public FloatingPointStack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}