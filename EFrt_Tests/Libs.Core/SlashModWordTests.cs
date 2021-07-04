/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt_Tests.Libs.Core
{
    using Xunit;

    using EFrt;
    using EFrt.Core;
    using EFrt.Core.Extensions;
    using EFrt.Core.Words;
    
    
    public class SlashModWordTests
    {
        public SlashModWordTests()
        {
            _interpreter = InterpreterFactory.CreateWithDefaults();
            _interpreter.AddCoreLibrary();
            _slashModWord = _interpreter.GetWord("/MOD");
        }

        [Fact]
        public void IsSlashModWordTest()
        {
            Assert.Equal("/MOD", _slashModWord.Name);
        }

        [Fact]
        public void ExpectedValuesTest()
        {
            // -> 123 4 /MOD
            _interpreter.Push(123);
            _interpreter.Push(4);
            _slashModWord.Action();

            Assert.Equal(30, _interpreter.Pop());  // n4
            Assert.Equal(3, _interpreter.Pop());  // n3
        }

        private readonly IInterpreter _interpreter;
        private readonly IWord _slashModWord;
    }
}
