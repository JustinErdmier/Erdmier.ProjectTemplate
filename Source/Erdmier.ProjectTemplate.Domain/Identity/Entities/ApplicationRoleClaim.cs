using Microsoft.AspNetCore.Identity;

namespace Erdmier.ProjectTemplate.Domain.Identity.Entities;

/// <summary>
///     Associates an authorization claim that's granted to all <see cref="ApplicationUser" /> entities which are
///     associated with the given <see cref="Role" />.
/// </summary>
public class ApplicationRoleClaim : IdentityRoleClaim<Guid>
{
    /// <summary> Gets or sets the associated <see cref="ApplicationRole" />. </summary>
    public ApplicationRole Role { get; set; } = null!;
}
