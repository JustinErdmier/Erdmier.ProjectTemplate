namespace Erdmier.ProjectTemplate.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
{
    /// <summary> The key used to find and retrieve the connection string from configuration. </summary>
    public const string ConnectionStringKey = "ConnectionStrings:ApplicationDbContext";

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ApplicationUserProfileImage> ProfileImages => Set<ApplicationUserProfileImage>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure the entities.
        builder.ConfigureIdentity();
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

        base.OnConfiguring(optionsBuilder);
    }
}
