using Microsoft.AspNetCore.Identity;

namespace Erdmier.ProjectTemplate.Domain.Identity.Entities;

/// <summary> Represents an authentication token for an <see cref="ApplicationUser" />. </summary>
public class ApplicationUserToken : IdentityUserToken<Guid>
{
    /// <summary> Gets or sets the <see cref="ApplicationUser" />. </summary>
    public virtual ApplicationUser User { get; set; } = null!;
}
