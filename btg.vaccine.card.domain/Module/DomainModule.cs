using btg.vaccine.card.domain.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace btg.vaccine.card.domain.Module
{
    public static class DomainModule
    {
        public static IServiceCollection AddDomainModule(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();

            return services;
        }
    }
} 