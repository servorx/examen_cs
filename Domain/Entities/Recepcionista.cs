using Domain.Entities.Auth;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Recepcionista : BaseEntity
{
    public IdVO Id { get; set; } = null!;
    public NombreVO Nombre { get; set; } = null!;
    public TelefonoVO Telefono { get; set; } = null!;
    public AnioExperienciaVO AnioExperiencia { get; set; } = null!;
    // valor por defecto true
    public EstadoVO IsActive { get; set; } = new EstadoVO(true);
    // relaciones
    // Clave foránea real (int)
    public int UserId { get; set; }
    public UserMember User { get; set; } = null!;
    // relacion uno a muchos
    public ICollection<OrdenServicio> OrdenesServicio { get; set; } = new List<OrdenServicio>();
    // constructores
    public Recepcionista() {
        IsActive = new EstadoVO(true); // garantiza que nunca será null
    }
    public Recepcionista(IdVO id, NombreVO nombre, TelefonoVO telefono, AnioExperienciaVO anioExperiencia, int userId)
    {
        Id = id;
        Nombre = nombre;
        Telefono = telefono;
        UserId = userId;
        AnioExperiencia = anioExperiencia;
    }
}
