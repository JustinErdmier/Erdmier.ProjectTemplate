namespace Erdmier.ProjectTemplate.Infrastructure.Identity.EntityConfigurations;

/// <summary> Configurations for the <see cref="ApplicationUserRole" /> entity. </summary>
public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.ToTable("ApplicationUserRoles");

        builder.HasKey(aur => new { aur.UserId, aur.RoleId });
    }
}
