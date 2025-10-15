namespace Api.DTOs.DetallesOrden;

public sealed record CreateDetalleOrdenDto(
    int OrdenServicioId,
    int RepuestoId,
    int Cantidad,
    decimal Costo
);
