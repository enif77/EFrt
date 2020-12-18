/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Stacks
{
    public class ObjectHeap : AStackBase<object>
    {
        public ObjectHeap(int capacity = 32)
            : base(capacity)
        {
        }


        /// <summary>
        /// Allocates N cells on the stack for a variable. 
        /// </summary>
        /// <param name="cells">A number of stack cells/items to be reserved.</param>
        /// <returns>The index (aka address) of the first reserved cell.</returns>
        public int Alloc(int cells = 1)
        {
            if (cells > 0)
            {
                var addr = Top + 1;
                Top += cells;

                return addr;
            }
            else if (cells < 0)
            {
                Top += cells;
            }

            return Top;
        }
    }
}