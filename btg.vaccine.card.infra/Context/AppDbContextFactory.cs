using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace btg.cartao.vacina.infra.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=btg_vaccine_db;User Id=sa;Password=vacccine@Passw0rd;TrustServerCertificate=true;MultipleActiveResultSets=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
} 