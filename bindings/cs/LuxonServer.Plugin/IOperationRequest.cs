namespace LuxonServer.Plugin;

public interface IOperationRequest
{
    byte OperationCode { get; }
    Dictionary<byte, object> Parameters { get; }
    WebFlags WebFlags { get; set; }
}
