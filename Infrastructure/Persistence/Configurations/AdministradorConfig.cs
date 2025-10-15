using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class AdministradorConfig : IEntityTypeConfiguration<Administrador>
{
    public void Configure(EntityTypeBuilder<Administrador> builder)
    {
        builder.ToTable("administradores");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(v => v.Value, value => new IdVO(value))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();

        builder.Property(a => a.Nombre)
            .HasConversion(v => v.Value, v => new NombreVO(v))
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Telefono)
            .HasConversion(v => v.Value, v => new TelefonoVO(v))
            .HasColumnName("telefono");

        builder.Property(a => a.NivelAcceso)
            .HasConversion(v => v.Value, v => new NivelAccesoVO(v))
            .HasColumnName("nivel_acceso");

        builder.Property(a => a.AreaResponsabilidad)
            .HasConversion(v => v.Value, v => new DescripcionVO(v))
            .HasColumnName("area_responsabilidad");

        builder.Property(a => a.IsActive)
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

        builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Administrador>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
