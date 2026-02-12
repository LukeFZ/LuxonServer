namespace LuxonServer.Plugin;

public enum JoinModeConstants : byte
{
    JoinOnly = 0,
    CreateIfNotExists = 1,
    RejoinOrJoin = 2,
    RejoinOnly = 3
}
