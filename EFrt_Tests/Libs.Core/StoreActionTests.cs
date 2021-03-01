/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests.Libs.Core
{
    using Xunit;

    using EFrt;
    using EFrt.Core;
    using EFrt.Core.Words;
    
    
    public class StoreActionTests
    {
        public StoreActionTests()
        {
            // https://xunit.net/docs/shared-context
            
            _interpreter = InterpreterFactory.CreateWithDefaults();
            _interpreter.AddCoreLibrary();
            _storeWord = _interpreter.GetWord("!");
            _fetchWord = _interpreter.GetWord("@");
        }

        [Fact]
        public void IsStoreWordTest()
        {
            Assert.Equal("!", _storeWord.Name);
        }
        
        [Fact]
        public void IsFetchWordTest()
        {
            Assert.Equal("@", _fetchWord.Name);
        }

        [Fact]
        public void StoreToAddressTest()
        {
            Assert.Equal(0, _interpreter.State.Heap.ReadInt32(4));
            
            // -> 123 4 !
            _interpreter.Push(123);
            _interpreter.Push(4);
            _storeWord.Action();
            
            Assert.Equal(123, _interpreter.State.Heap.ReadInt32(4));
            
            // -> 4 @
            _interpreter.Push(4);
            _fetchWord.Action();
            
            Assert.Equal(123, _interpreter.Pop());
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

        private readonly IInterpreter _interpreter;
        private readonly IWord _storeWord;
        private readonly IWord _fetchWord;
    }
}
