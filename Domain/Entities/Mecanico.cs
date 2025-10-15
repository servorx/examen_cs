using Domain.Entities.Auth;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Mecanico : BaseEntity
{
    public IdVO Id { get; set; } = null!;
    public NombreVO Nombre { get; set; } = null!;
    public TelefonoVO? Telefono { get; set; }
    public EspecialidadVO? Especialidad { get; set; }
    // valor por defecto true
    public EstadoVO IsActive { get; set; } = new EstadoVO(true);
    // relaciones
    // Clave foránea real (int)
    public int UserId { get; set; }
    public UserMember User { get; set; } = null!;
    // relacion uno a muchos
    public ICollection<OrdenServicio> OrdenesServicio { get; set; } = new List<OrdenServicio>();
    // constructores
    public Mecanico()
    {
        IsActive = new EstadoVO(true); // garantiza que nunca será null
    }
    public Mecanico(IdVO id, NombreVO nombre, TelefonoVO? telefono, EspecialidadVO? especialidad, EstadoVO? isActive, int userId)
    {
        Id = id;
        Nombre = nombre;
        Telefono = telefono;
        Especialidad = especialidad;
        IsActive = isActive ?? new EstadoVO(true); // valor por defecto si no se pasa
        UserId = userId;
    }
}
