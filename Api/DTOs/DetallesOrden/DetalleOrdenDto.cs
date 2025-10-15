namespace Api.DTOs.DetallesOrden;

public sealed record DetalleOrdenDto
{
    // se tiene que realizar con un constructor vacío para que se pueda crear una instancia de DTO 
    public int Id { get; init; }
    public int OrdenServicioId { get; init; }
    public int RepuestoId { get; init; }
    public int Cantidad { get; init; }
    public decimal Costo { get; init; }

    public DetalleOrdenDto() { } // 👈 Necesario
}
