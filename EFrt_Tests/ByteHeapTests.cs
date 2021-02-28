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
            var bh = new ByteHeap();

            Assert.Equal(0, bh.Count);
            Assert.Equal(-1, bh.Top);
        }

        // --- ALLOCATIONS TESTS ---
        
        [Fact]
        public void AllocReturnsAddressTest()
        {
            var bh = new ByteHeap();

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
            var bh = new ByteHeap();

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
            var bh = new ByteHeap();

            Assert.Equal(0, bh.AllocCells(10));
            Assert.Equal(40, bh.AllocCells(10));  // 40 = 10 * cell size.
        }

        // --- ADDRESS ALIGNMENT TESTS ---
        
        [Fact]
        public void ByteAlignedTest()
        {
            var bh = new ByteHeap();

            Assert.Equal(123, bh.ByteAligned(123));
        }
        
        [Fact]
        public void Int32AlignedTest()
        {
            var bh = new ByteHeap();

            // 124 = ((123 >> 2) << 2) + cell-size
            Assert.Equal(120, bh.Int32Aligned(120));
            Assert.Equal(124, bh.Int32Aligned(121));
            Assert.Equal(124, bh.Int32Aligned(122));
            Assert.Equal(124, bh.Int32Aligned(123));
            Assert.Equal(124, bh.Int32Aligned(124));
            Assert.Equal(128, bh.Int32Aligned(125));
        }
        
        [Fact]
        public void CellAlignedTest()
        {
            var bh = new ByteHeap();

            Assert.Equal(120, bh.CellAligned(120));
            Assert.Equal(124, bh.CellAligned(121));
            Assert.Equal(124, bh.CellAligned(122));
            Assert.Equal(124, bh.CellAligned(123));
            Assert.Equal(124, bh.CellAligned(124));
            Assert.Equal(128, bh.CellAligned(125));
        }
        
        [Fact]
        public void Int64AlignedTest()
        {
            var bh = new ByteHeap();

            // 128 = ((123 >> 4) << 4) + cell-size * 2 + cell-size * 2
            // 128 = ((125 >> 4) << 4) + cell-size * 2
            Assert.Equal(120, bh.Int64Aligned(120));
            Assert.Equal(128, bh.Int64Aligned(121));
            Assert.Equal(128, bh.Int64Aligned(122));
            Assert.Equal(128, bh.Int64Aligned(123));
            Assert.Equal(128, bh.Int64Aligned(124));
            Assert.Equal(128, bh.Int64Aligned(125));
            Assert.Equal(128, bh.Int64Aligned(126));
            Assert.Equal(128, bh.Int64Aligned(127));
            Assert.Equal(128, bh.Int64Aligned(128));
            Assert.Equal(136, bh.Int64Aligned(129));
        }
        
        [Fact]
        public void DoubleCellAlignedTest()
        {
            var bh = new ByteHeap();

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
            var bh = new ByteHeap();

            bh.Alloc(10);
            bh.Write(5, (byte)123);
            
            Assert.Equal(123, bh.ReadByte(5));
        }
        
        [Fact]
        public void ReadWriteInt32Test()
        {
            var bh = new ByteHeap();

            bh.Alloc(10);
            bh.Write(0, 123);
            bh.Write(4, 123456);
            
            Assert.Equal(123, bh.ReadInt32(0));
            Assert.Equal(123456, bh.ReadInt32(4));
        }
        
        [Fact]
        public void ReadWriteInt64Test()
        {
            var bh = new ByteHeap();

            bh.Alloc(32);
            bh.Write(0, 123L);
            bh.Write(8, 1234567890123L);
            
            Assert.Equal(123L, bh.ReadInt64(0));
            Assert.Equal(1234567890123L, bh.ReadInt64(8));
        }
        
        [Fact]
        public void ReadWriteFloatTest()
        {
            var bh = new ByteHeap();

            bh.Alloc(10);
            bh.Write(0, 123.0f);
            bh.Write(4, 123456.0f);
            
            Assert.Equal(123.0f, bh.ReadFloat(0));
            Assert.Equal(123456.0f, bh.ReadFloat(4));
        }
        
        [Fact]
        public void ReadWriteDoubleTest()
        {
            var bh = new ByteHeap();

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
            var bh = new ByteHeap();

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
            var bh = new ByteHeap();

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
            var bh = new ByteHeap();

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
            var bh = new ByteHeap();

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
            var bh = new ByteHeap();

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
    }
}
