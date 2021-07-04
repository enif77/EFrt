/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt_Tests
{
    using System;

    using Xunit;

    using EFrt.Core.Stacks;


    public class StackTests
    {
        [Fact]
        public void NewStackHasDefinedCapacityTest()
        {
            var s = new Stack(10);

            Assert.Equal(10, s.Items.Length);
        }
        
        [Fact]
        public void NewStackIsEmptyTest()
        {
            var s = new Stack();

            Assert.Equal(0, s.Count);
        }
        
        [Fact]
        public void StackInitClearsStackContentTest()
        {
            var s = new Stack(10);

            s.Push(123);

            Assert.Equal(1, s.Count);
            
            for (var i = 0; i < s.Items.Length; i++)
            {
                s.Items[i] = i + 1;
            }

            Assert.Equal(1, s.Items[0]);
            Assert.Equal(5, s.Items[4]);
            Assert.Equal(8, s.Items[7]);
            
            s.Init(0);
            
            Assert.Equal(0, s.Count);
            
            foreach (var t in s.Items)
            {
                Assert.Equal(0, t);
            }
        }
        
        [Fact]
        public void StackContainsOneTest()
        {
            var s = new Stack();

            s.Push(123);

            Assert.Equal(1, s.Count);
        }
        
        [Fact]
        public void PeekTest()
        {
            var s = new Stack();

            s.Push(123);
            s.Push(456);
            s.Push(789);

            Assert.Equal(789, s.Peek());
        }

        [Fact]
        public void PickTest()
        {
            var s = new Stack(10);

            s.Push(123);
            s.Push(456);
            s.Push(789);

            Assert.Equal(789, s.Pick(0));
            Assert.Equal(456, s.Pick(1));
            Assert.Equal(123, s.Pick(2));
        }
        
        [Fact]
        public void PushTest()
        {
            var s = new Stack();

            Push123(s);

            Assert.Equal(3, s.Pick(0));
            Assert.Equal(2, s.Pick(1));
            Assert.Equal(1, s.Pick(2));
        }

        [Fact]
        public void PopTest()
        {
            var s = new Stack();

            Push123(s);

            Assert.Equal(3, s.Peek());
            Assert.Equal(3, s.Pop());
            Assert.Equal(2, s.Peek());
        }

        [Fact]
        public void DupTest()
        {
            var s = new Stack();

            s.Push(123);
            
            Assert.Equal(123, s.Items[0]);
            Assert.Equal(0, s.Items[1]);

            s.Dup();

            Assert.Equal(123, s.Items[0]);
            Assert.Equal(123, s.Items[1]);
        }
        
        [Fact]
        public void DropTest()
        {
            var s = new Stack();

            Push123(s);
            
            Assert.Equal(3, s.Peek());

            s.Drop();

            Assert.Equal(2, s.Peek());
        }
        
        [Fact]
        public void SwapTest()
        {
            var s = new Stack();

            s.Push(123);
            s.Push(456);
            
            Assert.Equal(123, s.Items[0]);
            Assert.Equal(456, s.Items[1]);

            s.Swap();

            Assert.Equal(456, s.Items[0]);
            Assert.Equal(123, s.Items[1]);
        }
        
        [Fact]
        public void OverTest()
        {
            var s = new Stack();

            s.Push(123);
            s.Push(456);
            
            Assert.Equal(123, s.Items[0]);
            Assert.Equal(456, s.Items[1]);
            Assert.Equal(0, s.Items[2]);

            s.Over();

            Assert.Equal(123, s.Items[0]);
            Assert.Equal(456, s.Items[1]);
            Assert.Equal(123, s.Items[2]);
        }

        [Fact]
        public void RotTest()
        {
            var s = new Stack();

            Push123(s);
            
            Assert.Equal(1, s.Items[0]);
            Assert.Equal(2, s.Items[1]);
            Assert.Equal(3, s.Items[2]);

            s.Rot();

            Assert.Equal(2, s.Items[0]);
            Assert.Equal(3, s.Items[1]);
            Assert.Equal(1, s.Items[2]);
        }
        
        [Fact]
        public void RollTest()
        {
            var s = new Stack();

            for (var i = 1; i <= 10; i++)
            {
                s.Push(i);
            }

            Assert.Equal(2, s.Items[1]);
            Assert.Equal(3, s.Items[2]);
            Assert.Equal(10, s.Items[9]);
            Assert.Equal(0, s.Items[10]);

            s.Roll(7);

            Assert.Equal(2, s.Items[1]);
            Assert.Equal(4, s.Items[2]);
            Assert.Equal(3, s.Items[9]);
            Assert.Equal(0, s.Items[10]);
        }


        /// <summary>
        /// ( - 1 2 3)
        /// </summary>
        /// <param name="stack">A stack.</param>
        private void Push123(IGenericStack<int> stack)
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
        }
    }
}
