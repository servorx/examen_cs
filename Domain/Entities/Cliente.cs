using Domain.Entities.Auth;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Cliente : BaseEntity
{
    public IdVO Id { get; set; } = null!;
    public NombreVO Nombre { get; set; } = null!;
    public CorreoVO Correo { get; set; } = null!;
    public TelefonoVO Telefono { get; set; } = null!;
    public DireccionVO Direccion { get; set; } = null!;
    public EstadoVO IsActive { get; set; } = new EstadoVO(true);

    public int UserId { get; set; }
    public UserMember User { get; set; } = null!;

    public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();

    public Cliente()
    {
        IsActive = new EstadoVO(true);
    }

    public Cliente(IdVO id, NombreVO nombre, CorreoVO correo, TelefonoVO telefono, DireccionVO direccion, EstadoVO? isActive, int userId)
    {
        Id = id;
        Nombre = nombre;
        Correo = correo;
        Telefono = telefono;
        Direccion = direccion;
        IsActive = isActive ?? new EstadoVO(true);
        UserId = userId;
    }
}

