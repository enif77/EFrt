/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    public class ByteHeap : AStackBase<byte>
    {
        public const int CellSize = 4;


        public ByteHeap(int capacity = 256 * CellSize)  // 256 cells
            : base(capacity)
        {
        }


        /// <summary>
        /// Allocates N cells on the stack for a variable. 
        /// </summary>
        /// <param name="cells">A number of stack cells/items to be reserved.</param>
        /// <returns>The index (aka address) of the first cell of the newly reserved area.</returns>
        public int AllocBytes(int bytes = 1)
        {
            if (bytes > 0)
            {
                var addr = Top + 1;
                Top += bytes;

                return addr;
            }
            else if (bytes < 0)
            {
                Top += bytes * CellSize;
            }

            return Top;
        }


        /// <summary>
        /// Allocates N cells on the stack for a variable. 
        /// </summary>
        /// <param name="cells">A number of stack cells/items to be reserved.</param>
        /// <returns>The index (aka address) of the first cell of the newly reserved area.</returns>
        public int Alloc(int cells = 1)
        {
            return AllocBytes(cells * CellSize);
        }


        public void Write(int addr, int value)
        {
            var mem = Items;

            mem[addr++] = (byte)value;
            mem[addr++] = (byte)(value >> 8);
            mem[addr++] = (byte)(value >> 16);
            mem[addr]   = (byte)(value >> 24);
        }
    }
}