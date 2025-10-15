using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("clientes");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(v => v.Value, v => new IdVO(v))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();


        builder.Property(c => c.Nombre)
            .HasConversion(v => v.Value, v => new NombreVO(v))
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Correo)
            .HasConversion(v => v.Value, v => new CorreoVO(v))
            .HasColumnName("correo")
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(c => c.Telefono)
            .HasConversion(v => v.Value, v => new TelefonoVO(v))
            .HasColumnName("telefono")
            .HasMaxLength(20);

        builder.Property(c => c.Direccion)
            .HasConversion(v => v.Value, v => new DireccionVO(v))
            .HasColumnName("direccion")
            .HasMaxLength(255);

        builder.Property(c => c.IsActive)
            .HasConversion(v => v.Value, v => new EstadoVO(v))
            .HasColumnName("is_active")
            .IsRequired();
        
        builder.Property(m => m.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(m => m.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // FK explicita por el modelo de usuario
        builder.Property(a => a.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.HasOne(c => c.User)
            .WithOne()
            .HasForeignKey<Cliente>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
