namespace Erdmier.ProjectTemplate.Infrastructure.Extensions;

/// <summary> Provides extension methods for registering and configuring services in the dependency injection container. </summary>
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
                                                       IWebHostEnvironment     environment)
        => services
           .AddServices()
           .ConfigureOptions(configuration)
           .AddDbContexts(configuration, environment)
           .AddIdentity();

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailProvider, EmailProviderService>();

        return services;
    }

    private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailProviderOptions>(configuration.GetSection(EmailProviderOptions.ConfigurationSectionName));

        return services;
    }

    private static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration,
                                                    IWebHostEnvironment     environment)
    {
        string?              connectionString = configuration[ApplicationDbContext.ConnectionStringKey];
        MariaDbServerVersion serverVersion;

        try
        {
            serverVersion = new MariaDbServerVersion(ServerVersion.AutoDetect(connectionString));
        }
        catch (Exception exception)
        {
            if (connectionString is not null)
            {
                Log.Fatal("Failed to auto-detect MySQL server version from connection string: {ConnectionString}. Error Message: {ErrorMessage}",
                          connectionString,
                          exception.Message);

                throw;
            }

            Log.Fatal("Connection string is null; failed to detect MySql Server Version");

            Log.Error("Connection string is null; please ensure the connection string is set to the key: {ConnectionStringKey}",
                      ApplicationDbContext.ConnectionStringKey);

            throw new ArgumentNullException(nameof(connectionString), "Connection string is null.");
        }

        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            if (environment.IsDevelopment())
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }

            options.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

            options.UseMySql(connectionString, serverVersion,
                             mySqlOptions => mySqlOptions.MigrationsAssembly("Erdmier.ProjectTemplate.Infrastructure")
                                                         .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });

        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication();

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                                     .RequireAuthenticatedUser()
                                     .Build();
        });

        services.Configure<IdentityOptions>(options =>
        {
            // Sign In settings.
            options.SignIn.RequireConfirmedAccount = true;

            // Password settings.
            options.Password.RequireDigit           = true;
            options.Password.RequireLowercase       = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase       = true;
            options.Password.RequiredLength         = 8;
            options.Password.RequiredUniqueChars    = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(20);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers      = true;

            // User settings.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-._@+";
            options.User.RequireUniqueEmail        = true;
        });

        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/AccessDenied";
            options.Cookie.Name      = "ErdmierProjectTemplateCookie";
            options.Cookie.HttpOnly  = true;
            options.ExpireTimeSpan   = TimeSpan.FromMinutes(60);
            options.LoginPath        = "/Account/LogIn";

            // ReturnUrlParameter requires
            //using Microsoft.AspNetCore.Authentication.Cookies;
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration  = true;
        });

        return services;
    }
}
