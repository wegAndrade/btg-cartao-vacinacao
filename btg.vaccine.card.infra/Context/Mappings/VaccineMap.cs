using btg.cartao.vacina.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace btg.vaccine.card.infra.Context.Mappings
{
    public class VaccineMap : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.ToTable(nameof(Vaccine));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
        }
    }
}
