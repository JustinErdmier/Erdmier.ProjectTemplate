namespace Erdmier.ProjectTemplate.Application.Identity.FormModels;

public class UpdateProfileInput
{
    [ Required(AllowEmptyStrings = false, ErrorMessage = "A username is required."), Display(Name = "UserName"),
      MaxLength(100, ErrorMessage = "The username cannot be longer than 100 characters.") ]
    public string UserName { get; init; } = null!;

    [ Display(Name = "Phone Number"), Phone ]
    public string? PhoneNumber { get; init; }
}
