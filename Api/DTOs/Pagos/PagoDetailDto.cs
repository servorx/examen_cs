using Api.DTOs.Facturas;
using Api.DTOs.MetodosPago;
using Api.DTOs.EstadosPago;

namespace Api.DTOs.Pagos;

public record PagoDetailDto(
    int Id,
    decimal Monto,
    DateTime FechaPago,
    FacturaDto Factura,
    MetodoPagoDto MetodoPago,
    EstadoPagoDto EstadoPago
);
