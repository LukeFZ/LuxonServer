namespace LuxonServer.Plugin;

public enum HttpRequestQueueResult : byte
{
    Success = 0,
    RequestTimeout = 1,
    QueueTimeout = 2,
    Offline = 3,
    QueueFull = 4,
    Error = 5
}
