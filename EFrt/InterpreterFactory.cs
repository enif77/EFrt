/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt
{
    using System;

    using EFrt.Core;
    using EFrt.Core.Stacks;


    /// <summary>
    /// Helper methods for building an interpreter instance.
    /// </summary>
    public static class InterpreterFactory
    {
        /// <summary>
        /// Creates a new IInterpreter instance with default settings.
        /// </summary>
        /// <returns>A new IInterpreter instance with default settings.</returns>
        public static IInterpreter CreateWithDefaults()
        {
            return Create(new InterpreterConfiguration());
        }

        /// <summary>
        /// Creates a new IInterpreter instance with provided configuration.
        /// </summary>
        /// <returns>A new IInterpreter instance.</returns>
        public static IInterpreter Create(InterpreterConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            configuration.Validate();

            return new Interpreter(new InterpreterState(
                new Stack(configuration.StackSize),
                new ObjectStack(configuration.ObjectStackSize),
                new ReturnStack(configuration.ObjectStackSize),
                new ExceptionStack(configuration.ExceptionStackSize),
                new Heap(configuration.InitialHeapSize),
                new ObjectHeap(configuration.InitialObjectHeapSize),
                new WordsList()));
        }
    }
}
