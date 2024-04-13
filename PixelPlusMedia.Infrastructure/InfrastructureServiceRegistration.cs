using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PixelPlusMedia.Application.Contracts.Infrastructure;
using PixelPlusMedia.Application.Models.Mailer;
using PixelPlusMedia.Infrastructure.Mailer;

namespace PixelPlusMedia.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, MailerService>();

        return services;
    }
}
