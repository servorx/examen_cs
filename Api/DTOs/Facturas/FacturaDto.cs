namespace Api.DTOs.Facturas;

public record FacturaDto(
    int Id,
    int OrdenServicioId,
    decimal MontoRepuestos,
    decimal ManoObra,
    decimal Total,
    DateTime FechaGeneracion
);
