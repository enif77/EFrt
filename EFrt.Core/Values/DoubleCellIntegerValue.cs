/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Values
{
    using System.Runtime.InteropServices;


    [StructLayout(LayoutKind.Explicit)]
    public struct DoubleCellIntegerValue
    {
        [FieldOffset(0)] public long D;
        [FieldOffset(0)] public int A;
        [FieldOffset(4)] public int B;
    }
}
