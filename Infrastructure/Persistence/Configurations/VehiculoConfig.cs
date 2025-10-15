using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class VehiculoConfig : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        builder.ToTable("vehiculos");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();


        builder.Property(v => v.ClienteId)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .HasColumnName("cliente_id")
            .IsRequired();

        builder.Property(v => v.Marca)
            .HasConversion(m => m.Value, value => new NombreVO(value))
            .HasColumnName("marca")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Modelo)
            .HasConversion(m => m.Value, value => new NombreVO(value))
            .HasColumnName("modelo")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Anio)
            .HasConversion(a => a.Value, value => new AnioVehiculoVO(value))
            .HasColumnName("anio")
            .IsRequired();

        builder.Property(v => v.Vin)
            .HasConversion(vin => vin.Value, value => new VinVO(value))
            .HasColumnName("vin")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(v => v.Kilometraje)
            .HasConversion(k => k.Value, value => new KilometrajeVO(value))
            .HasColumnName("kilometraje")
            .IsRequired();

        // agregar las columnas del base entity
        builder.Property(v => v.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(v => v.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(v => v.Cliente)
            .WithMany(c => c.Vehiculos)
            .HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
