using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Erdmier.ProjectTemplate.WebUI.Areas.Account.Pages;

/// <summary> The model for the Log In page. </summary>
public class LogIn : PageModel
{
    /// <summary> The <see cref="SignInManager{TUser}" />. </summary>
    private readonly SignInManager<ApplicationUser> _signInManager;

    /// <summary> The <see cref="UserManager{TUser}" />. </summary>
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary> Instantiates a new <see cref="LogIn" /> model for the Log In page. </summary>
    /// <param name="signInManager"> The <see cref="SignInManager{TUser}" />. </param>
    /// <param name="userManager"> The <see cref="UserManager{TUser}" />. </param>
    public LogIn(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) =>
        (_signInManager, _userManager) = (signInManager, userManager);

    /// <summary> Sets the title of the page. </summary>
    [ ViewData ]
    public string Title => "Log In";

    /// <summary> Gets or sets the input model for the Log In page. </summary>
    [ BindProperty ]
    public LogInInput Input { get; set; } = new ();

    /// <summary> Gets or sets the return URL for when the user was redirected to the Log In page. </summary>
    public string? ReturnUrl { get; set; }

    /// <summary> The <see cref="UiResponse" /> for the Log In page. </summary>
    public UiResponse UiResponse { get; set; }

    /// <summary>
    ///     <c> GET </c> <b> ~/Account/LogIn </b>
    /// </summary>
    /// <param name="returnUrl"> The return URL for when the user was redirected to the Log In page. </param>
    public async Task OnGetAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        // clear the existing external cookie to ensure a clean login process.
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ReturnUrl = returnUrl;
    }

    /// <summary>
    ///     <c> POST </c> <b> ~/Account/LogIn </b>
    /// </summary>
    /// <param name="returnUrl"> The return URL for when the user was redirected to the Log In page. </param>
    /// <returns> An <see cref="IActionResult" />. </returns>
    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (! ModelState.IsValid)
            return Page();

        ApplicationUser? user = await FindUserAsync(Input.UserName);

        if (user is null)
        {
            // Do not reveal that the user does not exist or is not confirmed; this is a security best practice.

            UiResponse = UiResponse.Failure("Invalid login attempt.");

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return Page();
        }

        // This doesn't count login failures towards account lockout.
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true.
        SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, Input.Password, false, false);

        if (signInResult.Succeeded)
        {
            Log.Information("User {UserName} logged in", user.UserName);

            return LocalRedirect(returnUrl);
        }

        if (signInResult.IsLockedOut)
        {
            Log.Warning("The account for user {UserName} is locked", user.UserName);

            return RedirectToPage("./LockOut");
        }

        // If we get to this point and still can't log in, then something went wrong.
        // Do not reveal that the user does not exist or is not confirmed; this is a security best practice.
        UiResponse = UiResponse.Failure("Invalid login attempt.");

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        return Page();
    }

    /// <summary> Attempts to find a user by their user name or email address. </summary>
    /// <param name="userName">
    ///     Either the user's <see cref="ApplicationUser.UserName" /> or
    ///     <see cref="ApplicationUser.Email" />.
    /// </param>
    /// <returns> The found <see cref="ApplicationUser" />, <c> null </c> otherwise. </returns>
    private async Task<ApplicationUser?> FindUserAsync(string userName)
        => await _userManager.FindByNameAsync(userName) ?? await _userManager.FindByEmailAsync(userName);
}
