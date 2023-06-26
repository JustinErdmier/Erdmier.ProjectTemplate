using Erdmier.ProjectTemplate.WebUI.Common.Utilities;

namespace Erdmier.ProjectTemplate.WebUI.Areas.Account.Pages.Manage;

public class Profile : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public Profile(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager   = userManager;
        _signInManager = signInManager;
    }

    [ ViewData ]
    public string Title => "Profile";

    [ ViewData ]
    public string ActivePage => AccountManagePagesNavUtil.Profile;

    [ TempData ]
    public string StatusMessage { get; set; } = string.Empty;

    [ BindProperty ]
    public UpdateProfileInput Input { get; set; } = new ();

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

    public async Task<IActionResult> OnPostAsync()
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

        // If the username has changed, check if the new username is already in use.
        string? username = await _userManager.GetUserNameAsync(user);

        if (! string.Equals(Input.UserName, username, StringComparison.OrdinalIgnoreCase))
        {
            ApplicationUser? existingUser = await _userManager.FindByNameAsync(Input.UserName);

            if (existingUser is not null)
            {
                StatusMessage = "🤔 Username is already in use.";

                return RedirectToPage();
            }

            // If the username is not in use, update the username.
            IdentityResult setUserNameResult = await _userManager.SetUserNameAsync(user, Input.UserName);

            if (! setUserNameResult.Succeeded)
            {
                StatusMessage = "🤔 Unexpected error when trying to set username.";

                return RedirectToPage();
            }
        }

        // If the phone number has changed, update the phone number.
        string? phoneNumber = await _userManager.GetPhoneNumberAsync(user);

        if (! string.Equals(Input.PhoneNumber, phoneNumber, StringComparison.OrdinalIgnoreCase))
        {
            IdentityResult setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);

            if (! setPhoneResult.Succeeded)
            {
                StatusMessage = "🤔 Unexpected error when trying to set phone number.";

                return RedirectToPage();
            }
        }

        // Refresh the sign-in cookie to reflect any changes to the user.
        await _signInManager.RefreshSignInAsync(user);

        StatusMessage = "🎉 Your profile has been updated";

        return RedirectToPage();
    }

    private async Task LoadUser(ApplicationUser user) =>
        Input = new UpdateProfileInput
        {
            UserName    = (await _userManager.GetUserNameAsync(user))!,
            PhoneNumber = await _userManager.GetPhoneNumberAsync(user)
        };
}
