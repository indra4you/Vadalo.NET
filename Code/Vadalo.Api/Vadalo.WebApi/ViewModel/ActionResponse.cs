namespace Vadalo.Web.Api.ViewModel;

public sealed class ActionResponse<TData>(
    string status,
    string? message,
    TData data
)
{
    public string Status { get; private set; } = status;
    public string? Message { get; private set; } = message;
    public TData Data { get; private set; } = data;
}