using SharedLib.Tokens.Configuration;

namespace Sales.API.Configurations;

public static class ApiConfiguration
{
    internal const int DEFAULT_PAGE_SIZE = 10;
    internal const int DEFAULT_PAGE = 1;

    public static void AddSetupConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerConfig();
    }

    public static void AddCorsConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Total", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
    }

    public static void UseSecurity(this IApplicationBuilder app)
    {
        app.UseSwaggerConfig();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("Total");

        app.UseAuthConfiguration();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
