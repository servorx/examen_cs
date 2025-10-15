using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class DetalleOrdenConfig : IEntityTypeConfiguration<DetalleOrden>
{
    public void Configure(EntityTypeBuilder<DetalleOrden> builder)
    {
        builder.ToTable("detalle_orden");

        builder.HasKey(d => new { d.OrdenServicioId, d.RepuestoId });

        builder.Property(d => d.OrdenServicioId)
            .HasConversion(v => v.Value, v => new IdVO(v))
            .HasColumnName("orden_servicio_id");

        builder.Property(d => d.RepuestoId)
            .HasConversion(v => v.Value, v => new IdVO(v))
            .HasColumnName("repuesto_id");

        builder.Property(d => d.Cantidad)
            .HasConversion(v => v.Value, v => new CantidadVO(v))
            .HasColumnName("cantidad");

        builder.Property(d => d.Costo)
            .HasConversion(v => v.Value, v => new DineroVO(v))
            .HasColumnName("costo")
            .HasPrecision(10, 2);

        builder.HasOne(d => d.OrdenServicio)
            .WithMany(o => o.Detalles)
            .HasForeignKey(d => d.OrdenServicioId)
            .HasConstraintName("fk_do_orden")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.Repuesto)
            .WithMany(r => r.DetallesOrden)
            .HasForeignKey(d => d.RepuestoId)
            .HasConstraintName("fk_do_repuesto");
    }
}
