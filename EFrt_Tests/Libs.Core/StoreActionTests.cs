/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests.Libs.Core
{
    using Xunit;

    using EFrt;
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;
    
    
    public class StoreActionTests
    {
        public StoreActionTests()
        {
            // https://xunit.net/docs/shared-context
            
            _interpreter = InterpreterFactory.CreateWithDefaults();
            _interpreter.AddCoreLibrary();
            _storeWord = _interpreter.GetWord("!");
        }

        [Fact]
        public void IsStoreWordTest()
        {
            Assert.Equal("!", _storeWord.Name);
        }

        [Fact]
        public void StoreToAddressTest()
        {
            var addr = 32;

            Assert.Equal(0, _interpreter.State.Heap.ReadInt32(addr));
            
            // -> 123 32 !
            _interpreter.Push(123);
            _interpreter.Push(addr);
            _storeWord.Action();
            
            Assert.Equal(123, _interpreter.State.Heap.ReadInt32(addr));
            
            // -> 1_234_567_890 32 !
            _interpreter.Push(1_234_567_890);
            _interpreter.Push(addr);
            _storeWord.Action();
            
            Assert.Equal(1_234_567_890, _interpreter.State.Heap.ReadInt32(addr));
        }
        
        [Fact]
        public void StoreToNotCellAlignedAddressTest()
        {
            // -> 123 3 !
            // 3 = not cell-aligned address.
            _interpreter.Push(123);
            _interpreter.Push(3);
            
            var exception = Assert.Throws<InterpreterException>(()
                => _storeWord.Action());
            
            // -23 address alignment exception
            Assert.Equal(-23, exception.ExceptionCode);
        }

        [Fact]
        public void ExpectedParametersNotFound()
        {
            // The value parameter is missing.
            _interpreter.Push(3);
            
            var exception = Assert.Throws<InterpreterException>(()
                => _storeWord.Action());
            
            // -4 stack underflow
            Assert.Equal(-4, exception.ExceptionCode);
        }

        private readonly IInterpreter _interpreter;
        private readonly IWord _storeWord;
    }
}
