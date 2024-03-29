﻿/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Stacks
{
    /// <summary>
    /// Defines a stack.
    /// </summary>
    /// <typeparam name="T">A type of a stack item.</typeparam>
    public interface IGenericStack<T> : IStack
    {
        /// <summary>
        /// Items stored in this stack.
        /// </summary>
        T[] Items { get; }

        /// <summary>
        /// The index of the top item.
        /// </summary>
        int Top { get; set; }


        /// <summary>
        /// Removes all items from this stack, replacing them with a default value.
        /// </summary>
        /// <param name="defaultValue">A value.</param>
        void Init(T defaultValue);
        
        /// <summary>
        /// Removes N items from the top of this stack.
        /// </summary>
        /// <param name="n">The number of items to remove.</param>
        void Drop(int n = 1);

        /// <summary>
        /// Duplicates the topmost item on the stack.
        /// (a - a a)
        /// </summary>
        void Dup();

        /// <summary>
        /// Gets an item on the stack.
        /// </summary>
        /// <param name="index">A position of the item in the stack. 0 = the top of the stack.</param>
        /// <returns></returns>
        T Pick(int index);
               
        /// <summary>
        /// Returns a value from the top of the stack.
        /// </summary>
        /// <returns>A value from the top of the stack.</returns>
        T Peek();

        /// <summary>
        /// Removes a value from the top of the stack and returns it.
        /// </summary>
        /// <returns>A value from the top of the stack.</returns>
        T Pop();

        /// <summary>
        /// Pushes a value on the top of the stack.
        /// </summary>
        /// <param name="a">A value.</param>
        void Push(T a);

        /// <summary>
        /// (a b -- a b a)
        /// </summary>
        void Over();

        /// <summary>
        /// (a b c -- b c a)
        /// </summary>
        void Rot();

        /// <summary>
        /// Rotates indexth item to the top.
        /// </summary>
        /// <param name="index">A stack item index, where 0 = stack top, 1 = first below top, etc.</param>
        void Roll(int index);

        /// <summary>
        /// Swaps the two top most values on the stack.
        /// </summary>
        void Swap();
    }
}