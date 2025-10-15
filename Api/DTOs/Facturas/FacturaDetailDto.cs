using Api.DTOs.OrdenesServicio;
using Api.DTOs.Pagos;

namespace Api.DTOs.Facturas;

public sealed record FacturaDetailDto(
    int Id,
    int OrdenServicioId,
    decimal MontoRepuestos,
    decimal ManoObra,
    decimal Total,
    DateTime FechaGeneracion,
    OrdenServicioDto OrdenServicio,
    IReadOnlyList<PagoDto> Pagos
);
