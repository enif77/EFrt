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
                new ReturnStack(configuration.ReturnStackSize),
                new ExceptionStack(configuration.ExceptionStackSize),
                new Heap(configuration.InitialHeapSize),
                new ObjectHeap(configuration.InitialObjectHeapSize),
                new WordsList()));
        }

        /// <summary>
        /// Extension method, that adds all words from a library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <param name="wordsLibrary">A words library.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddLibrary(this IInterpreter interpreter, IWordsLIbrary wordsLibrary)
        {
            if (wordsLibrary == null) throw new ArgumentNullException(nameof(wordsLibrary));

            wordsLibrary.DefineWords();

            return interpreter;
        }

        /// <summary>
        /// Extension method, that adds all words from the CORE library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddCoreLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.Core.Library(interpreter));
        }

        /// <summary>
        /// Extension method, that adds all words from the CORE-EXT library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddCoreExtLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.CoreExt.Library(interpreter));
        }

        /// <summary>
        /// Extension method, that adds all words from the DOUBLE library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddDoubleLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.Double.Library(interpreter));
        }

        /// <summary>
        /// Extension method, that adds all words from the DOUBLE-EXT library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddDoubleExtLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.DoubleExt.Library(interpreter));
        }

        /// <summary>
        /// Extension method, that adds all words from the EXCEPTION library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddExceptionLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.Exception.Library(interpreter));
        }

        /// <summary>
        /// Extension method, that adds all words from the FLOATING library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddFloatingLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.Floating.Library(interpreter));
        }

        /// <summary>
        /// Extension method, that adds all words from the FLOATING-EXT library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddFloatingExtLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.FloatingExt.Library(interpreter));
        }

        /// <summary>
        /// Extension method, that adds all words from the OBJECT library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddObjectLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.Object.Library(interpreter));
        }

        /// <summary>
        /// Extension method, that adds all words from the STRING library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddStringLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.String.Library(interpreter));
        }

        /// <summary>
        /// Extension method, that adds all words from the TOOLS library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddToolsLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.Tools.Library(interpreter));
        }

        /// <summary>
        /// Extension method, that adds all words from the TOOLS-EXT library to an interpreter words list.
        /// </summary>
        /// <param name="interpreter">An interpreter instance.</param>
        /// <returns>The interpreter.</returns>
        public static IInterpreter AddToolsExtLibrary(this IInterpreter interpreter)
        {
            return AddLibrary(interpreter, new EFrt.Libs.ToolsExt.Library(interpreter));
        }
    }
}
