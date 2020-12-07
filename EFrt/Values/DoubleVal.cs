/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Values
{
    using System.Runtime.InteropServices;


    [StructLayout(LayoutKind.Explicit)]
    public struct DoubleVal
    {
        [FieldOffset(0)] public double D;
        [FieldOffset(0)] public int A;
        [FieldOffset(4)] public int B;
    }
}
