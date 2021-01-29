/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core
{
    /// <summary>
    /// A stack cell value without a value type check or conversions.
    /// </summary>
    public class StackCellValue : IStackCellValue
    {
        public StackCellValueType ValueType { get; private set; }

        public bool BooleanValue
        {
            get { return _integerValue != 0; }

            set
            {
                ValueType = StackCellValueType.Integer;
                _integerValue = value ? -1 : 0;
            }
        }

        public int IntegerValue
        {
            get { return (int)_integerValue; }

            set
            {
                ValueType = StackCellValueType.Integer;
                _integerValue = value;
            }
        }

        public long DoubleIntegerValue
        {
            get { return _integerValue; }

            set
            {
                ValueType = StackCellValueType.DoubleInteger;
                _integerValue = value;
            }
        }

        public double FloatingPointValue
        {
            get { return _floatingPointValue; }

            set
            {
                ValueType = StackCellValueType.FloatingPoint;
                _floatingPointValue = value;
            }
        }

        public string StringValue
        {
            get { return (_objectValue == null) ? string.Empty : _objectValue.ToString(); }

            set
            {
                ValueType = StackCellValueType.Object;
                _objectValue = value ?? string.Empty;
            }
        }

        public object ObjectValue
        {
            get { return _objectValue; }

            set
            {
                ValueType = StackCellValueType.Object;
                _objectValue = value;
            }
        }


        public StackCellValue()
        {
            IntegerValue = 0;
        }


        private long _integerValue;
        private double _floatingPointValue;
        private object _objectValue;
    }
}
