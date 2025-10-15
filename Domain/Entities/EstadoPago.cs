using Domain.ValueObjects;

namespace Domain.Entities;

public class EstadoPago
{
    public IdVO Id { get; set; } = null!;
    public NombreVO Nombre { get; set; } = null!;

    public EstadoPago() { }
    public EstadoPago(IdVO id, NombreVO nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}
