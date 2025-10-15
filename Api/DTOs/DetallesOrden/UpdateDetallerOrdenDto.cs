namespace Api.DTOs.DetallesOrden;

public sealed record UpdateDetalleOrdenDto(
    int Cantidad,
    decimal Costo
);
