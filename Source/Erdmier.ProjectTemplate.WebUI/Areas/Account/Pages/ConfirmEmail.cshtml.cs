using System.Text;

using Microsoft.AspNetCore.WebUtilities;

namespace Erdmier.ProjectTemplate.WebUI.Areas.Account.Pages;

public class ConfirmEmail : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ConfirmEmail(UserManager<ApplicationUser> userManager) => _userManager = userManager;

    [ ViewData ]
    public string Title => "Confirm Email";

    public UiResponse UiResponse { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid userId, string? token)
    {
        if (userId.Equals(Guid.Empty) || string.IsNullOrWhiteSpace(token))
        {
            UiResponse =
                UiResponse.Failure("We were unable to confirm your email address. Please try resending the confirmation email by clicking the button below; if the problem persists, please contact support.");

            return Page();
        }

        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            Log.Error("Failed to find user by ID with the given ID {UserId} when attempting to confirm their email", userId);

            UiResponse =
                UiResponse.Failure("We were unable to confirm your email address. Please try resending the confirmation email by clicking the button below; if the problem persists, please contact support.");

            return Page();
        }

        token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

        IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
        {
            Log.Information("Successfully confirmed email for user {UserName} / {UserEmail}", user.UserName, user.Email);

            UiResponse = UiResponse.Success("Your email address has been confirmed. You may now log in.");

            return Page();
        }

        Log.Error("Failed to confirm email for user {UserName} / {UserEmail}", user.UserName, user.Email);

        UiResponse =
            UiResponse.Failure("We were unable to confirm your email address. Please try resending the confirmation email by clicking the button below; if the problem persists, please contact support.");

        return Page();
    }
}
