using System.Text;

using Erdmier.ProjectTemplate.WebUI.Common.Utilities;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;

namespace Erdmier.ProjectTemplate.WebUI.Areas.Account.Pages.Manage;

public class Email : PageModel
{
    private readonly IEmailSender _emailSender;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public Email(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
    {
        _userManager   = userManager;
        _signInManager = signInManager;
        _emailSender   = emailSender;
    }

    [ ViewData ]
    public string Title => "Email";

    [ ViewData ]
    public string ActivePage => AccountManagePagesNavUtil.Email;

    [ TempData ]
    public string StatusMessage { get; set; } = string.Empty;

    [ BindProperty ]
    public NewEmailInput Input { get; set; } = new ();

    private string? CurrentEmail { get; set; }

    private bool IsEmailConfirmed { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        ApplicationUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            Log.Error("Unable to load user with ID '{UserId}'", _userManager.GetUserId(User));

            return NotFound("Unexpected error; please contact support.");
        }

        await LoadUser(user);

        return Page();
    }

    public async Task<IActionResult> OnPostChangeEmailAsync()
    {
        ApplicationUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            Log.Error("Unable to load user with ID '{UserId}'", _userManager.GetUserId(User));

            return NotFound();
        }

        if (! ModelState.IsValid)
        {
            await LoadUser(user);

            return Page();
        }

        // Check if email has changed.
        string? currentEmail = await _userManager.GetEmailAsync(user);

        if (string.Equals(Input.NewEmail, currentEmail, StringComparison.OrdinalIgnoreCase))
        {
            StatusMessage = "🤔 Looks like your email hasn't changed.";

            return RedirectToPage();
        }

        string userId = await _userManager.GetUserIdAsync(user);
        string token  = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        StatusMessage = "📧 Confirmation email sent. Please check your email.";

        // TODO: Replace with actual call to email sender service when implemented.
        return RedirectToPage("ConfirmEmailChange", new { userId, email = Input.NewEmail, token });
    }

    public async Task<IActionResult> OnPostSendVerificationEmailAsync()
    {
        ApplicationUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            Log.Error("Unable to load user with ID '{UserId}'", _userManager.GetUserId(User));

            return NotFound();
        }

        if (! ModelState.IsValid)
        {
            await LoadUser(user);

            return Page();
        }

        string userId = await _userManager.GetUserIdAsync(user);
        string token  = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        StatusMessage = "📧 Verification email sent. Please check your email.";

        // TODO: Replace with actual call to email sender service when implemented.
        return RedirectToPage("ConfirmEmail", new { userId, email = Input.NewEmail, token });
    }

    private async Task LoadUser(ApplicationUser user)
    {
        string? currentEmail = await _userManager.GetEmailAsync(user);

        CurrentEmail = currentEmail;

        IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

        if (currentEmail is not null)
            Input = new NewEmailInput
            {
                NewEmail = currentEmail
            };
    }
}
