namespace LuxonServer.Plugin;

public class SerializableGameState
{
    public int ActorCounter { get; set; }
    public List<SerializableActor> ActorList { get; set; } = [];
    public Dictionary<string, object> Binary { get; set; } = [];
    public bool CheckUserOnJoin { get; set; }
    public Dictionary<string, object> CustomProperties { get; set; } = [];
    public bool DeleteCacheOnLeave { get; set; }
    public int EmptyRoomTTL { get; set; }
    public bool IsOpen { get; set; }
    public bool IsVisible { get; set; }
    public string LobbyId { get; set; } = "";
    public int LobbyType { get; set; }
    public List<string> LobbyProperties { get; set; } = [];
    public byte MaxPlayers { get; set; }
    public int PlayerTTL { get; set; }
    public bool SuppressRoomEvents { get; set; }
    public int Slice { get; set; }
    public Dictionary<string, object> DebugInfo { get; set; } = [];
    public List<ExcludedActorInfo> ExcludedActors { get; set; } = [];
    public bool PublishUserId { get; set; }
    public List<string> ExpectedUsers { get; set; } = [];
    public int RoomFlags { get; set; }
    
}
