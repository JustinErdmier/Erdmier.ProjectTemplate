namespace Erdmier.ProjectTemplate.Infrastructure.Identity.EntityConfigurations;

/// <summary> Configurations for the <see cref="ApplicationRole" /> entity. </summary>
public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("ApplicationRoles");

        builder.HasKey(ar => ar.Id);

        builder.HasIndex(ar => ar.Id);

        builder.HasIndex(ar => ar.NormalizedName)
               .IsUnique();

        builder.Property(ar => ar.ConcurrencyStamp)
               .IsConcurrencyToken();

        builder.Property(ar => ar.Name)
               .HasMaxLength(256);

        builder.Property(ar => ar.NormalizedName)
               .HasMaxLength(256);

        builder.HasMany(ar => ar.UserRoles)
               .WithOne(aur => aur.Role)
               .HasForeignKey(aur => aur.RoleId)
               .IsRequired();

        builder.HasMany(ar => ar.RoleClaims)
               .WithOne(arc => arc.Role)
               .HasForeignKey(arc => arc.RoleId)
               .IsRequired();
    }
}
