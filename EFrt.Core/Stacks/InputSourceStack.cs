/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    public class InputSourceStack : AStackBase<IInputSource>
    {
        public InputSourceStack(int capacity = 32)
            : base(capacity)
        {
        }
    }
}