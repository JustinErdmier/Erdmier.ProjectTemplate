namespace Erdmier.ProjectTemplate.Application.EmailProvider.Models;

public class EmailProviderOptions
{
    public const string ConfigurationSectionName = "EmailProvider";

    public string ApiKey { get; set; } = string.Empty;
}
