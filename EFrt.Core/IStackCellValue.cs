/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core
{
    /// <summary>
    /// Defines a stack cell value type.
    /// </summary>
    public enum StackCellValueType
    {
        /// <summary>
        /// A 32 bit integer value.
        /// </summary>
        Integer,

        /// <summary>
        /// A 64 bit integer value.
        /// </summary>
        DoubleInteger,

        /// <summary>
        /// A floating poitn value.
        /// </summary>
        FloatingPoint,

        /// <summary>
        /// An object reference value.
        /// </summary>
        Object
    };


    /// <summary>
    /// Represents a stack cell value.
    /// </summary>
    public interface IStackCellValue
    {
        /// <summary>
        /// A boolean value. It is an alias for the IntegerValue, that converts boolean values to -1 and 0 and integer values to true and false.
        /// </summary>
        bool BooleanValue { get; set; }

        /// <summary>
        /// A 64 bit integer value.
        /// </summary>
        long DoubleIntegerValue { get; set; }

        /// <summary>
        /// A floating point value.
        /// </summary>
        double FloatingPointValue { get; set; }

        /// <summary>
        /// A 32 bit integer value.
        /// </summary>
        int IntegerValue { get; set; }

        /// <summary>
        /// A general purpose object reference.
        /// </summary>
        object ObjectValue { get; set; }

        /// <summary>
        /// A string value. An alias for the ObjectValue. Returns ObjectValue.ToString(). Sets the Object value to a string, converting a null to string.Empty.
        /// </summary>
        string StringValue { get; set; }

        /// <summary>
        /// A type of value stored in this stack cell.
        /// </summary>
        StackCellValueType ValueType { get; }
    }
}
