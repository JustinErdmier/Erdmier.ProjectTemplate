using Erdmier.ProjectTemplate.Application.Extensions;
using Erdmier.ProjectTemplate.Infrastructure.Extensions;
using Erdmier.ProjectTemplate.WebUI.Extensions;

Log.Logger = new LoggerConfiguration()
             .WriteTo
             .Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Properties}{NewLine}{Exception}")
             .CreateBootstrapLogger();

Log.Information("Booting up Erdmier.ProjectTemplate...");

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    {
        builder.Host.AddSerilog();

        builder.Services.AddWebUI(builder.Environment)
               .AddInfrastructure(builder.Configuration, builder.Environment)
               .AddApplication();
    }

    WebApplication app = builder.Build();

    {
        app.ConfigureMiddleware();

        app.Run();
    }
}
catch (Exception exception)
{
    Log.Fatal(exception, "Unhandled exception caught at boot up...");
}
finally
{
    Log.Information("Shutting down Erdmier.ProjectTemplate...");
    Log.CloseAndFlush();
}
