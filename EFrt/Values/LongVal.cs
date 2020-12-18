/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Values
{
    using System.Runtime.InteropServices;


    [StructLayout(LayoutKind.Explicit)]
    public struct LongVal
    {
        [FieldOffset(0)] public long D;
        [FieldOffset(0)] public int A;
        [FieldOffset(4)] public int B;
    }
}
