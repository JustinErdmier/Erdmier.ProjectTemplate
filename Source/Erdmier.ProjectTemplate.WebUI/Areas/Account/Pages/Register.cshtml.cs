using System.Text;
using System.Text.Encodings.Web;

using Erdmier.ProjectTemplate.Application.EmailProvider.Interfaces;
using Erdmier.ProjectTemplate.Application.EmailProvider.Models;

using Microsoft.AspNetCore.WebUtilities;

namespace Erdmier.ProjectTemplate.WebUI.Areas.Account.Pages;

public class Register : PageModel
{
    private readonly IEmailProvider _emailProvider;

    private readonly IUserEmailStore<ApplicationUser> _emailStore;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IUserStore<ApplicationUser> _userStore;

    public Register(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore, IEmailProvider emailProvider)
    {
        _userManager   = userManager;
        _userStore     = userStore;
        _emailStore    = (IUserEmailStore<ApplicationUser>) _userStore;
        _emailProvider = emailProvider;
    }

    [ ViewData ]
    public string Title => "Register";

    [ BindProperty ]
    public RegisterInput Input { get; set; } = new ();

    public UiResponse UiResponse { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (! ModelState.IsValid)
        {
            UiResponse = UiResponse.Failure("Please correct the form.");

            return Page();
        }

        if (! string.Equals(Input.Password, Input.ConfirmPassword))
        {
            UiResponse.Failure("Passwords do not match.");

            return Page();
        }

        // Check if the username and/or email address is already in use.
        ApplicationUser? existingUser = await _userManager.FindByNameAsync(Input.UserName);

        if (existingUser is not null)
        {
            UiResponse = UiResponse.Failure("Username is already in use.");

            return Page();
        }

        existingUser = await _userManager.FindByEmailAsync(Input.Email);

        if (existingUser is not null)
        {
            UiResponse = UiResponse.Failure("Email address is already in use.");

            return Page();
        }

        // Create the user.
        ApplicationUser newUser = new ();

        await _userStore.SetUserNameAsync(newUser, Input.UserName, CancellationToken.None);
        await _emailStore.SetEmailAsync(newUser, Input.Email, CancellationToken.None);

        IdentityResult newUserResult = await _userManager.CreateAsync(newUser, Input.Password);

        if (! newUserResult.Succeeded)
        {
            UiResponse = UiResponse.Failure("Failed to register. Please try again; if the problem persists, contact support.");

            newUserResult.Errors.ToList().ForEach(e => ModelState.AddModelError(string.Empty, e.Description));

            return Page();
        }

        // Send the confirmation email.
        string emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
        emailConfirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailConfirmationToken));

        // // TODO: Remove this code when the email provider is ready.
        // return RedirectToPage("ConfirmEmail", new { userId = newUser.Id, token = emailConfirmationToken });

        // TODO: Uncomment this code when the email provider is ready.
        string? callbackUrl = Url.Page(pageName: "/ConfirmEmail",
                                       pageHandler: null,
                                       values: new { area = "Account", userId = newUser.Id, token = emailConfirmationToken },
                                       protocol: Request.Scheme);

        if (string.IsNullOrEmpty(callbackUrl))
        {
            Log.Error("Failed to generate callback URL for email confirmation for newly registered user {UserId}", newUser.Id);

            UiResponse =
                UiResponse.Failure("Account created successfully; however we were unable to send a confirmation email. Please try requesting a new confirmation email by clicking the button below. If the problem persists, please contact support.");

            return Page();
        }

        ServiceResult emailResponse = await _emailProvider.SendEmailAsync(new EmailRequest(IEmailProvider.Senders.NoReply,
                                                                                           new[] { Input.Email },
                                                                                           IEmailProvider.Subjects.ConfirmEmail,
                                                                                           $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."));

        if (! emailResponse.Succeeded)
        {
            Log.Error("Failed to send email confirmation for newly registered user {UserId}: {ErrorMessage}", newUser.Id,
                      emailResponse.Message);

            UiResponse =
                UiResponse.Failure("Account created successfully; however we were unable to send a confirmation email. Please try requesting a new confirmation email by clicking the button below. If the problem persists, please contact support.");

            return Page();
        }

        UiResponse = UiResponse.Success("Account created successfully. Please check your email for a confirmation link.");

        return Page();
    }
}
