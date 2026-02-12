using System.Collections.Specialized;

namespace LuxonServer.Plugin;

public interface IHttpResponse
{
    HttpRequest Request { get; }
    int HttpCode { get; }
    string Reason { get; }
    byte[] Responsedata { get; }
    string ResponseText { get; }
    HttpRequestQueueResult Status { get; }
    int WebStatus { get; }
    ICallInfo CallInfo { get; }
    NameValueCollection Headers { get; }
}
