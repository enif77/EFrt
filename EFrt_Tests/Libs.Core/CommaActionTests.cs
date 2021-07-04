/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests.Libs.Core
{
    using Xunit;

    using EFrt;
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;
    
    
    public class CommaActionTests
    {
        public CommaActionTests()
        {
            _interpreter = InterpreterFactory.CreateWithDefaults();
            _interpreter.AddCoreLibrary();
            _commaWord = _interpreter.GetWord(",");
        }
        
        [Fact]
        public void IsCommaWordTest()
        {
            Assert.Equal(",", _commaWord.Name);
        }
        
        [Fact]
        public void StoreToAddressTest()
        {
            var here = _interpreter.State.Heap.Top + 1;

            Assert.Equal(0, _interpreter.State.Heap.ReadInt32(here));
            
            // -> 123 4 !
            _interpreter.Push(123);
            _commaWord.Action();
            
            Assert.Equal(123, _interpreter.State.Heap.ReadInt32(here));

            var here2 = _interpreter.State.Heap.Top + 1;

            // -> 1_234_567_890 4 !
            _interpreter.Push(1_234_567_890);
            _commaWord.Action();
            
            Assert.Equal(1_234_567_890, _interpreter.State.Heap.ReadInt32(here2));
        }
        
        [Fact]
        public void StoreToNotCellAlignedAddressTest()
        {
            // 3 ALLOC 123
            // 3 = not cell-aligned HERE address.
            _interpreter.State.Heap.Alloc(3);
            _interpreter.Push(123);
            
            var exception = Assert.Throws<InterpreterException>(()
                => _commaWord.Action());
            
            // -23 address alignment exception
            Assert.Equal(-23, exception.ExceptionCode);
        }

        [Fact]
        public void ExpectedParametersNotFound()
        {
            // The value parameter is missing.
            var exception = Assert.Throws<InterpreterException>(()
                => _commaWord.Action());
            
            // -4 stack underflow
            Assert.Equal(-4, exception.ExceptionCode);
        }
        
        
        private readonly IInterpreter _interpreter;
        private readonly IWord _commaWord;
    }
}