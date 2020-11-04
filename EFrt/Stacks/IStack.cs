/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Stacks
{
    /// <summary>
    /// Defines a stack.
    /// </summary>
    /// <typeparam name="T">A type of a stack item.</typeparam>
    public interface IStack<T>
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
        /// The number of items on this stack.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Is true, when no items are in this stack.
        /// </summary>
        bool IsEmpty { get; }


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
        /// <param name="index">A position of the item in the stack. 0 = the bottom of the stack.</param>
        /// <returns></returns>
        T Get(int index);

        
        
        void Over();
        T Peek();
        T Pop();
        void Push(T a);
        void Rot();
        void Swap();
    }
}