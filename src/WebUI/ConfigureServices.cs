namespace WebUI;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddSwaggerGen();

        return services;
    }
}