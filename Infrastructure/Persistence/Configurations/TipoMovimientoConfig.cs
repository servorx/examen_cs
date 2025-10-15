using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TipoMovimientoConfig : IEntityTypeConfiguration<TipoMovimiento>
{
    public void Configure(EntityTypeBuilder<TipoMovimiento> builder)
    {
        builder.ToTable("tipos_movimiento");

        builder.HasKey(tm => tm.Id);
        builder.Property(tm => tm.Id)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();


        builder.Property(tm => tm.Nombre)
            .HasConversion(n => n.Value, value => new NombreVO(value))
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(tm => tm.Nombre).IsUnique();
    }
}
