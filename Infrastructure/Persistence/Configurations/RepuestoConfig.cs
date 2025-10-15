using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RepuestoConfiguration : IEntityTypeConfiguration<Repuesto>
{
    public void Configure(EntityTypeBuilder<Repuesto> builder)
    {
        builder.ToTable("repuestos");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();


        builder.Property(r => r.Codigo)
            .HasConversion(c => c.Value, value => new CodigoRepuestoVO(value))
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(r => r.Codigo).IsUnique();

        builder.Property(r => r.Descripcion)
            .HasConversion(d => d.Value, value => new DescripcionVO(value))
            .HasColumnName("descripcion")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(r => r.CantidadStock)
            .HasConversion(c => c.Value, value => new CantidadVO(value))
            .HasColumnName("cantidad_stock")
            .IsRequired();

        builder.Property(r => r.PrecioUnitario)
            .HasConversion(p => p.Value, value => new DineroVO(value))
            .HasColumnName("precio_unitario")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.Property(r => r.ProveedorId)
            .HasConversion(id => id != null ? id.Value : default, value => new IdVO(value))
            .HasColumnName("proveedor_id");
        
        // agregar las columnas del base entity
        builder.Property(v => v.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(v => v.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(r => r.Proveedor)
            .WithMany(p => p.Repuestos)
            .HasForeignKey(r => r.ProveedorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
