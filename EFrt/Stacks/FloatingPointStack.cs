/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Stacks
{
    public class FloatingPointStack : AStackBase<double>
    {
        public FloatingPointStack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}