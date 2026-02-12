using System;
using System.Collections.Generic;
using System.Net;

namespace LuxonServer.Plugin;

public delegate void HttpRequestCallback();

public struct HttpRequest
{
    public string Accept;
    public HttpRequestCallback Callback;
    public string ContentType;
    public MemoryStream DataStream;
    public IDictionary<HttpRequestHeader, string> Headers;
    public IDictionary<string, string> CustomHeaders;
    public string Method;
    public string Url;
    public object? UserState;
    public bool Async;
}
