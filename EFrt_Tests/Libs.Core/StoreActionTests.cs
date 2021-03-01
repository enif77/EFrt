/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests.Libs.Core
{
    using System;

    using Xunit;

    using EFrt;
    using EFrt.Core;
    
    
    public class StoreActionTests
    {
        public StoreActionTests()
        {
            // https://xunit.net/docs/shared-context
            
            _interpreter = InterpreterFactory.CreateWithDefaults();
            _interpreter.AddCoreLibrary();
        }

        [Fact]
        public void StoreToAddressTest()
        {
            Assert.Equal(0, _interpreter.State.Heap.ReadInt32(4));
            
            _interpreter.Evaluate("123 4 !");
            
            Assert.Equal(123, _interpreter.State.Heap.ReadInt32(4));
            
            _interpreter.Evaluate("4 @");
            
            Assert.Equal(123, _interpreter.Pop());
        }
        
        // [Fact]
        // public void StoreToNotCellAlignedAddressTest()
        // {
        //     // 3 = not cell-aligned address.
        //     var exception = Assert.Throws<InterpreterException>(() => _interpreter.Evaluate("123 3 !"));
        //     
        //     // -23 address alignment exception
        //     Assert.Equal(-9, exception.ExceptionCode);
        // }

        private readonly IInterpreter _interpreter;
    }
}

/*
 
 
  
 */