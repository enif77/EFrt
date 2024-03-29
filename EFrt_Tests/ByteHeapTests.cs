/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests
{
    using System;

    using Xunit;

    using EFrt.Core.Stacks;


    public class ByteHeapTests
    {
        [Fact]
        public void NewByteHeapIsEmptyTest()
        {
            var bh = new Heap();

            Assert.Equal(0, bh.Count);
            Assert.Equal(-1, bh.Top);
        }

        // --- ALLOCATIONS TESTS ---
        
        [Fact]
        public void AllocReturnsAddressTest()
        {
            var bh = new Heap();

            // Returns the beginning of the newly allocated memory.
            Assert.Equal(0, bh.Alloc(10));
            Assert.Equal(10, bh.Alloc(10));
            
            // Returns the address of the last byte in the allocated space.
            Assert.Equal(14, bh.Alloc(-5));
            
            // Returns the address of the last byte in the allocated space.
            Assert.Equal(14, bh.Alloc(0));
        }
        
        [Fact]
        public void AllocSetsTopTest()
        {
            var bh = new Heap();

            // Top is the address of the last byte in the allocated space.
            
            bh.Alloc(10);
            Assert.Equal(9, bh.Top);
            
            bh.Alloc(10);
            Assert.Equal(19, bh.Top);
            
            bh.Alloc(-5);
            Assert.Equal(14, bh.Top);
            
            bh.Alloc(0);
            Assert.Equal(14, bh.Top);
        }
        
        [Fact]
        public void AllocCellsReturnsAddressTest()
        {
            var bh = new Heap();

            Assert.Equal(0, bh.AllocCells(10));
            Assert.Equal(40, bh.AllocCells(10));  // 40 = 10 * cell size.
        }

        // --- ADDRESS ALIGNMENT TESTS ---
        
        [Fact]
        public void IsByteAlignedTest()
        {
            var bh = new Heap();

            Assert.True(bh.IsByteAligned(123));
        }

        [Fact]
        public void ByteAlignedTest()
        {
            var bh = new Heap();

            Assert.Equal(123, bh.ByteAligned(123));
        }
        
        [Fact]
        public void IsCharAlignedTest()
        {
            var bh = new Heap();

            Assert.True(bh.IsCharAligned(0));
            Assert.True(bh.IsCharAligned(122));
            Assert.True(bh.IsCharAligned(-6));
            
            Assert.False(bh.IsCellAligned(-1));
            Assert.False(bh.IsCellAligned(1));
            Assert.False(bh.IsCellAligned(123));
        }
        
        [Fact]
        public void CharAlignedTest()
        {
            var bh = new Heap();

            // 124 = ((123 >> 1) << 1) + char-size
            Assert.Equal(120, bh.CharAligned(120));
            Assert.Equal(122, bh.CharAligned(121));
            Assert.Equal(122, bh.CharAligned(122));
            Assert.Equal(124, bh.CharAligned(123));
            Assert.Equal(124, bh.CharAligned(124));
            Assert.Equal(126, bh.CharAligned(125));
        }
        
        [Fact]
        public void IsCellAlignedTest()
        {
            var bh = new Heap();

            Assert.True(bh.IsCellAligned(0));
            Assert.True(bh.IsCellAligned(120));
            Assert.True(bh.IsCellAligned(-8));
            
            Assert.False(bh.IsCellAligned(-1));
            Assert.False(bh.IsCellAligned(1));
            Assert.False(bh.IsCellAligned(123));
        }
        
        [Fact]
        public void CellAlignedTest()
        {
            var bh = new Heap();

            // 124 = ((123 >> 2) << 2) + cell-size
            Assert.Equal(120, bh.CellAligned(120));
            Assert.Equal(124, bh.CellAligned(121));
            Assert.Equal(124, bh.CellAligned(122));
            Assert.Equal(124, bh.CellAligned(123));
            Assert.Equal(124, bh.CellAligned(124));
            Assert.Equal(128, bh.CellAligned(125));
        }
        
        [Fact]
        public void IsDoubleCellAlignedTest()
        {
            var bh = new Heap();

            Assert.True(bh.IsDoubleCellAligned(-8));
            Assert.True(bh.IsDoubleCellAligned(0));
            Assert.True(bh.IsDoubleCellAligned(120));
            
            Assert.False(bh.IsDoubleCellAligned(-1));
            Assert.False(bh.IsDoubleCellAligned(1));
            Assert.False(bh.IsDoubleCellAligned(123));
        }
        
        [Fact]
        public void DoubleCellAlignedTest()
        {
            var bh = new Heap();

            // 128 = ((123 >> 4) << 4) + cell-size * 2 + cell-size * 2
            // 128 = ((125 >> 4) << 4) + cell-size * 2
            Assert.Equal(120, bh.DoubleCellAligned(120));
            Assert.Equal(128, bh.DoubleCellAligned(121));
            Assert.Equal(128, bh.DoubleCellAligned(122));
            Assert.Equal(128, bh.DoubleCellAligned(123));
            Assert.Equal(128, bh.DoubleCellAligned(124));
            Assert.Equal(128, bh.DoubleCellAligned(125));
            Assert.Equal(128, bh.DoubleCellAligned(126));
            Assert.Equal(128, bh.DoubleCellAligned(127));
            Assert.Equal(128, bh.DoubleCellAligned(128));
            Assert.Equal(136, bh.DoubleCellAligned(129));
        }
        
        // --- READ/WRITE TESTS ---
        
        [Fact]
        public void ReadWriteByteTest()
        {
            var bh = new Heap();

            bh.Alloc(10);
            bh.Write(5, (byte)123);
            
            Assert.Equal(123, bh.ReadByte(5));
        }
        
        [Fact]
        public void ReadWriteInt16Test()
        {
            var bh = new Heap();

            bh.Alloc(10);
            bh.Write(0, (char)123);
            bh.Write(2, (char)23456);
            
            Assert.Equal(123, bh.ReadInt16(0));
            Assert.Equal(23456, bh.ReadInt16(2));
            
            // Big number test.
            bh.Write(4, (char)65535);
            
            Assert.Equal(0xffff, bh.ReadInt16(4));
            
            Assert.Equal(0x7b, bh.ReadByte(0));
            Assert.Equal(0xa0, bh.ReadByte(2));
        }
        
        [Fact]
        public void ReadWriteInt32Test()
        {
            var bh = new Heap();

            bh.Alloc(10);
            bh.Write(0, 123);
            bh.Write(4, 123456);
            
            Assert.Equal(123, bh.ReadInt32(0));
            Assert.Equal(123456, bh.ReadInt32(4));
            
            // Big number test.
            bh.Write(8, 1_234_567_890);
            
            Assert.Equal(1_234_567_890, bh.ReadInt32(8));
            
            // 49 96 02 D2
            Assert.Equal(0xD2, bh.ReadByte(8));
            Assert.Equal(0x02, bh.ReadByte(9));
            Assert.Equal(0x96, bh.ReadByte(10));
            Assert.Equal(0x49, bh.ReadByte(11));
        }
        
        [Fact]
        public void ReadWriteInt64Test()
        {
            var bh = new Heap();

            bh.Alloc(32);
            bh.Write(0, 123L);
            bh.Write(8, 1234567890123L);
            
            Assert.Equal(123L, bh.ReadInt64(0));
            Assert.Equal(1234567890123L, bh.ReadInt64(8));
            
            // Big number test.
            bh.Write(16, 1_234_567_890_123_456_789L);
            
            Assert.Equal(1_234_567_890_123_456_789L, bh.ReadInt64(16));
            
            // 11 22 10 F4 7D E9 81 15
            Assert.Equal(0x15, bh.ReadByte(16));
            Assert.Equal(0x81, bh.ReadByte(17));
            Assert.Equal(0xE9, bh.ReadByte(18));
            Assert.Equal(0x7D, bh.ReadByte(19));
            Assert.Equal(0xF4, bh.ReadByte(20));
            Assert.Equal(0x10, bh.ReadByte(21));
            Assert.Equal(0x22, bh.ReadByte(22));
            Assert.Equal(0x11, bh.ReadByte(23));
        }
        
        [Fact]
        public void ReadWriteFloatTest()
        {
            var bh = new Heap();

            bh.Alloc(10);
            bh.Write(0, 123.0f);
            bh.Write(4, 123456.0f);
            
            Assert.Equal(123.0f, bh.ReadFloat(0));
            Assert.Equal(123456.0f, bh.ReadFloat(4));
        }
        
        [Fact]
        public void ReadWriteDoubleTest()
        {
            var bh = new Heap();

            bh.Alloc(32);
            bh.Write(0, 123.0);
            bh.Write(8, 1234567890123.0);
            
            Assert.Equal(123.0, bh.ReadDouble(0));
            Assert.Equal(1234567890123.0, bh.ReadDouble(8));
        }
        
        // --- MOVE TESTS ---

        [Fact]
        public void MoveUpTest()
        {
            var bh = new Heap();

            bh.Alloc(32);

            for (var i = 0; i < 8; i++)
            {
                bh.Items[i] = (byte)i;
            }

            Assert.Equal(2, bh.Items[2]);
            Assert.Equal(0, bh.Items[10]);
            
            bh.Move(0, 8, 8);
            
            Assert.Equal(2, bh.Items[10]);
            Assert.Equal(7, bh.Items[15]);
        }
        
        [Fact]
        public void MoveUpOverlappingTest()
        {
            var bh = new Heap();

            bh.Alloc(32);

            for (var i = 0; i < 8; i++)
            {
                bh.Items[i] = (byte)i;
            }

            Assert.Equal(2, bh.Items[2]);
            Assert.Equal(4, bh.Items[4]);
            Assert.Equal(0, bh.Items[10]);
            
            bh.Move(0, 4, 8);
            
            Assert.Equal(2, bh.Items[2]);
            Assert.Equal(0, bh.Items[4]);
            Assert.Equal(2, bh.Items[6]);
            Assert.Equal(7, bh.Items[11]);
        }
        
        [Fact]
        public void MoveDownTest()
        {
            var bh = new Heap();

            bh.Alloc(32);

            for (var i = 0; i < 8; i++)
            {
                bh.Items[i + 8] = (byte)i;
            }

            Assert.Equal(2, bh.Items[10]);
            Assert.Equal(0, bh.Items[2]);
            
            bh.Move(8, 0, 8);
            
            Assert.Equal(2, bh.Items[2]);
        }
        
        [Fact]
        public void MoveDownOverlappingTest()
        {
            var bh = new Heap();

            bh.Alloc(32);

            for (var i = 0; i < 8; i++)
            {
                bh.Items[i + 8] = (byte)i;
            }

            Assert.Equal(2, bh.Items[10]);
            Assert.Equal(0, bh.Items[6]);
            
            bh.Move(8, 4, 8);
            
            Assert.Equal(7, bh.Items[11]);
            Assert.Equal(2, bh.Items[6]);
            Assert.Equal(0, bh.Items[2]);
        }
        
        // --- FILL TESTS ---
        
        [Fact]
        public void FillTest()
        {
            var bh = new Heap();

            bh.Alloc(32);

            for (var i = 0; i < 8; i++)
            {
                bh.Items[i] = (byte)i;
            }

            Assert.Equal(1, bh.Items[1]);
            Assert.Equal(2, bh.Items[2]);
            Assert.Equal(5, bh.Items[5]);
            Assert.Equal(6, bh.Items[6]);
            
            bh.Fill(2, 4, 9);
            
            Assert.Equal(1, bh.Items[1]);
            Assert.Equal(9, bh.Items[2]);
            Assert.Equal(9, bh.Items[5]);
            Assert.Equal(6, bh.Items[6]);
        }
        
        [Fact]
        public void FillCharsTest()
        {
            var bh = new Heap();

            bh.Alloc(32);

            for (var i = 0; i < 16; i++)
            {
                bh.Items[i] = (byte)i;
            }

            Assert.Equal(1, bh.Items[1]);
            Assert.Equal(2, bh.Items[2]);
            Assert.Equal(6, bh.Items[6]);
            Assert.Equal(10, bh.Items[10]);
            
            bh.Fill(2, 4, 'a');
            
            Assert.Equal(1, bh.Items[1]);
            Assert.Equal('a', bh.ReadInt16(2));  // Lower bound.
            Assert.Equal('a', bh.ReadInt16(6));  // Inside.
            Assert.Equal('a', bh.ReadInt16(8));  // Top bound.
            Assert.Equal(10, bh.Items[10]);
        }
    }
}
