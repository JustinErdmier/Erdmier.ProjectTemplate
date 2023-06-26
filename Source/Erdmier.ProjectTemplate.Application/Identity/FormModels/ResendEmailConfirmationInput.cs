namespace Erdmier.ProjectTemplate.Application.Identity.FormModels;

public sealed class ResendEmailConfirmationInput
{
    [ Required(AllowEmptyStrings = false), EmailAddress,
      MaxLength(256, ErrorMessage = "The email address cannot be longer than 100 characters.") ]
    public string Email { get; init; } = null!;
}
