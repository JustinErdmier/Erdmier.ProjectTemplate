using Microsoft.AspNetCore.Identity;

namespace Erdmier.ProjectTemplate.Domain.Identity.Entities;

/// <summary> Represents an association between an <see cref="ApplicationUser" /> and an <see cref="ApplicationRole" />. </summary>
public class ApplicationUserRole : IdentityUserRole<Guid>
{
    /// <summary> Gets or sets the <see cref="ApplicationUser" /> associated with <see cref="Role" />. </summary>
    public virtual ApplicationUser User { get; set; } = null!;

    /// <summary> Gets or sets the <see cref="ApplicationRole" /> associated with <see cref="User" />. </summary>
    public virtual ApplicationRole Role { get; set; } = null!;
}
