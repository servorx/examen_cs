using System.Text.Json.Serialization.Metadata;
using Domain.Entities.Auth;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Administrador : BaseEntity
{
    public IdVO Id { get; set; } = null!;
    public NombreVO Nombre { get; set; } = null!;
    public TelefonoVO Telefono { get; set; } = null!;
    public NivelAccesoVO NivelAcceso { get; set; } = null!;
    public DescripcionVO AreaResponsabilidad { get; set; } = null!;
    // valor por defecto true
    public EstadoVO IsActive { get; set; } = new EstadoVO(true);

    // Clave foránea real (int)
    public int UserId { get; set; }
    // Relación con el usuario del sistema (autenticación)
    public UserMember User { get; set; } = null!;
    // constructores 
    // constructor sin parametros
    public Administrador()
    {
        IsActive = new EstadoVO(true); // garantiza que nunca será null
    }
    public Administrador(IdVO id, NombreVO nombre, TelefonoVO telefono, NivelAccesoVO nivelAcceso, DescripcionVO areaResponsabilidad, EstadoVO? isActive, int userId)
    {
        Id = id;
        Nombre = nombre;
        Telefono = telefono;
        NivelAcceso = nivelAcceso;
        AreaResponsabilidad = areaResponsabilidad;
        IsActive = isActive ?? new EstadoVO(true); // valor por defecto si no se pasa
        UserId = userId;
    }
}
