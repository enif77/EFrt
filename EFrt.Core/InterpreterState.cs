/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using System;

    using EFrt.Core.Stacks;


    public class InterpreterState : IInterpreterState
    {
        public Stack Stack { get; }

        public FloatingPointStack FloatingPointStack { get; }

        public ObjectStack ObjectStack { get; }

        public ReturnStack ReturnStack { get; }

        public ExceptionStack ExceptionStack { get; }

        public Heap Heap { get; }

        public ObjectHeap ObjectHeap { get; }

        public int StateVariableAddress { get; }

        public int BaseVariableAddress { get; }

        public IWordsList WordsList { get; }


        public InterpreterState(Stack stack, FloatingPointStack floatingPointStack, ObjectStack objectStack, ReturnStack returnStack, ExceptionStack exceptionStack, Heap heap, ObjectHeap objectHeap, IWordsList wordsList)
        {
            Stack = stack ?? throw new ArgumentNullException(nameof(stack));
            FloatingPointStack = floatingPointStack ?? throw new ArgumentNullException(nameof(floatingPointStack));
            ObjectStack = objectStack ?? throw new ArgumentNullException(nameof(objectStack));
            ReturnStack = returnStack ?? throw new ArgumentNullException(nameof(returnStack));
            ExceptionStack = exceptionStack ?? throw new ArgumentNullException(nameof(exceptionStack));
            Heap = heap ?? throw new ArgumentNullException(nameof(heap));
            ObjectHeap = objectHeap ?? throw new ArgumentNullException(nameof(objectHeap));
            WordsList = wordsList ?? throw new ArgumentNullException(nameof(wordsList));

            // Allocate room for the STATE and the BASE variables.
            var systemVarsIndex = Heap.Alloc(2);

            // Setup the variables...
            StateVariableAddress = systemVarsIndex;
            BaseVariableAddress = systemVarsIndex + 1;

            // ... with default values.
            SetStateValue(false);  // False = interpreting.
            SetBaseValue(10);      // Decimal.
        }


        public void Reset()
        {
            Stack.Clear();
            FloatingPointStack.Clear();
            ObjectStack.Clear();
            ReturnStack.Clear();
            ExceptionStack.Clear();
            Heap.Clear();
            ObjectHeap.Clear();
            WordsList.Clear();
        }


        public void SetStateValue(bool value)
        {
            Heap.Items[StateVariableAddress] = value ? -1 : 0;
        }


        public void SetBaseValue(int value)
        {
            if (value < 2 || value > 36) throw new ArgumentOutOfRangeException(nameof(value), "The alloved BASE values are <2 .. 36>.");

            Heap.Items[BaseVariableAddress] = value;
        }
    }
}