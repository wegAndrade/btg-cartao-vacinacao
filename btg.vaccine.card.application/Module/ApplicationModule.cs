using Microsoft.Extensions.DependencyInjection;

namespace btg.vaccine.card.application.Module
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {

            services.AddMediatR(new MediatRServiceConfiguration()
            {
                AutoRegisterRequestProcessors = true,
            });

            return services;
        }
    }
}
