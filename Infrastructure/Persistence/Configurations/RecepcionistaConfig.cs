using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RecepcionistaConfig : IEntityTypeConfiguration<Recepcionista>
{
    public void Configure(EntityTypeBuilder<Recepcionista> builder)
    {
        builder.ToTable("recepcionistas");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(v => v.Value, v => new IdVO(v))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();


        builder.Property(m => m.Nombre)
            .HasConversion(
                nombre => nombre.Value,
                value => new NombreVO(value)
            )
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Telefono)
            .HasConversion(
                t => t == null ? null : t.Value,
                value => new TelefonoVO(value)
            )
            .HasColumnName("telefono")
            .HasMaxLength(20);

        builder.Property(m => m.AnioExperiencia)
            .HasConversion(ae => ae.Value, value => new AnioExperienciaVO(value))
            .HasColumnName("especialidad")
            .HasMaxLength(60);

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

        builder.HasOne(m => m.User)
            .WithOne()
            .HasForeignKey<Recepcionista>(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
