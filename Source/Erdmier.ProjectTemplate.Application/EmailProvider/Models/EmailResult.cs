namespace Erdmier.ProjectTemplate.Application.EmailProvider.Models;

public record EmailResult(ServiceResult.State Result = ServiceResult.State.Failed, string? Message = null)
    : ServiceResult(Result, Message)
{
    public new static EmailResult Success() => new (State.Succeeded);

    public new static EmailResult Success(string message) => new (State.Succeeded, message);

    public new static EmailResult Failure() => new ();

    public new static EmailResult Failure(string message) => new (State.Failed, message);
}
