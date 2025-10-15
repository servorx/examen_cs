using Domain.ValueObjects;

namespace Domain.Entities;

public class TipoServicio
{
    public IdVO Id { get; set; } = null!;
    public NombreVO Nombre { get; set; } = null!;
    public DescripcionVO Descripcion { get; set; } = null!;
    public DineroVO PrecioBase { get; set; } = null!;

    public ICollection<OrdenServicio> OrdenesServicio { get; set; } = new List<OrdenServicio>();

    // constructores
    public TipoServicio() { }
    public TipoServicio(IdVO id, NombreVO nombre, DescripcionVO descripcion, DineroVO precioBase)
    {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
        PrecioBase = precioBase;
    }
}
