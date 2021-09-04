/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    using System;
    
    using EFrt.Core.Values;
    
    
    /// <summary>
    /// Bytes based heap.
    /// </summary>
    public class Heap : AStackBase<byte>
    {
        /// <summary>
        /// A size of a char in bytes.
        /// </summary>
        public const int CharSize = sizeof(char);
    
        /// <summary>
        /// A size of a cell in bytes.
        /// </summary>
        public const int CellSize = sizeof(int);
        
        /// <summary>
        /// A size of two cells in bytes.
        /// </summary>
        public const int DoubleCellSize = CellSize * 2;

        /// <summary>
        /// Char alignment mask. 2 bytes.
        /// </summary>
        private const int Int16AddressMask = -1 << 1;
        
        /// <summary>
        /// Single cell alignment mask. 4 bytes.
        /// </summary>
        private const int Int32AddressMask = -1 << 2;
        
        /// <summary>
        /// Double cell alignment mask. 8 bytes.
        /// </summary>
        private const int Int64AddressMask = -1 << 3;
        
        
        /// <summary>
        /// Constructor. 
        /// </summary>
        /// <param name="capacity">A capacity of this heap in bytes. 1024 * CellSize by default.</param>
        public Heap(int capacity = 1024 * CellSize)  // 1024 cells
            : base(capacity)
        {
        }


        /// <summary>
        /// Allocates N cells on the stack for a variable. 
        /// </summary>
        /// <param name="bytes">A number of bytes to be reserved.</param>
        /// <returns>The index (aka address) of the first byte of the newly reserved area.</returns>
        public int Alloc(int bytes = 1)
        {
            if (bytes > 0)
            {
                var addr = Top + 1;
                Top += bytes;

                return addr;
            }
            
            if (bytes < 0)
            {
                Top += bytes;
            }

            return Top;
        }

        /// <summary>
        /// Allocates N cells on the stack for a variable. 
        /// </summary>
        /// <param name="cells">A number of stack cells/items to be reserved.</param>
        /// <returns>The index (aka address) of the first cell of the newly reserved area.</returns>
        public int AllocCells(int cells = 1)
        {
            return Alloc(cells * CellSize);
        }

        /// <summary>
        /// Checks, if an address is byte-aligned.
        /// </summary>
        /// <param name="addr">An address.</param>
        /// <returns>True, if an address is byte-aligned.</returns>
        public bool IsByteAligned(int addr) => true;

        /// <summary>
        /// Returns a byte-aligned address that is greater or equal to addr.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>An address.</returns>
        public int ByteAligned(int addr) => addr;
        
        /// <summary>
        /// Checks, if an address is char-aligned.
        /// </summary>
        /// <param name="addr">An address.</param>
        /// <returns>True, if an address is char-aligned.</returns>
        public bool IsCharAligned(int addr)
        {
            return (addr & Int16AddressMask) == addr;;
        }

        /// <summary>
        /// Returns a char-aligned address that is greater or equal to addr.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>An address.</returns>
        public int CharAligned(int addr)
        {
            var aligned = addr & Int16AddressMask;

            return (aligned < addr)
                ? aligned + CharSize
                : aligned;
        }
        
        /// <summary>
        /// Checks, if an address is single-cell-aligned.
        /// </summary>
        /// <param name="addr">An address.</param>
        /// <returns>True, if an address is single-cell-aligned.</returns>
        public bool IsCellAligned(int addr)
        {
            return (addr & Int32AddressMask) == addr;
        }
        
        /// <summary>
        /// Returns a single-cell-aligned address that is greater or equal to addr.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>An address.</returns>
        public int CellAligned(int addr)
        {
            var aligned = addr & Int32AddressMask;

            return (aligned < addr)
                ? aligned + CellSize
                : aligned;
        }
        
        /// <summary>
        /// Checks, if an address is single-cell-floating-point-aligned.
        /// </summary>
        /// <param name="addr">An address.</param>
        /// <returns>True, if an address is single-cell-floating-point-aligned.</returns>
        public bool IsSingleCellFloatingPointAligned(int addr)
        {
            return IsCellAligned(addr);
        }
        
        /// <summary>
        /// Returns a single-cell-floating-point-aligned address that is greater or equal to addr.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>An address.</returns>
        public int SingleCellFloatingPointAligned(int addr)
        {
            return CellAligned(addr);
        }
        
        /// <summary>
        /// Checks, if an address is double-cell-aligned.
        /// </summary>
        /// <param name="addr">An address.</param>
        /// <returns>True, if an address is double-cell-aligned.</returns>
        public bool IsDoubleCellAligned(int addr)
        {
            return (addr & Int64AddressMask) == addr;
        }
        
        /// <summary>
        /// Returns a double-cell-aligned address that is greater or equal to addr.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>An address.</returns>
        public int DoubleCellAligned(int addr)
        {
            var aligned = addr & Int64AddressMask;

            if (aligned >= addr)
            {
                return aligned;
            }

            aligned += DoubleCellSize;

            return (aligned < addr)
                ? aligned + DoubleCellSize
                : aligned;
        }

        /// <summary>
        /// Checks, if an address is double-cell-aligned.
        /// </summary>
        /// <param name="addr">An address.</param>
        /// <returns>True, if an address is double-cell-aligned.</returns>
        public bool IsDoubleCellFloatingPointAligned(int addr)
        {
            return IsDoubleCellAligned(addr);
        }
        
        /// <summary>
        /// Returns a double-cell-floating-point-aligned address that is greater or equal to addr.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>An address.</returns>
        public int DoubleCellFloatingPointAligned(int addr)
        {
            return DoubleCellAligned(addr);
        }
        
        /// <summary>
        /// Fills a range of bytes with a value.
        /// </summary>
        /// <param name="addr">A start address.</param>
        /// <param name="count">A number of bytes to be filled with the value.</param>
        /// <param name="value">A value.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when addr or addr + count are out of the 0 .. Heap.Length range.</exception>
        public void Fill(int addr, int count, byte value)
        {
            // TODO: Remove checks or use FORTH exceptions.
            
            if (addr < 0 || addr >= Items.Length) throw new ArgumentOutOfRangeException(nameof(addr), $"The address {addr} is out of the <0 .. Heap.Length) range.");
            
            // Do not fill nothing.
            if (count <= 0)
            {
                return;
            }
            
            if (addr + count >= Items.Length) throw new ArgumentOutOfRangeException(nameof(count), $"The address {addr} plus the count {count} is out of the <0 .. Heap.Length) range.");

            Array.Fill(Items, value, addr, count);
        }

        /// <summary>
        /// Fills a range of chars with a value.
        /// </summary>
        /// <param name="addr">A start address.</param>
        /// <param name="count">A number of chars to be filled with the value.</param>
        /// <param name="value">A value.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when addr or addr + count * sizeof(char) are out of the 0 .. Heap.Length range.</exception>
        public void Fill(int addr, int count, char value)
        {
            // TODO: Remove checks or use FORTH exceptions.
            
            if (addr < 0 || addr >= Items.Length) throw new ArgumentOutOfRangeException(nameof(addr), $"The address {addr} is out of the <0 .. Heap.Length) range.");
            
            // Do not fill nothing.
            if (count <= 0)
            {
                return;
            }
            
            if (addr + count * CharSize >= Items.Length) throw new ArgumentOutOfRangeException(nameof(count), $"The address {addr} plus the count * sizeof(char) {count} is out of the <0 .. Heap.Length) range.");

            var hi = (byte)(value >> 8);
            var lo = (byte)value;
            
            for (var i = addr; i < addr + count * CharSize; i++)
            {
                Items[i++] = lo;
                Items[i] = hi;
            }
        }
        
        /// <summary>
        /// If count is greater than zero, copy the contents of count consecutive cells at srcAddr
        /// to the count consecutive cells at destAddr.
        /// </summary>
        /// <param name="srcAddr">The source address.</param>
        /// <param name="destAddr">The destination address.</param>
        /// <param name="count">The number of cells to be copyed to the destAddr.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown, when srcAddr or descAddr or srcAddr + count or descAddr + count are out of the 0 .. Heap.Length range.</exception>
        public void Move(int srcAddr, int destAddr, int count)
        {
            // TODO: Remove checks or use FORTH exceptions.
            
            if (srcAddr < 0 || srcAddr >= Items.Length) throw new ArgumentOutOfRangeException(nameof(srcAddr), $"The source address {srcAddr} is out of the <0 .. Heap.Length) range.");
            if (destAddr < 0 || destAddr >= Items.Length) throw new ArgumentOutOfRangeException(nameof(destAddr), $"The destination address {destAddr} is out of the <0 .. Heap.Length) range.");

            // Do not copy to the same memory location or nothing at all.
            if (srcAddr == destAddr || count <= 0)
            {
                return;
            }

            if (srcAddr + count >= Items.Length) throw new ArgumentOutOfRangeException(nameof(count), $"The source address {srcAddr} plus the count {count} is out of the <0 .. Heap.Length) range.");
            if (destAddr + count >= Items.Length) throw new ArgumentOutOfRangeException(nameof(count), $"The destination address {destAddr} plus the count {count} is out of the <0 .. Heap.Length) range.");

            if (srcAddr > destAddr)
            {
                var s = srcAddr;
                for (var d = destAddr; d < destAddr + count; d++)
                {
                    Items[d] = Items[s++];
                }   
            }
            else
            {
                var s = srcAddr + count - 1;
                for (var d = destAddr + count - 1; d >= destAddr; d--)
                {
                    Items[d] = Items[s--];
                }
            }
        }
        
        /// <summary>
        /// Writes a byte to an address.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <param name="value">A value.</param>
        public void Write(int addr, byte value)
        {
            Items[addr] = value;
        }
        
        /// <summary>
        /// Reads a byte from an array of bytes.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>A byte value.</returns>
        public byte ReadByte(int addr)
        {
            return Items[addr];
        }
        
        /// <summary>
        /// Writes a char value to an address.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <param name="value">A value.</param>
        public void Write(int addr, char value)
        {
            var mem = Items;

            mem[addr++] = (byte) value;
            mem[addr] = (byte) (value >> 8);
        }
        
        /// <summary>
        /// Reads a int from an array of bytes.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>An int value.</returns>
        public char ReadInt16(int addr)
        {
            var mem = Items;
            
            var v = (int)mem[addr++];
            v |= (int)mem[addr] << 8;
            
            return (char)v;
        }
        
        /// <summary>
        /// Writes an int value to an address.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <param name="value">A value.</param>
        public void Write(int addr, int value)
        {
            var mem = Items;

            mem[addr++] = (byte) value;
            mem[addr++] = (byte) (value >> 8);
            mem[addr++] = (byte) (value >> 16);
            mem[addr]   = (byte) (value >> 24);
        }
        
        /// <summary>
        /// Reads a int from an array of bytes.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>An int value.</returns>
        public int ReadInt32(int addr)
        {
            var mem = Items;
            
            var v = (int)mem[addr++];
            v |= (int)mem[addr++] << 8;
            v |= (int)mem[addr++] << 16;
            v |= (int)mem[addr] << 24;
            
            return v;
        }
        
        /// <summary>
        /// Writes a long value to an address.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <param name="value">A value.</param>
        public void Write(int addr, long value)
        {
            var mem = Items;
            
            mem[addr++] = (byte) value;
            mem[addr++] = (byte) (value >> 8);
            mem[addr++] = (byte) (value >> 16);
            mem[addr++] = (byte) (value >> 24);
            mem[addr++] = (byte) (value >> 32);
            mem[addr++] = (byte) (value >> 40);
            mem[addr++] = (byte) (value >> 48);
            mem[addr]   = (byte) (value >> 56);
        }
        
        /// <summary>
        /// Reads a long from an array of bytes.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>A long value.</returns>
        public long ReadInt64(int addr)
        {
            var mem = Items;
            
            var v = (long)mem[addr++];
            v |= (long)mem[addr++] << 8;
            v |= (long)mem[addr++] << 16;
            v |= (long)mem[addr++] << 24;
            v |= (long)mem[addr++] << 32;
            v |= (long)mem[addr++] << 40;
            v |= (long)mem[addr++] << 48;
            v |= (long)mem[addr] << 56;
            
            return v;
        }
        
        /// <summary>
        /// Writes a double value to an address.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <param name="value">A value.</param>
        public void Write(int addr, float value)
        {
            var mem = Items;
            var fp = new SingleCellFloatingPointValue()
            {
                F = value
            };
            
            mem[addr++] = (byte) (fp.A);
            mem[addr++] = (byte) (fp.A >> 8);
            mem[addr++] = (byte) (fp.A >> 16);
            mem[addr]   = (byte) (fp.A >> 24);
        }
        
        /// <summary>
        /// Reads a double from an array of bytes.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>A double value.</returns>
        public float ReadFloat(int addr)
        {
            var mem = Items;
            var fp = new SingleCellFloatingPointValue();
            
            fp.A  = (int)mem[addr++];
            fp.A |= (int)mem[addr++] << 8;
            fp.A |= (int)mem[addr++] << 16;
            fp.A |= (int)mem[addr] << 24;
            
            return fp.F;
        }
        
        /// <summary>
        /// Writes a double value to an address.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <param name="value">A value.</param>
        public void Write(int addr, double value)
        {
            var mem = Items;
            var fp = new FloatingPointValue()
            {
                F = value
            };
            
            mem[addr++] = (byte) fp.B;
            mem[addr++] = (byte) (fp.B >> 8);
            mem[addr++] = (byte) (fp.B >> 16);
            mem[addr++] = (byte) (fp.B >> 24);
            
            mem[addr++] = (byte) (fp.A);
            mem[addr++] = (byte) (fp.A >> 8);
            mem[addr++] = (byte) (fp.A >> 16);
            mem[addr]   = (byte) (fp.A >> 24);
        }
        
        /// <summary>
        /// Reads a double from an array of bytes.
        /// </summary>
        /// <param name="addr">A value index, aka address.</param>
        /// <returns>A double value.</returns>
        public double ReadDouble(int addr)
        {
            var mem = Items;
            var fp = new FloatingPointValue();
            
            fp.B  = (int)mem[addr++];
            fp.B |= (int)mem[addr++] << 8;
            fp.B |= (int)mem[addr++] << 16;
            fp.B |= (int)mem[addr++] << 24;
            
            fp.A  = (int)mem[addr++];
            fp.A |= (int)mem[addr++] << 8;
            fp.A |= (int)mem[addr++] << 16;
            fp.A |= (int)mem[addr] << 24;
            
            return fp.F;
        }
    }
}

/*

https://stackoverflow.com/questions/1287143/simplest-way-to-copy-int-to-byte
https://docs.microsoft.com/cs-cz/dotnet/api/system.io.binarywriter.write?view=net-5.0
https://docs.microsoft.com/en-us/dotnet/api/system.bitconverter?redirectedfrom=MSDN&view=net-5.0
https://github.com/microsoft/referencesource/blob/master/mscorlib/system/bitconverter.cs
https://github.com/microsoft/referencesource/blob/master/mscorlib/system/io/binarywriter.cs
 
 */