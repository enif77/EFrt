/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt_Tests
{
    using System;

    using Xunit;

    using EFrt.Stacks;


    public class StackTests
    {
        [Fact]
        public void NewStackIsEmptyTest()
        {
            var s = new ReturnStack();

            Assert.Equal(0, s.Count);
        }

        [Fact]
        public void StackContainsOneTest()
        {
            var s = new ReturnStack();

            s.Push(123);

            Assert.Equal(1, s.Count);
        }

        [Fact]
        public void Push123Test()
        {
            var s = new ReturnStack();

            s.Push(123);

            Assert.Equal(123, s.Pick(s.Top));
        }

        [Fact]
        public void PopTest()
        {
            var s = new ReturnStack();

            Push123(s);

            Assert.Equal(3, s.Pop());
            Assert.Equal(2, s.Peek());
        }

        [Fact]
        public void DropTest()
        {
            var s = new ReturnStack();

            Push123(s);
            
            Assert.Equal(3, s.Peek());

            s.Drop();

            Assert.Equal(2, s.Peek());
        }



        /// <summary>
        /// ( - 1 2 3)
        /// </summary>
        /// <param name="stack">A stack.</param>
        private void Push123(IStack<int> stack)
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
        }
    }
}
