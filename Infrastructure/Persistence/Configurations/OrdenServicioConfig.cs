using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class OrdenServicioConfig: IEntityTypeConfiguration<OrdenServicio>
{
    public void Configure(EntityTypeBuilder<OrdenServicio> builder)
    {
        builder.ToTable("orden_servicio");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .UseIdentityByDefaultColumn();


        builder.Property(o => o.VehiculoId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .IsRequired();

        builder.Property(o => o.MecanicoId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .IsRequired();

        builder.Property(o => o.TipoServicioId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .IsRequired();

        builder.Property(o => o.EstadoId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .IsRequired();

        builder.Property(o => o.FechaIngreso)
            .HasConversion(f => f.Value, value => new FechaHistoricaVO(value))
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(o => o.FechaEntregaEstimada)
            .HasConversion(f => f.Value, value => new FechaHistoricaVO(value))
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(m => m.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(m => m.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Relaciones
        builder.HasOne(o => o.Vehiculo)
            .WithMany(v => v.OrdenesServicio)
            .HasForeignKey(o => o.VehiculoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Mecanico)
            .WithMany(m => m.OrdenesServicio)
            .HasForeignKey(o => o.MecanicoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Recepcionista)
            .WithMany(m => m.OrdenesServicio)
            .HasForeignKey(o => o.RecepcionistaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.TipoServicio)
            .WithMany()
            .HasForeignKey(o => o.TipoServicioId);

        builder.HasOne(o => o.Estado)
            .WithMany()
            .HasForeignKey(o => o.EstadoId);

        builder.HasMany(o => o.Detalles)
            .WithOne(d => d.OrdenServicio)
            .HasForeignKey(d => d.OrdenServicioId);

        builder.HasMany(o => o.Facturas)
            .WithOne(f => f.OrdenServicio)
            .HasForeignKey(f => f.OrdenServicioId);
    }
}
