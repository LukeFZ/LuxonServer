namespace LuxonServer.Interop;

[Flags]
internal enum MessageFlags : byte
{
    IsEncrypted = 1 << 0
}