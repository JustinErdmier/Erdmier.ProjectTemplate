using Erdmier.ProjectTemplate.WebUI.Common.Utilities;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Erdmier.ProjectTemplate.WebUI.Areas.Account.Pages.Manage;

public class Password : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    /// <inheritdoc />
    public Password(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager   = userManager;
    }

    [ViewData]
    public string Title => "Password";

    [ViewData]
    public string ActivePage => AccountManagePagesNavUtil.Password;

    [TempData]
    public string StatusMessage { get; set; } = string.Empty;

    [BindProperty]
    public NewPasswordInput Input { get; set; } = new ();

    public async Task<IActionResult> OnGetAsync()
    {
        ApplicationUser? user = await _userManager.GetUserAsync(User);

        if (user is not null)
            return Page();

        Log.Error("Unable to load user with ID '{UserId}'", _userManager.GetUserId(User));

        return NotFound("Unexpected error; please contact support.");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (! ModelState.IsValid)
            return Page();

        ApplicationUser? user = await _userManager.GetUserAsync(User);

        if (user is null)
        {
            Log.Error("Unable to load user with ID '{UserId}'", _userManager.GetUserId(User));

            return NotFound("Unexpected error; please contact support.");
        }

        IdentityResult? changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);

        if (! changePasswordResult.Succeeded)
        {
            foreach (IdentityError error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            Log.Error("Unable to change password for user with ID '{UserId}'", _userManager.GetUserId(User));

            StatusMessage = "Unexpected error; please contact support.";

            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);

        Log.Information("User with ID '{UserId}' changed their password successfully", _userManager.GetUserId(User));

        StatusMessage = "Your password has been changed.";

        return RedirectToPage();
    }
}
