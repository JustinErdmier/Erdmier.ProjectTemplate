namespace Erdmier.ProjectTemplate.Application.Identity.FormModels;

/// <summary> An input model for the register page. </summary>
public sealed class RegisterInput
{
    /// <summary> Gets or initializes the user's username. </summary>
    [ Required(AllowEmptyStrings = false, ErrorMessage = "A username is required."), Display(Name = "UserName"),
      MaxLength(100, ErrorMessage = "The username cannot be longer than 100 characters.") ]
    public string UserName { get; init; } = null!;

    /// <summary> Gets or initializes the user's email address. </summary>
    [ Required(AllowEmptyStrings = false, ErrorMessage = "An email address is required."), Display(Name = "Email"),
      MaxLength(100, ErrorMessage = "The email address cannot be longer than 100 characters."),
      EmailAddress(ErrorMessage = "The email address is not valid.") ]
    public string Email { get; init; } = null!;

    /// <summary> Gets or initializes the user's password. </summary>
    [ Required(AllowEmptyStrings = false), DataType(DataType.Password), Display(Name = "Password"),
      StringLength(100, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.") ]
    public string Password { get; init; } = null!;

    /// <summary> Gets or initializes the user's password confirmation. </summary>
    [ Required(AllowEmptyStrings = false), DataType(DataType.Password), Display(Name = "Confirm Password"),
      Compare(nameof(Password), ErrorMessage = "The passwords do not match.") ]
    public string ConfirmPassword { get; init; } = null!;
}
