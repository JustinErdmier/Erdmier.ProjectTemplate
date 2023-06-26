namespace Erdmier.ProjectTemplate.Domain.Identity.Entities;

[ StronglyTypedId ]
public partial struct ApplicationUserProfileImageId { }

/// <summary> Represents the profile image for a <see cref="ApplicationUser" />. </summary>
public class ApplicationUserProfileImage : EntityBase<ApplicationUserProfileImageId>
{
    /// <summary>
    ///     Gets or sets the <see langword="byte[]" /> of the content data of the
    ///     <see cref="ApplicationUserProfileImage" />.
    /// </summary>
    public byte[] Content { get; set; } = Array.Empty<byte>();

    /// <summary> Gets or sets the untrusted name of the <see cref="ApplicationUserProfileImage" />. </summary>
    public string UntrustedName { get; set; } = null!;

    /// <summary> Gets or sets the size of the <see cref="ApplicationUserProfileImage" /> in megabytes. </summary>
    public long Size { get; set; }

    /// <summary> Gets or sets the <see cref="FileExtension" /> of the <see cref="ApplicationUserProfileImage" />. </summary>
    public FileExtension Extension { get; set; }

    /// <summary> Gets or sets the date and time the <see cref="ApplicationUserProfileImage" /> was uploaded in UTC +00:00. </summary>
    public DateTimeOffset UploadedUtc { get; set; }

    /// <summary>
    ///     Gets or sets the id of the <see cref="ApplicationUser" /> that owns the
    ///     <see cref="ApplicationUserProfileImage" />.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary> Gets or sets the <see cref="ApplicationUser" /> that owns the <see cref="ApplicationUserProfileImage" />. </summary>
    public ApplicationUser? User { get; set; }
}

/// <summary> Provides extension methods for the <see cref="ApplicationUserProfileImage" /> entity. </summary>
public static class ApplicationUserProfileImageExtensions
{
    /// <summary> Generates the content url for the given <paramref name="image" />. </summary>
    /// <param name="image"> The <see cref="ApplicationUserProfileImage" /> entity whose content url is being generated. </param>
    /// <returns> A <see langword="string" />. </returns>
    public static string GenerateContentUrl(this ApplicationUserProfileImage image) =>
        $"data:{image.Extension.GetContentType()};base64,{Convert.ToBase64String(image.Content)}";
}
