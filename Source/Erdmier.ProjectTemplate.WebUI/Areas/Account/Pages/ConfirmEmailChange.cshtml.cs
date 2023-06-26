using System.Text;

using Microsoft.AspNetCore.WebUtilities;

namespace Erdmier.ProjectTemplate.WebUI.Areas.Account.Pages;

public class ConfirmEmailChange : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public ConfirmEmailChange(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager   = userManager;
    }

    [ ViewData ]
    public string Title => "Confirm Email Change";

    public UiResponse UiResponse { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid userId, string? email, string? token)
    {
        if (userId.Equals(Guid.Empty) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
        {
            UiResponse =
                UiResponse.Failure("We were unable to confirm your email address. Please try resending the confirmation email by clicking the button below; if the problem persists, please contact support.");

            return Page();
        }

        ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            Log.Error("Failed to find user by ID with the given ID {UserId} when attempting to confirm their email change", userId);

            UiResponse =
                UiResponse.Failure("We were unable to confirm your email address. Please try resending the confirmation email by clicking the button below; if the problem persists, please contact support.");

            return Page();
        }

        token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

        IdentityResult changedEmailResult = await _userManager.ChangeEmailAsync(user, email, token);

        if (! changedEmailResult.Succeeded)
        {
            Log.Error("Failed to confirm email for user {UserName} / {UserEmail} due to one or more errors returned by the IdentityResult object",
                      user.UserName, user.Email);

            UiResponse =
                UiResponse.Failure("We were unable to confirm your email address. Please try resending the confirmation email by clicking the button below; if the problem persists, please contact support.");

            changedEmailResult.Errors.ToList().ForEach(e => ModelState.AddModelError(string.Empty, e.Description));

            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);

        Log.Information("Successfully confirmed email change for user {UserName} / {UserEmail}", user.UserName, user.Email);

        UiResponse = UiResponse.Success("Your email address has been confirmed. You may now log in.");

        return Page();
    }
}
