using System.Text;

using Erdmier.ProjectTemplate.Application.EmailProvider.Interfaces;

using Microsoft.AspNetCore.WebUtilities;

namespace Erdmier.ProjectTemplate.WebUI.Areas.Account.Pages;

public class ResendEmailConfirmation : PageModel
{
    // ReSharper disable once NotAccessedField.Local
    private readonly IEmailProvider _emailProvider;

    private readonly UserManager<ApplicationUser> _userManager;

    public ResendEmailConfirmation(IEmailProvider emailProvider, UserManager<ApplicationUser> userManager)
    {
        _emailProvider = emailProvider;
        _userManager   = userManager;
    }

    [ ViewData ]
    public string Title => "Resend Email Confirmation";

    [ BindProperty ]
    public ResendEmailConfirmationInput Input { get; set; } = new ();

    public UiResponse UiResponse { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (! ModelState.IsValid)
            return Page();

        ApplicationUser? user = await _userManager.FindByEmailAsync(Input.Email);

        if (user is null)
        {
            Log.Error("Failed to find user by email with the given email {UserEmail} when attempting to resend their email confirmation email",
                      Input.Email);

            // Do not reveal that the user doesn't exist.
            UiResponse = UiResponse.Success("Verification email sent; please check your email.");

            return Page();
        }

        string emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        emailConfirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailConfirmationToken));

        // TODO: Remove this code when the email provider is ready.
        return RedirectToPage("ConfirmEmail", new { userId = user.Id, token = emailConfirmationToken });

        // TODO: Uncomment this code when the email provider is ready.
        // string? callbackUrl = Url.Page(pageName: "/ConfirmEmail",
        //                                pageHandler: null,
        //                                values: new { area = "Account", userId = user.Id, token = emailConfirmationToken },
        //                                protocol: Request.Scheme);
        //
        // if (string.IsNullOrWhiteSpace(callbackUrl))
        // {
        //     Log.Error("Unable to generate callback URL for email confirmation for request to resend email confirmation for {UserName} / {UserEmail}",
        //               user.UserName,
        //               user.Email);
        //
        //     UiResponse = UiResponse.Failure("Unable to send confirmation email; please contact support.");
        //
        //     return Page();
        // }
        //
        // ServiceResult sendEmailResult = await _emailProvider.SendEmailAsync(new EmailRequest(IEmailProvider.Senders.NoReply,
        //                                                                                            new[] { Input.Email },
        //                                                                                            IEmailProvider.Subjects.ConfirmEmail,
        //                                                                                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."));
        //
        // if (sendEmailResult.Succeeded)
        // {
        //     UiResponse = UiResponse.Success("Verification email sent; please check your email.");
        //
        //     return Page();
        // }
        //
        // Log.Error("Unable to send email confirmation email for request to resend email confirmation for {UserName} / {UserEmail}: {ErrorMessage}",
        //           user.UserName,
        //           user.Email,
        //           sendEmailResult.Message);
        //
        // UiResponse = UiResponse.Failure("Unable to send confirmation email; please contact support.");
        //
        // return Page();
    }
}
