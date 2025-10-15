using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EstadoCitaConfig : IEntityTypeConfiguration<EstadoCita>
{
    public void Configure(EntityTypeBuilder<EstadoCita> builder)
    {
        builder.ToTable("estados_cita");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();


        builder.Property(e => e.Nombre)
            .HasConversion(n => n.Value,value => new NombreVO(value))
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();
    }
}
