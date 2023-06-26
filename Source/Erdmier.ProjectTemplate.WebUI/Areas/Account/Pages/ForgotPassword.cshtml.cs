using System.Text;

using Erdmier.ProjectTemplate.Application.EmailProvider.Interfaces;

using Microsoft.AspNetCore.WebUtilities;

namespace Erdmier.ProjectTemplate.WebUI.Areas.Account.Pages;

public class ForgotPassword : PageModel
{
    // ReSharper disable once NotAccessedField.Local
    private readonly IEmailProvider _emailProvider;

    private readonly UserManager<ApplicationUser> _userManager;

    public ForgotPassword(IEmailProvider emailProvider, UserManager<ApplicationUser> userManager)
    {
        _emailProvider = emailProvider;
        _userManager   = userManager;
    }

    [ ViewData ]
    public string Title => "Forgot Password";

    [ BindProperty ]
    public ForgotPasswordInput Input { get; set; } = new ();

    public UiResponse UiResponse { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (! ModelState.IsValid)
            return Page();

        ApplicationUser? user = await _userManager.FindByEmailAsync(Input.Email);

        if (user is null || ! await _userManager.IsEmailConfirmedAsync(user))
        {
            // Do not reveal that the user doesn't exist.
            UiResponse = UiResponse.Success("Please check your email to reset your password.");

            return Page();
        }

        string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        resetPasswordToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetPasswordToken));

        // TODO: Remove this code when the email provider is ready.
        return RedirectToPage("ResetPassword", new { token = resetPasswordToken });

        // TODO: Uncomment this code when the email provider is ready.
        // string? callbackUrl = Url.Page(pageName: "/ResetPassword",
        //                                pageHandler: null,
        //                                values: new { area = "Account", token = resetPasswordToken },
        //                                protocol: Request.Scheme);
        //
        // if (string.IsNullOrWhiteSpace(callbackUrl))
        // {
        //     Log.Error("Unable to generate callback URL for password reset for {UserName} / {UserEmail}", user.UserName, user.Email);
        //
        //     UiResponse = UiResponse.Failure("We're sorry, we're having problems on our end. Please try again later or contact support.");
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
        //     UiResponse = UiResponse.Success("Please check your email to reset your password.");
        //
        //     return Page();
        // }
        //
        // Log.Error("Unable to send password reset email for {UserName} / {UserEmail}: {ErrorMessage}",
        //           user.UserName, user.Email, sendEmailResult.Message);
        //
        // UiResponse = UiResponse.Failure("We're sorry, we're having problems on our end. Please try again later or contact support.");
        //
        // return Page();
    }
}
