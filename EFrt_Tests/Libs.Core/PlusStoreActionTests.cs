/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests.Libs.Core
{
    using Xunit;

    using EFrt;
    using EFrt.Core;
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
            Assert.Equal(0, _interpreter.State.Heap.ReadInt32(4));
            
            // 123
            _interpreter.State.Heap.Write(4, 123);
            
            // -> 5 4 +!
            _interpreter.Push(5);
            _interpreter.Push(4);
            _plusStoreWord.Action();
            
            Assert.Equal(123 + 5, _interpreter.State.Heap.ReadInt32(4));
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
