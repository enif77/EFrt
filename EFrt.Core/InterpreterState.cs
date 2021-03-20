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

        public InputSourceStack InputSourceStack { get; }
        
        public ByteHeap Heap { get; }

        public ObjectHeap ObjectHeap { get; }

        public string Picture { get; set; }
        
        public int StateVariableAddress { get; }

        public int BaseVariableAddress { get; }

        public IWordsList WordsList { get; }


        public InterpreterState(Stack stack, FloatingPointStack floatingPointStack, ObjectStack objectStack, ReturnStack returnStack, ExceptionStack exceptionStack, InputSourceStack inputSourceStack, ByteHeap heap, ObjectHeap objectHeap, IWordsList wordsList)
        {
            Stack = stack ?? throw new ArgumentNullException(nameof(stack));
            FloatingPointStack = floatingPointStack ?? throw new ArgumentNullException(nameof(floatingPointStack));
            ObjectStack = objectStack ?? throw new ArgumentNullException(nameof(objectStack));
            ReturnStack = returnStack ?? throw new ArgumentNullException(nameof(returnStack));
            ExceptionStack = exceptionStack ?? throw new ArgumentNullException(nameof(exceptionStack));
            InputSourceStack = inputSourceStack ?? throw new ArgumentNullException(nameof(inputSourceStack));
            Heap = heap ?? throw new ArgumentNullException(nameof(heap));
            ObjectHeap = objectHeap ?? throw new ArgumentNullException(nameof(objectHeap));
            Picture = string.Empty;
            WordsList = wordsList ?? throw new ArgumentNullException(nameof(wordsList));

            // Allocate room for the STATE and the BASE variables.
            var systemVarsIndex = Heap.AllocCells(2);

            // Setup the variables...
            StateVariableAddress = systemVarsIndex;
            BaseVariableAddress = systemVarsIndex + ByteHeap.CellSize;

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
            InputSourceStack.Clear();
            Heap.Clear();
            ObjectHeap.Clear();
            Picture = string.Empty;
            WordsList.Clear();
        }


        public void SetStateValue(bool value)
        {
            Heap.Write(StateVariableAddress, value ? -1 : 0);
        }


        public void SetBaseValue(int value)
        {
            if (value < 2 || value > 36) throw new ArgumentOutOfRangeException(nameof(value), "The allowed BASE values are <2 .. 36>.");

            Heap.Write(BaseVariableAddress, value);
        }
    }
}