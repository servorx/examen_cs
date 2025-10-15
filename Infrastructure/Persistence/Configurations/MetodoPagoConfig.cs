using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class MetodoPagoConfig : IEntityTypeConfiguration<MetodoPago>
{
    public void Configure(EntityTypeBuilder<MetodoPago> builder)
    {
        builder.ToTable("metodos_pago");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(v => v.Value, value => new IdVO(value))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();


        builder.Property(m => m.Nombre)
            .HasConversion(n => n.Value, value => new NombreVO(value))
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();
    }
}
