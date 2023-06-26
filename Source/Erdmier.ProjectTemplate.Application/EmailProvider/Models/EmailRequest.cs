using System.Text.Json;
using System.Text.Json.Serialization;

namespace Erdmier.ProjectTemplate.Application.EmailProvider.Models;

/// <summary> A model representing the request to send an email. </summary>
public sealed class EmailRequest
{
    /// <summary> Initializes a new instance of the <see cref="EmailRequest" /> class. </summary>
    /// <param name="senderEmail"> The email address of the sender (e.g., no-reply@yourdomain.com). </param>
    /// <param name="recipientEmails"> An array of email addresses to whom the email will be sent. </param>
    /// <param name="subject"> The text which will appear in the email's subject line. </param>
    /// <param name="body"> The text which will appear in the body of the email. </param>
    public EmailRequest(string senderEmail, string[] recipientEmails, string subject, string body)
    {
        SenderEmail     = senderEmail;
        RecipientEmails = recipientEmails;
        Subject         = subject;
        Body            = body;
    }

    /// <summary> The email address of the sender (e.g., no-reply@yourdomain.com). </summary>
    [ JsonPropertyName("senderEmail") ]
    public string SenderEmail { get; }

    /// <summary> An array of email addresses to whom the email will be sent. </summary>
    [ JsonPropertyName("recipientEmails") ]
    public string[] RecipientEmails { get; }

    /// <summary> The text which will appear in the email's subject line. </summary>
    [ JsonPropertyName("subject") ]
    public string Subject { get; }

    /// <summary> The text which will appear in the body of the email. </summary>
    [ JsonPropertyName("body") ]
    public string Body { get; }
}

/// <summary> Provides extension methods for the <see cref="EmailRequest" /> class. </summary>
public static class EmailRequestExtensions
{
    /// <summary> Serializes the <paramref name="request" /> into a valid JSON string. </summary>
    /// <param name="request"> The <see cref="EmailRequest" /> object to be serialized. </param>
    /// <returns> A <see langword="string" /> representing the <paramref name="request" /> as JSON. </returns>
    public static string Serialize(this EmailRequest request) => JsonSerializer.Serialize(request);
}
