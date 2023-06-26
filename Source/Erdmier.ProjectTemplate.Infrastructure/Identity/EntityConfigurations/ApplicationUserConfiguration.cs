namespace Erdmier.ProjectTemplate.Infrastructure.Identity.EntityConfigurations;

/// <summary> Configurations for the <see cref="ApplicationUser" /> entity. </summary>
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUsers");

        builder.HasKey(au => au.Id);

        builder.HasIndex(au => au.NormalizedUserName)
               .IsUnique();

        builder.HasIndex(au => au.NormalizedEmail)
               .IsUnique();

        builder.Property(au => au.ConcurrencyStamp)
               .IsConcurrencyToken();

        builder.Property(au => au.UserName)
               .IsRequired();

        builder.Property(au => au.Email)
               .IsRequired();

        builder.Property(au => au.UserName)
               .HasMaxLength(256);

        builder.Property(au => au.NormalizedUserName)
               .HasMaxLength(256);

        builder.Property(au => au.Email)
               .HasMaxLength(256);

        builder.Property(au => au.NormalizedEmail)
               .HasMaxLength(256);

        builder.HasMany(au => au.Claims)
               .WithOne(auc => auc.User)
               .HasForeignKey(auc => auc.UserId)
               .IsRequired();

        builder.HasMany(au => au.Logins)
               .WithOne(aul => aul.User)
               .HasForeignKey(aul => aul.UserId)
               .IsRequired();

        builder.HasMany(au => au.Tokens)
               .WithOne(aut => aut.User)
               .HasForeignKey(aut => aut.UserId)
               .IsRequired();

        builder.HasMany(au => au.UserRoles)
               .WithOne(aur => aur.User)
               .HasForeignKey(aur => aur.UserId)
               .IsRequired();

        builder.HasOne(au => au.ProfileImage)
               .WithOne(pi => pi.User)
               .HasForeignKey<ApplicationUserProfileImage>(pi => pi.UserId);
    }
}
