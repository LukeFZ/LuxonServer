using System.Runtime.InteropServices;

namespace LuxonServer;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct ObjectManagerInterface
{
    public delegate* unmanaged[Cdecl]<nint, void> DestroyObject;
    public delegate* unmanaged[Cdecl]<void*, void> FreeAllocation;
}