using Microsoft.AspNetCore.Identity;

namespace Erdmier.ProjectTemplate.Domain.Identity.Entities;

/// <summary> Associates an authorization claim with the given <see cref="User" />. </summary>
public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
    /// <summary> Gets or sets the associated <see cref="ApplicationUser" />. </summary>
    public virtual ApplicationUser User { get; set; } = null!;
}
