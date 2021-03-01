/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests
{
    using System;

    using Xunit;

    using EFrt;
    using EFrt.Core;
    
    
    public class InterpreterHeapExtensionsTests
    {
        public InterpreterHeapExtensionsTests()
        {
            _interpreter = InterpreterFactory.CreateWithDefaults();
        }

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
       

        private readonly IInterpreter _interpreter;
    }
}

/*
 
 
  
 */