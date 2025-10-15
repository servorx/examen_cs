namespace Api.DTOs.Pagos;

public record CreatePagoDto(
    int FacturaId,
    int MetodoPagoId,
    int EstadoPagoId,
    decimal Monto,
    DateTime FechaPago
);
