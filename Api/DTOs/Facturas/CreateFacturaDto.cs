namespace Api.DTOs.Facturas;

public record CreateFacturaDto(
    int OrdenServicioId,
    decimal MontoRepuestos,
    decimal ManoObra,
    decimal Total,
    DateTime FechaGeneracion
);
