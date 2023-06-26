namespace Erdmier.ProjectTemplate.WebUI.Extensions;

/// <summary> Provides extension methods for registering and configuring services to the dependency injection container. </summary>
public static class DependencyInjectionExtensions
{
    /// <summary> Registers and configures the services required for the framework to function. </summary>
    /// <param name="services"> The <see cref="IServiceCollection" /> to which the services are being registered. </param>
    /// <param name="environment"> The <see cref="IWebHostEnvironment" /> representing the app's runtime environment. </param>
    /// <returns> The modified <paramref name="services" />. </returns>
    public static IServiceCollection AddWebUI(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddControllers();

        services.AddRazorPages(options =>
        {
            options.Conventions.AllowAnonymousToAreaPage("Account", "/LogIn");
            options.Conventions.AllowAnonymousToAreaPage("Account", "/Register");
            options.Conventions.AllowAnonymousToAreaPage("Account", "/ConfirmEmail");
            options.Conventions.AllowAnonymousToAreaPage("Account", "/ResendEmailConfirmation");
            options.Conventions.AllowAnonymousToAreaPage("Account", "/ResetPassword");
            options.Conventions.AllowAnonymousToAreaPage("Account", "/ForgotPassword");
        });

        services.AddServerSideBlazor(options =>
        {
            if (environment.IsDevelopment())
                options.DetailedErrors = true;
        });

        services.AddHsts(options =>
        {
            options.Preload           = true;
            options.IncludeSubDomains = true;
            options.MaxAge            = TimeSpan.FromDays(60);
        });

        services.AddHttpClient();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddScoped(sp =>
        {
            ActionContext? actionContext = sp.GetRequiredService<IActionContextAccessor>().ActionContext;
            var            factory       = sp.GetRequiredService<IUrlHelperFactory>();

            // Marking this with "!" as it should only ever be requested in the context of a request, in which case
            // the ActionContextAccessor will always have a value. If this throws a runtime exception, it's because a
            // service is attempting to access the IUrlHelper outside of a request context.
            // If that's the case, don't 😊.
            return factory.GetUrlHelper(actionContext!);
        });

        return services;
    }
}
