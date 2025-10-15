using Domain.ValueObjects;

namespace Domain.Entities;

public class MetodoPago
{
    public IdVO Id { get; set; } = null!;
    public NombreVO Nombre { get; set; } = null!;
    // constructores
    public MetodoPago() { }
    public MetodoPago(IdVO id, NombreVO nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}
