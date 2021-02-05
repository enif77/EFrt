/* EFrt - (C) 2021 Premysl Fara  */

namespace EFrt.Core.Values
{
    using System.Runtime.InteropServices;


    [StructLayout(LayoutKind.Explicit)]
    public struct SingleCellFloatingPointValue
    {
        [FieldOffset(0)] public float F;
        [FieldOffset(0)] public int A;
    }
}
