
using btg.vaccine.card.api.Middlewares;
using btg.vaccine.card.application.Module;
using btg.vaccine.card.domain.Module;
using btg.vaccine.card.infra.Module;

namespace btg.cartao.vacina
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDomainModule();
            builder.Services.AddInfaModule(builder.Configuration);
            builder.Services.AddApplicationModule();
            builder.Services.AddControllers();
            builder.Services.AddLogging();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<NotificationMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
