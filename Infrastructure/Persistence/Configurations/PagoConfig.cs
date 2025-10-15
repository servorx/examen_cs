using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PagoConfig : IEntityTypeConfiguration<Pago>
{
    public void Configure(EntityTypeBuilder<Pago> builder)
    {
        builder.ToTable("pagos");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();


        builder.Property(p => p.FacturaId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .HasColumnName("factura_id")
            .IsRequired();

        builder.Property(p => p.MetodoPagoId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .HasColumnName("metodo_pago_id")
            .IsRequired();

        builder.Property(p => p.EstadoPagoId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .HasColumnName("estado_pago_id")
            .IsRequired();

        builder.Property(p => p.Monto)
            .HasConversion(m => m.Value, value => new DineroVO(value))
            .HasColumnName("monto")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(p => p.FechaPago)
            .HasConversion(f => f.Value, value => new FechaHistoricaVO(value))
            .HasColumnName("fecha_pago")
            .IsRequired();
        
        builder.Property(m => m.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(m => m.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(p => p.Factura)
            .WithMany(f => f.Pagos)
            .HasForeignKey(p => p.FacturaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.MetodoPago)
            .WithMany()
            .HasForeignKey(p => p.MetodoPagoId);

        builder.HasOne(p => p.EstadoPago)
            .WithMany()
            .HasForeignKey(p => p.EstadoPagoId);
    }
}
