using Microsoft.Extensions.Options;
using Sales.API.Extensions;
using Sales.API.Middlewares;
using Sales.API.Services;
using Sales.API.Services.Cart;
using Sales.API.Services.Catalog;
using Sales.API.Services.Order;
using Sales.API.Services.Payment;
using Sales.API.Services.User;
using System.Reflection;

namespace Sales.API.Configurations;

public static class DependencyInjection
{
    public static void RegisterAllDependencies(this WebApplicationBuilder builder)
    {
        builder.AddHttpClientServices();
        builder.RegisterModelsSettings();
        builder.AddServices();
        builder.AddCustomMiddlewares();
        builder.AddHandlers();
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IUserService, UserService>();
    }

    public static void AddHandlers(this WebApplicationBuilder builder) =>
        builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    public static void AddHttpClientServices(this WebApplicationBuilder builder)
    {
        builder.AddHttpClientService<ICartRestService, CartRestService>(options => options.CartUrl);
        builder.AddHttpClientService<ICatalogRestService, CatalogRestService>(options => options.CatalogUrl);
        builder.AddHttpClientService<IPaymentRestService, PaymentRestService>(options => options.PaymentUrl);
        builder.AddHttpClientService<IOrderRestService, OrderRestService>(options => options.OrderUrl);
    }

    private static void AddHttpClientService<TInterface, TImplementation>(
        this WebApplicationBuilder builder,
        Func<AppServicesSettings, string> getServiceUrl)
        where TInterface : class
        where TImplementation : Service, TInterface
    {
        builder.Services.AddHttpClient<TInterface, TImplementation>((serviceProvider, httpClient) =>
        {
            var appServices = serviceProvider.GetRequiredService<IOptions<AppServicesSettings>>().Value;

            using var scope = serviceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var token = userService.GetToken();

            var serviceUrl = getServiceUrl(appServices);

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            httpClient.BaseAddress = new Uri(serviceUrl);
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            };
        })
        .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
    }

    public static void RegisterModelsSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AppServicesSettings>(builder.Configuration.GetSection(nameof(AppServicesSettings)));
    }

    public static void AddCustomMiddlewares(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<GlobalExceptionMiddleware>();
    }
}
