using System.Collections;

namespace LuxonServer.Plugin;

public interface ISetPropertiesRequest : IOperationRequest
{
    int ActorNumber { get; set; }
    bool Broadcast { get; set; }
    bool HttpForward { get; set; }
    Hashtable Properties { get; set; }
    Hashtable ExpectedValues { get; set; }
}
