/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Stacks
{
    public class DataStack : AStackBase<StackValue>
    {
        public DataStack(int capacity = 32)
            : base(capacity)
        {
        }


        public void Push(int a)
        {
            Items[++Top].Int = a;
        }

        public void Push(uint a)
        {
            Items[++Top].UInt = a;
        }

        public void Push(float a)
        {
            Items[++Top].Float = a;
        }

        public void Push(short a)
        {
            Items[++Top].Short = a;
        }

        public void Push(short a, short b)
        {
            Items[++Top] = new StackValue(a, b);
        }

        public void Push(ushort a)
        {
            Items[++Top].UShort = a;
        }

        public void Push(ushort a, ushort b)
        {
            Items[++Top] = new StackValue(a, b);
        }

        public void Push(byte a)
        {
            Items[++Top].Byte = a;
        }

        public void Push(byte a, byte b)
        {
            Items[++Top] = new StackValue(a, b);
        }

        public void Push(byte a, byte b, byte c)
        {
            Items[++Top] = new StackValue(a, b, c);
        }

        public void Push(byte a, byte b, byte c, byte d)
        {
            Items[++Top] = new StackValue(a, b, c, d);
        }
    }
}