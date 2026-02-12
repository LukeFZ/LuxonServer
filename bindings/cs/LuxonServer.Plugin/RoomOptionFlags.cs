namespace LuxonServer.Plugin;

[Flags]
public enum RoomOptionFlags
{
    CheckUserOnJoin = 1 << 0,
    DeleteCacheOnLeave = 1 << 1,
    SuppressRoomEvents = 1 << 2,
    PublishUserId = 1 << 3,
    DeleteNullProps = 1 << 4,
    BroadcastPropsChangeToAll = 1 << 5,
    SuppressPlayerInfo = 1 << 6
}
