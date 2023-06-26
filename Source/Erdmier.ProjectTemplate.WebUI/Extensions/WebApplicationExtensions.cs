namespace Erdmier.ProjectTemplate.WebUI.Extensions;

/// <summary> Provides extension methods for configuring the application. </summary>
public static class WebApplicationExtensions
{
    /// <summary> Adds and configures Serilog as the app's default logger. </summary>
    /// <param name="builder"> The <see cref="ConfigureHostBuilder" /> used to add and configure Serilog. </param>
    /// <returns> The modified <paramref name="builder" />. </returns>
    public static ConfigureHostBuilder AddSerilog(this ConfigureHostBuilder builder)
    {
        builder.UseSerilog((context, config) => config
                                                .ReadFrom.Configuration(context.Configuration));

        return builder;
    }

    /// <summary> Configures the middleware for the application. </summary>
    /// <param name="app"> The <see cref="WebApplication" /> whose middleware is being configured. </param>
    /// <returns> The modified <paramref name="app" />. </returns>
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (! app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        return app;
    }
}
