namespace Erdmier.ProjectTemplate.Infrastructure.Persistence.EntityConfigurationModelBuilders;

public static class ApplicationDbContextEntityConfigurationModelBuilder
{
    /// <summary> Configures entity models for the Identity API. </summary>
    /// <param name="modelBuilder"> The <see cref="ModelBuilder" /> used for configuring the entity models. </param>
    public static void ConfigureIdentity(this ModelBuilder modelBuilder)
    {
        new ApplicationUserConfiguration().Configure(modelBuilder.Entity<ApplicationUser>());
        new ApplicationUserProfileImageConfiguration().Configure(modelBuilder.Entity<ApplicationUserProfileImage>());
        new ApplicationRoleConfiguration().Configure(modelBuilder.Entity<ApplicationRole>());
        new ApplicationUserRoleConfiguration().Configure(modelBuilder.Entity<ApplicationUserRole>());
        new ApplicationUserClaimConfiguration().Configure(modelBuilder.Entity<ApplicationUserClaim>());
        new ApplicationUserLoginConfiguration().Configure(modelBuilder.Entity<ApplicationUserLogin>());
        new ApplicationUserTokenConfiguration().Configure(modelBuilder.Entity<ApplicationUserToken>());
        new ApplicationRoleClaimConfiguration().Configure(modelBuilder.Entity<ApplicationRoleClaim>());
    }
}
