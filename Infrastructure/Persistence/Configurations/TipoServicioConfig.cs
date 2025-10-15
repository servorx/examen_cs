using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TipoServicioConfig : IEntityTypeConfiguration<TipoServicio>
{
    public void Configure(EntityTypeBuilder<TipoServicio> builder)
    {
        builder.ToTable("tipos_servicio");

        builder.HasKey(ts => ts.Id);
        builder.Property(ts => ts.Id)
            .HasConversion(id => id.Value, value => new IdVO(value))
            .HasColumnName("id")
            .UseIdentityByDefaultColumn();


        builder.Property(ts => ts.Nombre)
            .HasConversion(n => n.Value, value => new NombreVO(value))
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ts => ts.Descripcion)
            .HasConversion(d => d.Value, value => new DescripcionVO(value))
            .HasColumnName("descripcion")
            .HasMaxLength(255);

        builder.Property(ts => ts.PrecioBase)
            .HasConversion(p => p.Value, value => new DineroVO(value))
            .HasColumnName("precio_base")
            .HasColumnType("decimal(10,2)")
            .IsRequired();
    }
}
