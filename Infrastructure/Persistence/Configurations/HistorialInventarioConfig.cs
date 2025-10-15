using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Configurations;

public class HistorialInventarioConfig : IEntityTypeConfiguration<HistorialInventario>
{
    public void Configure(EntityTypeBuilder<HistorialInventario> builder)
    {
        builder.ToTable("historial_inventario");

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .UseIdentityByDefaultColumn();


        builder.Property(h => h.RepuestoId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .IsRequired();

        builder.Property(h => h.AdminId)
            .HasConversion(
                id => id != null ? id.Value : (int?)null,
                value => value.HasValue ? new IdVO(value.Value) : null
            );

        builder.Property(h => h.TipoMovimientoId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .IsRequired();

        builder.Property(h => h.Cantidad)
            .HasConversion(c => c.Value, value => new CantidadVO(value))
            .IsRequired();

        builder.Property(h => h.FechaMovimiento)
            .HasConversion(f => f.Value, value => new FechaHistoricaVO(value))
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(h => h.Observaciones)
            .HasConversion(
                o => o != null ? o.Value : null,
                value => value != null ? new DescripcionVO(value) : null
            )
            .HasMaxLength(255);

        builder.HasOne(h => h.Repuesto)
            .WithMany(r => r.Historiales)
            .HasForeignKey(h => h.RepuestoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.Administrador)
            .WithMany()
            .HasForeignKey(h => h.AdminId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(h => h.TipoMovimiento)
            .WithMany()
            .HasForeignKey(h => h.TipoMovimientoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
