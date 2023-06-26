using System.Text;

using Microsoft.AspNetCore.WebUtilities;

namespace Erdmier.ProjectTemplate.WebUI.Areas.Account.Pages;

public class ResetPassword : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ResetPassword(UserManager<ApplicationUser> userManager) => _userManager = userManager;

    [ ViewData ]
    public string Title => "Reset Password";

    [ BindProperty ]
    public ResetPasswordInput Input { get; set; } = new ();

    public UiResponse UiResponse { get; set; }

    public string? InitialTokenValue { get; set; }

    public IActionResult OnGet(string? token)
    {
        if (token is null)
        {
            UiResponse =
                UiResponse.Failure("We were unable to process your request. Please try again; if the problem persists, please contact support.");

            return Page();
        }

        InitialTokenValue = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (! ModelState.IsValid)
            return Page();

        ApplicationUser? user = await _userManager.FindByEmailAsync(Input.Email);

        if (user is null)
        {
            Log.Error("Failed to find user by email with the given email {UserEmail} when attempting to reset their password", Input.Email);

            // Don't reveal that the user doesn't exist.
            UiResponse = UiResponse.Success("Successfully reset your password!");

            return Page();
        }

        IdentityResult resetPasswordResult = await _userManager.ResetPasswordAsync(user, Input.Token, Input.Password);

        if (resetPasswordResult.Succeeded)
        {
            Log.Information("Successfully reset password for user {UserName} / {UserEmail}", user.UserName, user.Email);

            UiResponse = UiResponse.Success("Successfully reset your password!");

            return Page();
        }

        Log.Error("Failed to reset password for user {UserName} / {UserEmail}", user.UserName, user.Email);

        resetPasswordResult.Errors.ToList().ForEach(error => ModelState.AddModelError(string.Empty, error.Description));

        UiResponse =
            UiResponse.Failure("We were unable to process your request. Please try again; if the problem persists, please contact support.");

        return Page();
    }
}
