using Microsoft.AspNetCore.Identity;

namespace Erdmier.ProjectTemplate.Domain.Identity.Entities;

/// <summary> Represents a user of the application. </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary> Gets or sets a list of associated <see cref="ApplicationUserClaim" /> entities. </summary>
    public virtual ICollection<ApplicationUserClaim> Claims { get; set; } = null!;

    /// <summary> Gets or sets a list of associated <see cref="ApplicationUserLogin" /> entities. </summary>
    public virtual ICollection<ApplicationUserLogin> Logins { get; set; } = null!;

    /// <summary> Gets or sets a list of associated <see cref="ApplicationUserToken" /> entities. </summary>
    public virtual ICollection<ApplicationUserToken> Tokens { get; set; } = null!;

    /// <summary> Gets or sets a list of associated <see cref="ApplicationUserRole" /> entities. </summary>
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = null!;

    /// <summary> Gets or sets the user's <see cref="ApplicationUserProfileImage" /> entity. </summary>
    public ApplicationUserProfileImage? ProfileImage { get; set; }
}
