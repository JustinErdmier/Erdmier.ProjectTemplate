namespace Erdmier.ProjectTemplate.Application.Common.Models;

public record struct UiResponse(string? ResponseMessage = null, UiResponse.State ResponseState = UiResponse.State.Failed)
{
    public enum State { Failed, Succeeded }

    public bool HasMessage => ! string.IsNullOrWhiteSpace(ResponseMessage);

    public bool Succeeded => ResponseState.Equals(State.Succeeded);

    public bool Failed => ResponseState.Equals(State.Failed);

    public static UiResponse Success() => new (ResponseState: State.Succeeded);

    public static UiResponse Success(string message) => new (message, State.Succeeded);

    public static UiResponse Failure() => new ();

    public static UiResponse Failure(string message) => new (message);
}
