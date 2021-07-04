/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests.Libs.Core
{
    using Xunit;

    using EFrt;
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;
    
    
    public class PlusStoreActionTests
    {
        public PlusStoreActionTests()
        {
            _interpreter = InterpreterFactory.CreateWithDefaults();
            _interpreter.AddCoreLibrary();
            _plusStoreWord = _interpreter.GetWord("+!");
        }

        [Fact]
        public void IsPlusStoreWordTest()
        {
            Assert.Equal("+!", _plusStoreWord.Name);
        }

        [Fact]
        public void StoreToAddressTest()
        {
            var addr = 32;

            Assert.Equal(0, _interpreter.State.Heap.ReadInt32(addr));
            
            // 123
            _interpreter.State.Heap.Write(addr, 123);
            
            // -> 5 32 +!
            _interpreter.Push(5);
            _interpreter.Push(addr);
            _plusStoreWord.Action();
            
            Assert.Equal(123 + 5, _interpreter.State.Heap.ReadInt32(addr));
        }
        
        [Fact]
        public void StoreToNotCellAlignedAddressTest()
        {
            // 123
            _interpreter.State.Heap.Write(3, 123);
            
            // -> 5 4 +!
            _interpreter.Push(5);
            _interpreter.Push(3);
            
            var exception = Assert.Throws<InterpreterException>(()
                => _plusStoreWord.Action());
            
            // -23 address alignment exception
            Assert.Equal(-23, exception.ExceptionCode);
        }

        [Fact]
        public void ExpectedParametersNotFound()
        {
            // The value parameter is missing.
            _interpreter.Push(3);
            
            var exception = Assert.Throws<InterpreterException>(()
                => _plusStoreWord.Action());
            
            // -4 stack underflow
            Assert.Equal(-4, exception.ExceptionCode);
        }

        private readonly IInterpreter _interpreter;
        private readonly IWord _plusStoreWord;
    }
}
