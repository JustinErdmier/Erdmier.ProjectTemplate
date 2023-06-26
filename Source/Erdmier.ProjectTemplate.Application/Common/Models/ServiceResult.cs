namespace Erdmier.ProjectTemplate.Application.Common.Models;

/// <summary> A standardized result model for various application services. </summary>
/// <remarks> A service may utilize more detailed result models, but this is the standard model for all services. </remarks>
/// <param name="Result">
///     Indicates whether the operation <see cref="State.Succeeded" /> or otherwise
///     <see cref="State.Failed" />.
/// </param>
/// <param name="Message">
///     An optional <see langword="string" /> which will provide details as to why the specific value
///     for <paramref name="Result" /> was chosen.
/// </param>
public record ServiceResult(ServiceResult.State Result = ServiceResult.State.Failed, string? Message = null)
{
    public enum State { Failed, Succeeded }

    public bool HasMessage => ! string.IsNullOrWhiteSpace(Message);

    public bool Failed => Result.Equals(State.Failed);

    public bool Succeeded => Result.Equals(State.Succeeded);

    public static ServiceResult Success() => new (State.Succeeded);

    public static ServiceResult Success(string message) => new (State.Succeeded, message);

    public static ServiceResult Failure() => new ();

    public static ServiceResult Failure(string message) => new (State.Failed, message);
}
