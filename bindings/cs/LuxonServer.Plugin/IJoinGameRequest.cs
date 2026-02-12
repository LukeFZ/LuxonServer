using System.Collections;

namespace LuxonServer.Plugin;

public interface IJoinGameRequest : IOperationRequest
{
    int ActorNr { get; set; }
    Hashtable ActorProperties { get; set; }
    bool BroadcastActorProperties { get; set; }
    bool DeleteCacheOnLeave { get; set; }
    int EmptyRoomLiveTime { get; set; }
    string GameId { get; set; }
    Hashtable GameProperties { get; set; }
    bool SuppressRoomEvents { get; set; }
    bool CreateIfNotExists { get; }
    string LobbyName { get; }
    byte LobbyType { get; }
    JoinModeConstants JoinMode { get; }
    RoomOptionFlags RoomFlags { get; set; }
    int PlayerTTL { get; set; }
}
