/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt
{
    using System.Runtime.InteropServices;


    [StructLayout(LayoutKind.Explicit)]
    public struct EfrtValue
    {
        [FieldOffset(0)] public float Float;
       
        [FieldOffset(0)] public int Int;
        [FieldOffset(0)] public uint UInt;

        [FieldOffset(0)] public short Short;
        [FieldOffset(2)] public short Short2;

        [FieldOffset(0)] public ushort UShort;
        [FieldOffset(2)] public ushort UShort2;
        
        [FieldOffset(0)] public byte Byte;
        [FieldOffset(1)] public byte Byte2;
        [FieldOffset(2)] public byte Byte3;
        [FieldOffset(3)] public byte Byte4;


        public EfrtValue(float a) : this()
        {
            Float = a;
        }

        
        public EfrtValue(int a) : this()
        {
            Int = a;
        }

        public EfrtValue(uint a) : this()
        {
            UInt = a;
        }


        public EfrtValue(short a) : this()
        {
            Short = a;
        }

        public EfrtValue(short a, short b) : this()
        {
            Short = a;
            Short2 = b;
        }


        public EfrtValue(ushort a) : this()
        {
            UShort = a;
        }

        public EfrtValue(ushort a, ushort b) : this()
        {
            UShort = a;
            UShort2 = b;
        }


        public EfrtValue(byte a) : this()
        {
            Byte = a;
        }

        public EfrtValue(byte a, byte b) : this()
        {
            Byte = a;
            Byte2 = b;
        }

        public EfrtValue(byte a, byte b, byte c) : this()
        {
            Byte = a;
            Byte2 = b;
            Byte3 = c;
        }

        public EfrtValue(byte a, byte b, byte c, byte d) : this()
        {
            Byte = a;
            Byte2 = b;
            Byte3 = c;
            Byte4 = d;
        }
    }
}

/*

https://csharppedia.com/en/tutorial/5626/how-to-use-csharp-structs-to-create-a-union-type---similar-to-c-unions-

*/