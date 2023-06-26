namespace Erdmier.ProjectTemplate.Application.Identity.FormModels;

public sealed class NewEmailInput
{
    [ Required(AllowEmptyStrings = false), EmailAddress,
      MaxLength(256, ErrorMessage = "The email address cannot be longer than 100 characters.") ]
    public string NewEmail { get; init; } = null!;
}
