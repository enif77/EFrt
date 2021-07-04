/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests
{
    using Xunit;

    using EFrt;
    using EFrt.Core;
    using EFrt.Core.Extensions;
    
    
    /// <summary>
    /// Tests for heap related extensions.
    /// </summary>
    public class InterpreterHeapExtensionsTests
    {
        public InterpreterHeapExtensionsTests()
        {
            _interpreter = InterpreterFactory.CreateWithDefaults();
        }

        // --- CELL ALIGNED ADDRESSES TESTS ---
        
        [Fact]
        public void ValidCellAlignedAddressTest()
        {
            // 4 = valid address.
            _interpreter.CheckCellAlignedAddress(4);
            
            // Heap.Items.Length - 4 = valid address.
            _interpreter.CheckCellAlignedAddress(_interpreter.State.Heap.Items.Length - 4);
        }
        
        [Fact]
        public void InvalidCellAlignedAddressTest()
        {
            // -1 = invalid address.
            var exception = Assert.Throws<InterpreterException>(() => _interpreter.CheckCellAlignedAddress(-1));
            
            // -9 invalid memory address
            Assert.Equal(-9, exception.ExceptionCode);
            
            // Heap.Items.Length + 100 = invalid address.
            exception = Assert.Throws<InterpreterException>(() => _interpreter.CheckCellAlignedAddress(_interpreter.State.Heap.Items.Length + 100));
            
            // -9 invalid memory address
            Assert.Equal(-9, exception.ExceptionCode);
        }
        
        [Fact]
        public void CellAlignedAddressTest()
        {
            // 0 = cell-aligned address.
            _interpreter.CheckCellAlignedAddress(0);
            
            // 4 = cell-aligned address.
            _interpreter.CheckCellAlignedAddress(4);
        }
        
        [Fact]
        public void CellNotAlignedAddressTest()
        {
            // 3 = not cell-aligned address.
            var exception = Assert.Throws<InterpreterException>(() => _interpreter.CheckCellAlignedAddress(3));
            
            // -23 address alignment exception
            Assert.Equal(-23, exception.ExceptionCode);
        }
       
        // --- ADDRESS RANGE CHECKS ---
        
        [Fact]
        public void ValidAddressesRangeTest()
        {
            // 10 first bytes of the heap.
            _interpreter.CheckAddressesRange(0, 10);
            
            // All bytes of the heap.
            _interpreter.CheckAddressesRange(0, _interpreter.State.Heap.Items.Length);
        }
        
        [Fact]
        public void InvalidAddressesRangeTest()
        {
            // Range start before first bytes of the heap.
            var exception = Assert.Throws<InterpreterException>(() => _interpreter.CheckAddressesRange(-5, 10));
            
            // -9 invalid memory address
            Assert.Equal(-9, exception.ExceptionCode);
            
            // Range after the last bytes of the heap.
            exception = Assert.Throws<InterpreterException>(() => _interpreter.CheckAddressesRange(_interpreter.State.Heap.Items.Length + 10, 10));
            
            // -9 invalid memory address
            Assert.Equal(-9, exception.ExceptionCode);
            
            // Range end after the last bytes of the heap.
            exception = Assert.Throws<InterpreterException>(() => _interpreter.CheckAddressesRange(10, _interpreter.State.Heap.Items.Length));
            
            // -9 invalid memory address
            Assert.Equal(-9, exception.ExceptionCode);
            
            // Count is zero.
            exception = Assert.Throws<InterpreterException>(() => _interpreter.CheckAddressesRange(10, 0));
            
            // -21 unsupported operation
            Assert.Equal(-21, exception.ExceptionCode);
            
            // Count is less than zero.
            exception = Assert.Throws<InterpreterException>(() => _interpreter.CheckAddressesRange(10, -1));
            
            // -21 unsupported operation
            Assert.Equal(-21, exception.ExceptionCode);
        }

        private readonly IInterpreter _interpreter;
    }
}

/*
 
 
  
 */