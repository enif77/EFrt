/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    public abstract class AStackBase<T> : IStack<T>
    {
        public T[] Items { get; }

        public int Top { get; set; }

        public int Count => Top + 1;
        
        public bool IsEmpty => Count <= 0;


        protected AStackBase(int capacity = 32)
        {
            Items = new T[capacity];

            Init(default(T));
        }


        public void Init(T defaultValue)
        {
            Top = -1;
            for (var i = 0; i < Items.Length; i++)
            {
                Items[i] = defaultValue;
            }
        }


        public void Clear()
        {
            Init(default(T));
        }


        public T Peek()
        {
            return Items[Top];
        }


        public T Pick(int index)
        {
            return Items[Top - index];
        }

        // (a -- )
        public T Pop()
        {
            return Items[Top--];
        }

        // (-- a)
        public void Push(T a)
        {
            Items[++Top] = a;
        }

        // (a -- a a)
        public void Dup()
        {
            Items[Top + 1] = Items[Top];
            Top++;
        }

        // (a --)
        public void Drop(int n = 1)
        {
            Top -= n;
        }

        // (a b -- b a)
        public void Swap()
        {
            // t = b
            var t = Items[Top];

            // a a
            Items[Top] = Items[Top - 1];

            // b a
            Items[Top - 1] = t;
        }

        // (a b -- a b a)
        public void Over()
        {
            Items[Top + 1] = Items[Top - 1];
            Top++;
        }

        // (a b c -- b c a)
        public void Rot()
        {
            // t = a
            var t = Items[Top - 2];

            // b b c
            Items[Top - 2] = Items[Top - 1];

            // b c c
            Items[Top - 1] = Items[Top];

            // b c a
            Items[Top] = t;
        }

        // ( -- )
        public void Roll(int index)
        {
            var item = Items[Top - index];
            for (var i = (Top - index) + 1; i <= Top; i++)
            {
                Items[i - 1] = Items[i];
            }

            Items[Top] = item;
        }
    }
}