namespace Erdmier.ProjectTemplate.Infrastructure.Identity.EntityConfigurations;

/// <summary> Configurations for the <see cref="ApplicationUserClaim" /> entity. </summary>
public class ApplicationUserClaimConfiguration : IEntityTypeConfiguration<ApplicationUserClaim>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ApplicationUserClaim> builder)
    {
        builder.ToTable("ApplicationUserClaims");

        builder.HasKey(auc => auc.Id);
    }
}
