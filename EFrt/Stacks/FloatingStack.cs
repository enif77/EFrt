/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Stacks
{
    public class FloatingStack : AStackBase<double>
    {
        public FloatingStack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}