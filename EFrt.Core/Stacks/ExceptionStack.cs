/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    public class ExceptionStack : AStackBase<ExceptionFrame>
    {
        public ExceptionStack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}