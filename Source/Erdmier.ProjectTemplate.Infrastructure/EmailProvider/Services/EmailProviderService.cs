using Microsoft.Extensions.Options;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace Erdmier.ProjectTemplate.Infrastructure.EmailProvider.Services;

/// <summary> A service which provides the ability to send emails. </summary>
public sealed class EmailProviderService : IEmailProvider
{
    private readonly EmailProviderOptions _options;

    public EmailProviderService(IOptions<EmailProviderOptions> optionsAccessor) => _options = optionsAccessor.Value;

    /// <inheritdoc />
    public async Task<EmailResult> SendEmailAsync(EmailRequest request)
    {
        if (string.IsNullOrWhiteSpace(_options.ApiKey))
            Log.Error("EmailProvider API Key is not set");

        SendGridClient client = new (_options.ApiKey);

        SendGridMessage message = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress(request.SenderEmail),
                                                                                   request.RecipientEmails
                                                                                          .Select(email => new EmailAddress(email))
                                                                                          .ToList(),
                                                                                   request.Subject,
                                                                                   string.Empty,
                                                                                   request.Body);

        message.SetClickTracking(false, false);

        Response sendEmailResponse = await client.SendEmailAsync(message);

        if (sendEmailResponse.IsSuccessStatusCode)
            return EmailResult.Success();

        Log.Error("Failed to send email to {RecipientEmails} with subject {EmailSubject}. Status code: {StatusCode}",
                  request.RecipientEmails,
                  request.Subject,
                  sendEmailResponse.StatusCode);

        return EmailResult.Failure();
    }
}
