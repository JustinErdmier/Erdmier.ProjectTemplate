using Microsoft.AspNetCore.Identity;

namespace Erdmier.ProjectTemplate.Domain.Identity.Entities;

/// <summary> Represents a role for authorizing requests in role-based authorization processes. </summary>
public class ApplicationRole : IdentityRole<Guid>
{
    /// <summary> Instantiates a new <see cref="ApplicationRole" />. </summary>
    public ApplicationRole() { }

    /// <summary> Instantiates a new <see cref="ApplicationRole" />. </summary>
    /// <param name="roleName"> The name of the <see cref="ApplicationRole" />. </param>
    public ApplicationRole(string roleName) : base(roleName) { }

    /// <summary> Gets or sets a list of associated <see cref="ApplicationUser" /> entities. </summary>
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = null!;

    /// <summary> Gets or sets a list of associated <see cref="ApplicationRoleClaim" /> entities. </summary>
    public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = null!;
}
