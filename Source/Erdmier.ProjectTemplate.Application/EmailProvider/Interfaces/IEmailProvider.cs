namespace Erdmier.ProjectTemplate.Application.EmailProvider.Interfaces;

/// <summary> A service which provides the ability to send emails. </summary>
public interface IEmailProvider
{
    /// <summary> Processes the <paramref name="request" /> to send an email. </summary>
    /// <param name="request">
    ///     The <see cref="EmailRequest" /> representing the data to use for generating and sending the
    ///     email.
    /// </param>
    /// <returns> An <see cref="EmailResult" />. </returns>
    Task<EmailResult> SendEmailAsync(EmailRequest request);

    /// <summary> A collection of email addresses which may be used as the sender of an email. </summary>
    public static class Senders
    {
        /// <summary> The email address which should be used when sending emails which do not require a reply. </summary>
        public const string NoReply = "no-reply@yourdomain.com";
    }

    /// <summary> A collection of email subjects which may be used when sending emails. </summary>
    public static class Subjects
    {
        /// <summary> The name of the company which will be used in the email subject line. </summary>
        private const string CompanyName = "Erdmier Project Template";

        /// <summary> The subject line which should be used when sending an email to confirm a user's email address. </summary>
        public const string ConfirmEmail = $"Confirm Your Email - {CompanyName}";

        /// <summary> The subject line which should be used when sending an email to verify a user's email address. </summary>
        public const string VerifyEmail = $"Verify Your Email - {CompanyName}";

        /// <summary> The subject line which should be used when sending an email to set a user's password. </summary>
        public const string SetPassword = $"Set Your Password - {CompanyName}";

        /// <summary> The subject line which should be used when sending an email to reset a user's password. </summary>
        public const string ResetPassword = $"Reset Your Password - {CompanyName}";
    }
}
