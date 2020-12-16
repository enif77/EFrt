/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Stacks
{
    public class ObjectStack : AStackBase<object>
    {
        public ObjectStack(int capacity = 32)
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
            var addr = Top + 1;
            Top += cells;

            return addr;
        }
    }
}