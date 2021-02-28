/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    using System;


    public class Heap : AStackBase<int>
    {
        public Heap(int capacity = 256)
            : base(capacity)
        {
        }


        /// <summary>
        /// Allocates N cells on the stack for a variable. 
        /// </summary>
        /// <param name="cells">A number of stack cells/items to be reserved.</param>
        /// <returns>The index (aka address) of the first cell of the newly reserved area.</returns>
        public int Alloc(int cells = 1)
        {
            if (cells > 0)
            {
                var addr = Top + 1;
                Top += cells;

                return addr;
            }
            
            if (cells < 0)
            {
                Top += cells;
            }

            return Top;
        }

        /// <summary>
        /// If count is greater than zero, copy the contents of count consecutive cells at srcAddr
        /// to the count consecutive cells at destAddr.
        /// </summary>
        /// <param name="srcAddr">The source address.</param>
        /// <param name="destAddr">The destination address.</param>
        /// <param name="count">The number of cells to be copyed to the destAddr.</param>
        public void Move(int srcAddr, int destAddr, int count)
        {
            if (srcAddr < 0 || srcAddr >= Items.Length) throw new ArgumentOutOfRangeException(nameof(srcAddr), $"The source address {srcAddr} is out of the <0 .. Heap.Lenght) range.");
            if (destAddr < 0 || destAddr >= Items.Length) throw new ArgumentOutOfRangeException(nameof(destAddr), $"The destination address {destAddr} is out of the <0 .. Heap.Lenght) range.");

            // Do not copy to the same memory location or nothing at all.
            if (srcAddr == destAddr || count <= 0)
            {
                return;
            }

            if (srcAddr + count >= Items.Length) throw new ArgumentOutOfRangeException(nameof(count), $"The source address {srcAddr} plus the count {count} is out of the <0 .. Heap.Lenght) range.");
            if (destAddr + count >= Items.Length) throw new ArgumentOutOfRangeException(nameof(count), $"The destination address {destAddr} plus the count {count} is out of the <0 .. Heap.Lenght) range.");

            var s = srcAddr;
            for (var d = destAddr; d < destAddr + count; d++)
            {
                Items[d] = Items[s++];
            }
        }
    }
}