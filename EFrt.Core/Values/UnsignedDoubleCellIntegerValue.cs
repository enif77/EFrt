/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Values
{
    using System.Runtime.InteropServices;


    [StructLayout(LayoutKind.Explicit)]
    public struct UnsignedDoubleCellIntegerValue
    {
        [FieldOffset(0)] public ulong UD;
        [FieldOffset(0)] public int A;
        [FieldOffset(4)] public int B;
    }
}
