namespace WebUI;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();

        services.AddControllersWithViews();

        services.AddSwaggerGen();

        return services;
    }
}