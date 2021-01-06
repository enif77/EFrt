/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core
{
    using System;

    using EFrt.Core.Stacks;


    public class InterpreterState : IInterpreterState
    {
        public Stack Stack { get; }

        public ObjectStack ObjectStack { get; }

        public ReturnStack ReturnStack { get; }

        public ExceptionStack ExceptionStack { get; }

        public Heap Heap { get; }

        public ObjectHeap ObjectHeap { get; }

        public IWordsList WordsList { get; }


        public InterpreterState(Stack stack, ObjectStack objectStack, ReturnStack returnStack, ExceptionStack exceptionStack, Heap heap, ObjectHeap objectHeap, IWordsList wordsList)
        {
            Stack = stack ?? throw new ArgumentNullException(nameof(stack));
            ObjectStack = objectStack ?? throw new ArgumentNullException(nameof(objectStack));
            ReturnStack = returnStack ?? throw new ArgumentNullException(nameof(returnStack));
            ExceptionStack = exceptionStack ?? throw new ArgumentNullException(nameof(exceptionStack));
            Heap = heap ?? throw new ArgumentNullException(nameof(heap));
            ObjectHeap = objectHeap ?? throw new ArgumentNullException(nameof(objectHeap));
            WordsList = wordsList ?? throw new ArgumentNullException(nameof(wordsList));
        }


        public void Reset()
        {
            Stack.Clear();
            ObjectStack.Clear();
            ReturnStack.Clear();
            ExceptionStack.Clear();
            Heap.Clear();
            ObjectHeap.Clear();
            WordsList.Clear();
        }
    }
}