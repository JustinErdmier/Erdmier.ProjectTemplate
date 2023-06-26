namespace Erdmier.ProjectTemplate.Infrastructure.Identity.EntityConfigurations;

/// <summary> Configurations for the <see cref="ApplicationUserLogin" /> entity. </summary>
public class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
    {
        builder.ToTable("ApplicationUserLogins");

        builder.HasKey(aul => aul.UserId);

        // Limit the size of the composite key columns due to common DB restrictions.
        builder.Property(aul => aul.LoginProvider).HasMaxLength(128);
        builder.Property(aul => aul.ProviderKey).HasMaxLength(128);
    }
}
