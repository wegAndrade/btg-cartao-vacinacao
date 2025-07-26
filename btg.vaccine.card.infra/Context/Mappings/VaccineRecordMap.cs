using btg.cartao.vacina.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace btg.vaccine.card.infra.Context.Mappings
{
    public class VaccineRecordMap : IEntityTypeConfiguration<VaccineRecord>
    {
        public void Configure(EntityTypeBuilder<VaccineRecord> builder)
        {
            builder.ToTable(nameof(VaccineRecord));
            builder.HasKey(vr => vr.Id);
            
            builder.Property(vr => vr.IdPessoa)
                .IsRequired();
                
            builder.Property(vr => vr.Vacina)
                .IsRequired();
                
            builder.Property(vr => vr.Doses)
                .IsRequired();
                
            builder.Property(vr => vr.DataAplicacao)
                .IsRequired();
                
            builder.Property(vr => vr.DataAtualizacao)
                .IsRequired();

            // Relacionamentos
            builder.HasOne<Person>()
                .WithMany()
                .HasForeignKey(vr => vr.IdPessoa)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Vaccine>()
                .WithMany()
                .HasForeignKey(vr => vr.Vacina)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 