/* EFrt - (C) 2020 - 2021 Premysl Fara  */

namespace EFrt.Core
{
    using EFrt.Core.Stacks;
    
    
    /// <summary>
    /// Extensions method for the heap manipulations.
    /// </summary>
    public static class InterpreterHeapExtensions
    {
        #region address checks

        /// <summary>
        /// Checks, if an address is byte aligned and if it is from the 0 .. Heap.Length - 1 range.
        /// Wont return (throws an InterpreterException), if the address is not byte aligned.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="addr">An address.</param>
        public static void CheckByteAlignedAddress(this IInterpreter interpreter, int addr)
        {
            // -9 invalid memory address
            if (addr < 0 || addr >= interpreter.State.Heap.Items.Length) throw new InterpreterException(-9, $"The address {addr} is out of the <0 .. Heap.Length) range.");
            
            if (interpreter.State.Heap.IsByteAligned(addr) == false)
            {
                // -23 address alignment exception
                throw new InterpreterException(-23, $"The {addr} is not a byte aligned address.");
            }
        }
        
        /// <summary>
        /// Checks, if the next allocated space address is byte aligned and if it is from the 0 .. Heap.Length - 1 range.
        /// Wont return (throws an InterpreterException), if the next allocated space address is not byte aligned.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void CheckByteAlignedHereAddress(this IInterpreter interpreter)
        {
            CheckCharAlignedAddress(interpreter, interpreter.State.Heap.Top + 1);
        }
        
        
        /// <summary>
        /// Checks, if an address is cell aligned and if it is from the 0 .. Heap.Length - 1 range.
        /// Wont return (throws an InterpreterException), if the address is not char aligned.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="addr">An address.</param>
        public static void CheckCharAlignedAddress(this IInterpreter interpreter, int addr)
        {
            // -9 invalid memory address
            if (addr < 0 || addr > interpreter.State.Heap.Items.Length - Heap.CharSize) throw new InterpreterException(-9, $"The address {addr} is out of the <0 .. Heap.Length) range.");
            
            if (interpreter.State.Heap.IsCharAligned(addr) == false)
            {
                // -23 address alignment exception
                throw new InterpreterException(-23, $"The {addr} is not a char aligned address.");
            }
        }
        
        /// <summary>
        /// Checks, if the next allocated space address is cell aligned and if it is from the 0 .. Heap.Length - 1 range.
        /// Wont return (throws an InterpreterException), if the next allocated space address is not char aligned.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void CheckCharAlignedHereAddress(this IInterpreter interpreter)
        {
            CheckCharAlignedAddress(interpreter, interpreter.State.Heap.Top + 1);
        }
        
        /// <summary>
        /// Checks, if an address is cell aligned and if it is from the 0 .. Heap.Length - 1 range.
        /// Won't return (throws an InterpreterException), if the address is not cell aligned.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="addr">An address.</param>
        public static void CheckCellAlignedAddress(this IInterpreter interpreter, int addr)
        {
            // -9 invalid memory address
            if (addr < 0 || addr > interpreter.State.Heap.Items.Length - Heap.CellSize) throw new InterpreterException(-9, $"The address {addr} is out of the <0 .. Heap.Length) range.");
            
            if (interpreter.State.Heap.IsCellAligned(addr) == false)
            {
                // -23 address alignment exception
                throw new InterpreterException(-23, $"The {addr} is not a cell aligned address.");
            }
        }
        
        /// <summary>
        /// Checks, if the next allocated space address is cell aligned and if it is from the 0 .. Heap.Length - 1 range.
        /// Won't return (throws an InterpreterException), if the next allocated space address is not cell aligned.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        public static void CheckCellAlignedHereAddress(this IInterpreter interpreter)
        {
            CheckCellAlignedAddress(interpreter, interpreter.State.Heap.Top + 1);
        }

        /// <summary>
        /// Checks, if addresses defined by the start address and the count are from the 0 .. Heap.Length - 1 range.
        /// Won't return (throws an InterpreterException), if defined addresses are not from the heap address range.
        /// </summary>
        /// <param name="interpreter">An IInterpreter instance.</param>
        /// <param name="startAddress">A start address.</param>
        /// <param name="count">A number of bytes.</param>
        public static void CheckAddressesRange(this IInterpreter interpreter, int startAddress, int count)
        {
            // -9 invalid memory address
            if (startAddress < 0 || startAddress >= interpreter.State.Heap.Items.Length) throw new InterpreterException(-9, $"The address {startAddress} is out of the <0 .. Heap.Length) range.");
            
            // -21 unsupported operation
            if (count <= 0) throw new InterpreterException(-21, $"The count {count} is out of the (0 .. n) range."); 
            
            // -9 invalid memory address
            if (startAddress + count - 1 >= interpreter.State.Heap.Items.Length) throw new InterpreterException(-9, $"The address {startAddress} plus the count {count} is out of the <0 .. Heap.Length) range.");
        }
        
        #endregion
    }
}
