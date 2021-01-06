/* EFrt - (C) 2020 Premysl Fara  */

namespace EFrt.Core.Values
{
    using System.Runtime.InteropServices;


    [StructLayout(LayoutKind.Explicit)]
    public struct UnsignedSingleCellIntegerValue
    {
        [FieldOffset(0)] public uint U;
        [FieldOffset(0)] public int V;
    }
}
