using System;
using System.Collections;
using System.Collections.Generic;

namespace LuxonServer.Plugin;

public interface IPluginHost
{
    Dictionary<string, object> Environment { get; }
    IList<IActor> GameActors { get; }
    IList<IActor> GameActorsActive { get; }
    IList<IActor> GameActorsInactive { get; }
    string GameId { get; }
    Hashtable GameProperties { get; }
    Dictionary<string, object> CustomGameProperties { get; }
    int MasterClientId { get; }
    bool IsSuspended { get; }
    bool IsSyspended { get; } // lmao x2

    void BroadcastEvent(IList<int> receiverActors, int senderActor, byte evCode, Dictionary<byte, object> data,
        CacheOperations cacheOp, SendParameters sendParameters = new());

    void BroadcastEvent(byte target, int senderActor, byte targetGroup, byte evCode, Dictionary<byte, object> data,
        CacheOperations cacheOp,
        SendParameters sendParameters = new());

    void BroadcastErrorInfoEvent(string message, SendParameters sendParameters = new());
    void BroadcastErrorInfoEvent(string message, ICallInfo info, SendParameters sendParameters = new());

    object CreateOneTimeTimer(Action callback, int dueTimeMs);
    object CreateOneTimeTimer(ICallInfo info, Action callback, int dueTimeMs);
    object CreateTimer(Action callback, int dueTimeMs, int intervalMs);

    SerializableGameState GetSerializableGameState();

    void HttpRequest(HttpRequest request);
    void HttpRequest(HttpRequest request, ICallInfo info);

    void LogDebug(object? message);
    void LogError(object? message);
    void LogFatal(object? message);
    void LogInfo(object? message);
    void LogWarning(object? message);

    bool SetGameState(SerializableGameState state);
    bool SetProperties(int actorNr, Hashtable properties, Hashtable expected, bool broadcast);

    void StopTimer(object timer);
    bool RemoveActor(int actorNr, string? reasonDetail);
    bool RemoveActor(int actorNr, RemoveActorReason reason, string? reasonDetail);
    
    bool TryRegisterType(Type type, byte typeCode, Func<object, byte[]> serializeFunction, Func<byte[], object> deserializeFunction);
    
    EnvironmentVersion GetEnvironmentVersion();

    IPluginLogger CreateLogger(string name);

    void Enqueue(Action action);
    bool ExecuteCacheOperation(CacheOp operation, out string errorMessage);
    IPluginFiber GetRoomFiber();
}
