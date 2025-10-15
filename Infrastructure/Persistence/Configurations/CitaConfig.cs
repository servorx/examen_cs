using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CitaConfig : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        builder.ToTable("citas");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(v => v.Value, v => new IdVO(v))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();

        builder.Property(c => c.ClienteId)
            .HasConversion(v => v.Value, v => new IdVO(v))
            .HasColumnName("cliente_id")
            .IsRequired();

        builder.Property(c => c.VehiculoId)
            .HasConversion(v => v.Value, v => new IdVO(v))
            .HasColumnName("vehiculo_id")
            .IsRequired();

        builder.Property(c => c.FechaCita)
            .HasConversion(v => v.Value, v => new FechaCitaVO(v))
            .HasColumnName("fecha_cita")
            .IsRequired();

        builder.Property(c => c.Motivo)
            .HasConversion(
                v => v == null ? null : v.Value,
                v => v == null ? null : new DescripcionVO(v)
            )
            .HasColumnName("motivo")
            .HasMaxLength(255);

        builder.Property(c => c.EstadoId)
            .HasConversion(v => v.Value, v => new IdVO(v))
            .HasColumnName("estado_id")
            .IsRequired();

        builder.Property(m => m.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(m => m.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(c => c.Cliente)
            .WithMany()
            .HasForeignKey(c => c.ClienteId)
            .HasConstraintName("fk_cita_cliente")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Vehiculo)
            .WithMany()
            .HasForeignKey(c => c.VehiculoId)
            .HasConstraintName("fk_cita_vehiculo")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Estado)
            .WithMany()
            .HasForeignKey(c => c.EstadoId)
            .HasConstraintName("fk_cita_estado");
    }
}
