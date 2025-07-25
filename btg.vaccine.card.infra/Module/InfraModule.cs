using btg.cartao.vacina.infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace btg.vaccine.card.infra.Module
{
    public static class InfraModule
    {
        public static IServiceCollection AddInfaModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));

            return services;
        }
    }
}
