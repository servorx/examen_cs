using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Auth;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Proveedor : BaseEntity
{
    public IdVO Id { get; set; } = null!;
    public NombreVO Nombre { get; set; } = null!;
    public TelefonoVO? Telefono { get; set; }
    public CorreoVO? Correo { get; set; }
    public DireccionVO? Direccion { get; set; }

    public EstadoVO IsActive { get; set; } = new EstadoVO(true);

    // 👇 propiedad real mapeable para EF
    public bool IsActiveBool
    {
        get => IsActive.Value;
        private set => IsActive = new EstadoVO(value);
    }

    public int UserId { get; set; }
    public UserMember User { get; set; } = null!;
    public ICollection<Repuesto> Repuestos { get; set; } = new List<Repuesto>();

    public Proveedor() { }

    public Proveedor(IdVO id, NombreVO nombre, TelefonoVO? telefono, CorreoVO? correo,
                     DireccionVO? direccion, EstadoVO? isActive, int userId)
    {
        Id = id;
        Nombre = nombre;
        Telefono = telefono;
        Correo = correo;
        Direccion = direccion;
        IsActive = isActive ?? new EstadoVO(true);
        UserId = userId;
    }
}


