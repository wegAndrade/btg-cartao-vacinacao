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
            builder.HasKey(vr => new { vr.PersonId, vr.VaccineId });
            
            builder.Property(vr => vr.PersonId)
                .IsRequired();
                
            builder.Property(vr => vr.VaccineId)
                .IsRequired();
                
            builder.Property(vr => vr.Applications)
                .IsRequired();
                
            builder.Property(vr => vr.ApplicationDate)
                .IsRequired();
                
            builder.Property(vr => vr.UpdateDate)
                .IsRequired();

            // Relacionamentos
            builder.HasOne<Person>()
                .WithMany()
                .HasForeignKey(vr => vr.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Vaccine>()
                .WithMany()
                .HasForeignKey(vr => vr.VaccineId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 