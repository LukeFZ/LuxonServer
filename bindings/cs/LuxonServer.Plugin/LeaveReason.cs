namespace LuxonServer.Plugin;

public enum LeaveReason : short
{
    ClientDisconnect = 0,
    ClientTimeoutDisconnect = 1,
    ManagedDisconnect = 2,
    ServerDisconnect = 3,
    TimeoutDisconnect = 4,
    ConnectTimeout = 5,

    SwitchRoom = 100,
    LeaveRequest = 101,
    PlayerTtlTimedOut = 102,
    PeerLastTouchTimedout = 103,
    PluginRequest = 104,
    PluginFailedJoin = 105,

    InternalServerError = -1,
    OperationInvalid = -2,
    OperationDenied = -3,
    SendBufferFull = -11,
    GameFull = 32765,
    GameClosed = 32764,
    PluginReportedError = 32752,
    JoinFailedPeerAlreadyJoined = 32750,
    JoinFailedFoundInactiveJoiner = 32749,
    JoinFailedWithRejoinerNotFound = 32748,
    JoinFailedFoundExcludedUserId = 32747,
    JoinFailedFoundActiveJoiner = 32746,
    HttpLimitReached = 32745,
    OperationLimitReached = 32743,
    SlotError = 32742,
    EventCacheExceeded = 32739,
    ConnectionSwitch = 32735,
    ActorRemoved = 32734,
}
