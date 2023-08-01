namespace Erdmier.ProjectTemplate.Application.Identity.FormModels;

public sealed class NewPasswordInput
{
    [ Required(AllowEmptyStrings = false), DataType(DataType.Password) ]
    public string OldPassword { get; init; } = null!;

    [ Required(AllowEmptyStrings = false), DataType(DataType.Password),
      StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8) ]
    public string NewPassword { get; init; } = null!;

    [ Required(AllowEmptyStrings = false), DataType(DataType.Password),
      Compare(nameof(NewPassword), ErrorMessage = "The new password and confirmation password do not match.") ]
    public string ConfirmPassword { get; init; } = null!;
}
