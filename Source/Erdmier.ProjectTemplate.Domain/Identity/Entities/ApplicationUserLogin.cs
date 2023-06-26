using Microsoft.AspNetCore.Identity;

namespace Erdmier.ProjectTemplate.Domain.Identity.Entities;

/// <summary> Associates an authentication login attempt with an <see cref="ApplicationUser" />. </summary>
public class ApplicationUserLogin : IdentityUserLogin<Guid>
{
    /// <summary> Gets or sets the <see cref="ApplicationUser" />. </summary>
    public virtual ApplicationUser User { get; set; } = null!;
}
