/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using EFrt.Core;
    using EFrt.Core.Stacks;


    public static class InterpreterFactory
    {
        /// <summary>
        /// Creates a new IInterpreter instance with default settings.
        /// </summary>
        /// <returns>A new IInterpreter instance with default settings.</returns>
        public static IInterpreter CreateWithDefaults()
        {
            return new Interpreter(new InterpreterState(
                new Stack(32),
                new ObjectStack(32),
                new ReturnStack(32),
                new Heap(1024),
                new ObjectHeap(1024),
                new WordsList()));
        }
    }
}
