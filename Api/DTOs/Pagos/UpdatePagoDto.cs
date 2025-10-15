namespace Api.DTOs.Pagos;

public record UpdatePagoDto(
    int FacturaId,
    int MetodoPagoId,
    int EstadoPagoId,
    decimal Monto,
    DateTime FechaPago
);
