namespace Erdmier.ProjectTemplate.Application.Extensions;

/// <summary> Provides extensions methods for registering and configuring services in the dependency injection container. </summary>
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();

        return services;
    }
}
