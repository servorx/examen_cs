using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class FacturaConfiguration : IEntityTypeConfiguration<Factura>
{
    public void Configure(EntityTypeBuilder<Factura> builder)
    {
        builder.ToTable("facturas");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .UseIdentityByDefaultColumn();


        builder.Property(f => f.OrdenServicioId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .IsRequired();

        builder.Property(f => f.MontoRepuestos)
            .HasConversion(d => d.Value, value => new DineroVO(value))
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(f => f.ManoObra)
            .HasConversion(d => d.Value, value => new DineroVO(value))
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(f => f.Total)
            .HasConversion(d => d.Value, value => new DineroVO(value))
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(f => f.FechaGeneracion)
            .HasConversion(fv => fv.Value, value => new FechaHistoricaVO(value))
            .HasColumnType("timestamp with time zone")
            .IsRequired();

                // agregar las columnas del base entity
        builder.Property(v => v.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(v => v.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Relaciones
        builder.HasOne(f => f.OrdenServicio)
            .WithMany(o => o.Facturas)
            .HasForeignKey(f => f.OrdenServicioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.Pagos)
            .WithOne(p => p.Factura)
            .HasForeignKey(p => p.FacturaId);
    }
}
