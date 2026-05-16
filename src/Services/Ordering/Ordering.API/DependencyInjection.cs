namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiService(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

    public static WebApplication AddApi(this WebApplication app)
    {
        return app;
    }
}
