﻿/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt
{
    using System;


    /// <summary>
    /// Interpreter configuration.
    /// </summary>
    public class InterpreterConfiguration
    {
        /// <summary>
        /// The default stack size in cells.
        /// </summary>
        public const int DefaultStackSize = 32;

        /// <summary>
        /// The default object stack size in cells.
        /// </summary>
        public const int DefaultObjectStackSize = 32;

        /// <summary>
        /// The default return stack size in cells.
        /// </summary>
        public const int DefaultReturnStackSize = 32;

        /// <summary>
        /// The default exception stack size in cells.
        /// </summary>
        public const int DefaultExceptionStackSize = 32;

        /// <summary>
        /// The default initial heap size in cells.
        /// </summary>
        public const int DefaultInitialHeapSize = 1024;

        /// <summary>
        /// The default initial object heap size in cells.
        /// </summary>
        public const int DefaultInitialObjectHeapSize = 1024;


        /// <summary>
        /// The requested stack size in cells.
        /// </summary>
        public int StackSize { get; set; } = DefaultStackSize;

        /// <summary>
        /// The requested object stack size in cells.
        /// </summary>
        public int ObjectStackSize { get; set; } = DefaultObjectStackSize;

        /// <summary>
        /// The requested return stack size in cells.
        /// </summary>
        public int ReturnStackSize { get; set; } = DefaultReturnStackSize;

        /// <summary>
        /// The requested exception stack size in cells.
        /// </summary>
        public int ExceptionStackSize { get; set; } = DefaultExceptionStackSize;

        /// <summary>
        /// The requested initial heap size in cells.
        /// </summary>
        public int InitialHeapSize { get; set; } = DefaultInitialHeapSize;

        /// <summary>
        /// The requested initial object heap size in cells.
        /// </summary>
        public int InitialObjectHeapSize { get; set; } = DefaultInitialObjectHeapSize;


        /// <summary>
        /// Validates this configuration.
        /// </summary>
        public void Validate()
        {
            if (StackSize < 0) throw new Exception(nameof(StackSize) + " out of the <0 .. Int32.Max> range: " + StackSize);
            if (ObjectStackSize < 0) throw new Exception(nameof(ObjectStackSize) + " out of the <0 .. Int32.Max> range: " + ObjectStackSize);
            if (ReturnStackSize < 0) throw new Exception(nameof(ReturnStackSize) + " out of the <0 .. Int32.Max> range: " + ReturnStackSize);
            if (ExceptionStackSize < 0) throw new Exception(nameof(ExceptionStackSize) + " out of the <0 .. Int32.Max> range: " + ExceptionStackSize);
            if (InitialHeapSize < 0) throw new Exception(nameof(InitialHeapSize) + " out of <0 .. Int32.Max> range: " + InitialHeapSize);
            if (InitialObjectHeapSize < 0) throw new Exception(nameof(InitialHeapSize) + " out of <0 .. Int32.Max> range: " + InitialObjectHeapSize);
        }
    }
}
