namespace Erdmier.ProjectTemplate.Application.Identity.FormModels;

/// <summary> A form model for the Log In page; gets the user's username/email and password. </summary>
public sealed class LogInInput
{
    /// <summary> Gets or sets the user's username or email. </summary>
    [ Required(AllowEmptyStrings = false) ]
    public string UserName { get; init; } = null!;

    /// <summary> Gets or sets the user's password. </summary>
    [ Required(AllowEmptyStrings = false), DataType(DataType.Password) ]
    public string Password { get; init; } = null!;
}
