namespace Api.DTOs.Pagos;

public record PagoDto(
    int Id,
    int FacturaId,
    int MetodoPagoId,
    int EstadoPagoId,
    decimal Monto,
    DateTime FechaPago
);
