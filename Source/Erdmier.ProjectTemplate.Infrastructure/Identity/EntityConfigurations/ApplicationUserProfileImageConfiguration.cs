namespace Erdmier.ProjectTemplate.Infrastructure.Identity.EntityConfigurations;

/// <summary> Configurations for the <see cref="ApplicationUserProfileImage" /> entity. </summary>
public class ApplicationUserProfileImageConfiguration : IEntityTypeConfiguration<ApplicationUserProfileImage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUserProfileImage> builder)
    {
        builder.ToTable("ApplicationUserProfileImages");

        builder.HasKey(aupi => aupi.Id);

        builder.Property(aupi => aupi.Content)
               .HasMaxLength(2097152)
               .IsRequired();

        builder.Property(pi => pi.UntrustedName)
               .IsRequired();

        builder.Property(pi => pi.Extension)
               .HasConversion<string>()
               .HasMaxLength(10);
    }
}
