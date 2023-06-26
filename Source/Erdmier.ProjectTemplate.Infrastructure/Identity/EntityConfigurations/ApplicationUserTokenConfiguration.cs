namespace Erdmier.ProjectTemplate.Infrastructure.Identity.EntityConfigurations;

/// <summary> Configures how to design and map the database table for <see cref="ApplicationUserToken" />. </summary>
public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        // Maps to the ApplicationUserTokens table.
        builder.ToTable("ApplicationUserTokens");

        // Composite primary key consisting of the UserId, LoginProvider, and Name.
        builder.HasKey(aut => new { aut.UserId, aut.LoginProvider, aut.Name });

        // Limit the size of the composite key columns due to common DB restrictions.
        builder.Property(aut => aut.LoginProvider).HasMaxLength(128);
        builder.Property(aut => aut.Name).HasMaxLength(128);
    }
}
