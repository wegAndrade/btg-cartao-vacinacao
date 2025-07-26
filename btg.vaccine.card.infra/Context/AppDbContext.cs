using btg.cartao.vacina.domain.Entities;
using btg.vaccine.card.infra.Context.Mappings;
using Microsoft.EntityFrameworkCore;

namespace btg.cartao.vacina.infra.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccineRecord> VaccineRecords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new VaccineMap());
            modelBuilder.ApplyConfiguration(new VaccineRecordMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
