namespace Api.DTOs.Facturas;

public record UpdateFacturaDto(
    decimal MontoRepuestos,
    decimal ManoObra,
    decimal Total,
    DateTime FechaGeneracion
);
