using System.Net;

namespace Ciandt.FlowTools.FlowReviewer.Flow;

public sealed record FlowError(
    HttpStatusCode StatusCode,
    string Message,
    string? Content = null)
{
    public string FullMessage => string.IsNullOrEmpty(Content) ? Message : $"{Message}: {Content}";

    public override string ToString() => $"FlowError {{ StatusCode = {(int)StatusCode}, Message = \"{FullMessage}\" }}";
}
