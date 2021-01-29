/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    public class Stack : AStackBase<IStackCellValue>
    {
        public Stack(int capacity = 32)
            : base(capacity)
        {
            for (var i = 0; i < capacity; i++)
            {
                Items[i] = new StackCellValue();  // TODO: Add some stack-cells generator to support different stack-cell implementations.
            }
        }


        public override void Clear()
        {
            Top = -1;
            for (var i = 0; i < Items.Length; i++)
            {
                Items[i].IntegerValue = 0;
            }
        }

        // (-- flag)
        public void Push(bool flag)
        {
            Items[++Top].BooleanValue = flag;
        }

        // (-- n)
        public void Push(int n)
        {
            Items[++Top].IntegerValue = n;
        }

        // (-- d)
        public void Push(long d)
        {
            Items[++Top].DoubleIntegerValue = d;
        }

        // (-- f)
        public void Push(double f)
        {
            Items[++Top].FloatingPointValue = f;
        }

        // (-- s)
        public void Push(string s)
        {
            Items[++Top].StringValue = s;
        }

        // (-- o)
        public void Push(object o)
        {
            Items[++Top].ObjectValue = o;
        }

        // (a -- a a)
        public override void Dup()
        {
            var top = Items[Top];
            var ntop = Items[Top + 1];

            switch (top.ValueType)
            {
                case StackCellValueType.FloatingPoint: ntop.FloatingPointValue = top.FloatingPointValue; break;
                case StackCellValueType.Object: ntop.ObjectValue = top.ObjectValue; break;

                default: ntop.IntegerValue = top.IntegerValue; break;
            }

            Top++;
        }

        // (a b -- a b a)
        public override void Over()
        {
            var ntop = Items[Top + 1];
            var btop = Items[Top - 1];

            switch (btop.ValueType)
            {
                case StackCellValueType.FloatingPoint: ntop.FloatingPointValue = btop.FloatingPointValue; break;
                case StackCellValueType.Object: ntop.ObjectValue = btop.ObjectValue; break;

                default: ntop.IntegerValue = btop.IntegerValue; break;
            }

            Top++;
        }
    }
}