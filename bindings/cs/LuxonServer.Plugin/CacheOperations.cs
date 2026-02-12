namespace LuxonServer.Plugin;

public enum CacheOperations : byte
{
    DoNotCache = 0,
    MergeCache = 1,
    ReplaceCache = 2,
    RemoveCache = 3,
    AddToRoomCache = 4,
    AddToRoomCacheGlobal = 5,
    RemoveFromRoomCache = 6,
    RemoveFromCacheForActorsLeft = 7,
    SliceIncreateIndex = 10,
    SliceSetIndex = 11,
    SlicePurgeIndex = 12,
    SlicePurgeUpToIndex = 13
}
