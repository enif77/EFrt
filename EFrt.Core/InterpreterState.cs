/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;

    using EFrt.Core.Stacks;


    public class InterpreterState : IInterpreterState
    {
        public Stack Stack { get; }
        public ReturnStack ReturnStack { get; }
        public ExceptionStack ExceptionStack { get; }
        public Heap Heap { get; }
        public ObjectHeap ObjectHeap { get; }
        public IWordsList WordsList { get; }


        public InterpreterState(Stack stack, ReturnStack returnStack, ExceptionStack exceptionStack, Heap heap, ObjectHeap objectHeap, IWordsList wordsList)
        {
            Stack = stack ?? throw new ArgumentNullException(nameof(stack));
            ReturnStack = returnStack ?? throw new ArgumentNullException(nameof(returnStack));
            ExceptionStack = exceptionStack ?? throw new ArgumentNullException(nameof(exceptionStack));
            Heap = heap ?? throw new ArgumentNullException(nameof(heap));
            ObjectHeap = objectHeap ?? throw new ArgumentNullException(nameof(objectHeap));
            WordsList = wordsList ?? throw new ArgumentNullException(nameof(wordsList));
        }


        public void Reset()
        {
            Stack.Clear();
            ReturnStack.Clear();
            ExceptionStack.Clear();
            Heap.Clear();
            ObjectHeap.Clear();
            WordsList.Clear();
        }
    }
}