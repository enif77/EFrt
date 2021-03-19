/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests.Libs.CoreExt
{
    using Xunit;

    using EFrt;
    using EFrt.Core;
    using EFrt.Core.Words;
    
    
    public class WithinWordTests
    {
        public WithinWordTests()
        {
            _interpreter = InterpreterFactory.CreateWithDefaults();
            _interpreter.AddCoreExtLibrary();
            _withinWord = _interpreter.GetWord("WITHIN");
        }

        [Fact]
        public void IsWithinWordTest()
        {
            Assert.Equal("WITHIN", _withinWord.Name);
        }
        
        // Inside interval tests
        
        [Fact]
        public void IsBetweenTest()
        {
            _interpreter.Push(10);
            _interpreter.Push(5);
            _interpreter.Push(15);
            _withinWord.Action();
            
            Assert.Equal(-1, _interpreter.Pop());
        }
        
        [Fact]
        public void IsBetweenWhenEqualToBottomEndTest()
        {
            _interpreter.Push(5);
            _interpreter.Push(5);
            _interpreter.Push(15);
            _withinWord.Action();
            
            Assert.Equal(-1, _interpreter.Pop());
        }
        
        [Fact]
        public void IsNotBetweenBelowTest()
        {
            _interpreter.Push(3);
            _interpreter.Push(5);
            _interpreter.Push(15);
            _withinWord.Action();
            
            Assert.Equal(0, _interpreter.Pop());
        }
        
        [Fact]
        public void IsNotBetweenAboveTest()
        {
            _interpreter.Push(20);
            _interpreter.Push(5);
            _interpreter.Push(15);
            _withinWord.Action();
            
            Assert.Equal(0, _interpreter.Pop());
        }
        
        [Fact]
        public void IsNotBetweenWhenEqualToTopEndTest()
        {
            _interpreter.Push(15);
            _interpreter.Push(5);
            _interpreter.Push(15);
            _withinWord.Action();
            
            Assert.Equal(0, _interpreter.Pop());
        }
        
        // Outside interval tests
        
        [Fact]
        public void IsOutsideBelowTest()
        {
            _interpreter.Push(0);
            _interpreter.Push(15);
            _interpreter.Push(5);
            _withinWord.Action();
            
            Assert.Equal(-1, _interpreter.Pop());
        }
        
        [Fact]
        public void IsOutsideAboveTest()
        {
            _interpreter.Push(20);
            _interpreter.Push(15);
            _interpreter.Push(5);
            _withinWord.Action();
            
            Assert.Equal(-1, _interpreter.Pop());
        }
        
        [Fact]
        public void IsNotOutsideWhenEqualToBottomEndTest()
        {
            _interpreter.Push(5);
            _interpreter.Push(15);
            _interpreter.Push(5);
            _withinWord.Action();
            
            Assert.Equal(0, _interpreter.Pop());
        }
        
        [Fact]
        public void IsNotOutsideTest()
        {
            _interpreter.Push(10);
            _interpreter.Push(15);
            _interpreter.Push(5);
            _withinWord.Action();
            
            Assert.Equal(0, _interpreter.Pop());
        }
        
        [Fact]
        public void IsOutsideWhenEqualToTopEndTest()
        {
            _interpreter.Push(15);
            _interpreter.Push(15);
            _interpreter.Push(5);
            _withinWord.Action();
            
            Assert.Equal(-1, _interpreter.Pop());
        }

        private readonly IInterpreter _interpreter;
        private readonly IWord _withinWord;
    }
}
