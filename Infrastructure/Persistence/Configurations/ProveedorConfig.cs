using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProveedorConfig : IEntityTypeConfiguration<Proveedor>
{
    public void Configure(EntityTypeBuilder<Proveedor> builder)
    {
        builder.ToTable("proveedores");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,            // al guardar en la BD
                value => new IdVO(value)   // al leer desde la BD
            )
            .HasColumnName("id")
            .UseIdentityAlwaysColumn()
            .IsRequired();


        builder.Property(p => p.Nombre)
            .HasConversion(n => n.Value, value => new NombreVO(value))
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Telefono)
            .HasConversion(
                t => t == null ? null : t.Value,
                value => value == null ? null : new TelefonoVO(value))
            .HasColumnName("telefono")
            .HasMaxLength(20);

        builder.Property(p => p.Correo)
            .HasConversion(
                c => c == null ? null : c.Value,
                value => value == null ? null : new CorreoVO(value))
            .HasColumnName("correo")
            .HasMaxLength(100);

        builder.Property(p => p.Direccion)
            .HasConversion(
                d => d == null ? null : d.Value,
                value => value == null ? null : new DireccionVO(value))
            .HasColumnName("direccion")
            .HasMaxLength(255);

        builder.Property(p => p.IsActive)
            .HasConversion(
                estado => estado.Value,
                value => new EstadoVO(value)
            )
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .HasDefaultValueSql("TRUE") // PostgreSQL lo entiende directamente
            .IsRequired();


        builder.Property(m => m.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(m => m.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(a => a.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<Proveedor>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
