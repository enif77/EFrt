/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Stacks
{
    
    public class ObjectStack : AStackBase<object>
    {
        public ObjectStack(int capacity = 32)
            : base(capacity)
        {
        }


        public void Push(int a)
        {
            Items[++Top] = a;
        }

        public void Push(float a)
        {
            Items[++Top] = a;
        }

        public void Push(string a)
        {
            Items[++Top] = a;
        }
    }
}