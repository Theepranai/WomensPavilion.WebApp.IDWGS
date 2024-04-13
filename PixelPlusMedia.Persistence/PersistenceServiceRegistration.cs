using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelPlusMedia.Application.Contracts.aws;
using PixelPlusMedia.Application.Contracts.Persistence;
using PixelPlusMedia.Persistence.Repositories;

namespace PixelPlusMedia.Persistence;
public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => {
            options.UseNpgsql(configuration.GetConnectionString("DBConnectionString"));
            options.EnableSensitiveDataLogging();
        });

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserDetailRepository, UserDetailRepository>();
        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<ISubMessageRepository, SubMessageRepository>();
        services.AddScoped<IAWSServiceRepository, AWSServiceRepository>();

        return services;
    }
}
