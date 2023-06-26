namespace Erdmier.ProjectTemplate.Domain.Common.Enums;

/// <summary> Represents the extension of the file (e.g., .txt, .doc, .jpg, etc.). </summary>
public enum FileExtension { Png, Jpg, Jpeg }

/// <summary> Provides extension methods for the <see cref="FileExtension" /> enum. </summary>
public static class FileExtensionExtensions
{
    /// <summary>
    ///     Converts the given <paramref name="fileExtension" /> to a standardized string format including the dot (e.g.,
    ///     FileExtension.Png => ".png").
    /// </summary>
    /// <param name="fileExtension"> The <see cref="FileExtension" /> member to be converted. </param>
    /// <returns> A <see langword="string" />. </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if the given <paramref name="fileExtension" /> is not a member of
    ///     the <see cref="FileExtension" /> enum.
    /// </exception>
    public static string ToString(this FileExtension fileExtension) =>
        fileExtension switch
        {
            FileExtension.Png  => ".png",
            FileExtension.Jpg  => ".jpg",
            FileExtension.Jpeg => ".jpeg",
            _ => throw new ArgumentOutOfRangeException(nameof(fileExtension), fileExtension,
                                                       $"{nameof(fileExtension)} must be a member of the {nameof(FileExtension)} enum.")
        };

    /// <summary> Attempts to parse the given <paramref name="fileExtensionString" /> to a <see cref="FileExtension" /> member. </summary>
    /// <param name="fileExtensionString"> A <see langword="string" /> representing the <see cref="FileExtension" />. </param>
    /// <returns> A <see cref="FileExtension" /> member. </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if the given <paramref name="fileExtensionString" /> does not
    ///     match one of the accepted patterns for a supported file extension.
    /// </exception>
    public static FileExtension ToFileExtension(this string fileExtensionString) =>
        fileExtensionString.ToLowerInvariant() switch
        {
            ".png" or "png"   => FileExtension.Png,
            ".jpg" or "jpg"   => FileExtension.Jpg,
            ".jpeg" or "jpeg" => FileExtension.Jpeg,
            _                 => throw new ArgumentOutOfRangeException(nameof(fileExtensionString), fileExtensionString, null)
        };

    /// <summary>
    ///     Gets a <see langword="string" /> representing the content type of the given <paramref name="fileExtension" />
    ///     to be included in the file's data string.
    /// </summary>
    /// <param name="fileExtension"> The <see cref="FileExtension" /> member whose content type is to be gotten. </param>
    /// <returns> A <see langword="string" />. </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if the given <paramref name="fileExtension" /> is not a member of
    ///     the <see cref="FileExtension" /> enum.
    /// </exception>
    public static string GetContentType(this FileExtension fileExtension) =>
        fileExtension switch
        {
            FileExtension.Png  => "image/png",
            FileExtension.Jpg  => "image/jpeg",
            FileExtension.Jpeg => "image/jpeg",
            _ => throw new ArgumentOutOfRangeException(nameof(fileExtension), fileExtension,
                                                       $"{nameof(fileExtension)} must be a member of the {nameof(FileExtension)} enum.")
        };
}
