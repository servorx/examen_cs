using Domain.ValueObjects;
using MediatR;

namespace Application.Pagos;

public sealed record UpdatePago(
    IdVO Id,
    IdVO FacturaId,
    IdVO MetodoPagoId,
    IdVO EstadoPagoId,
    DineroVO Monto,
    FechaHistoricaVO FechaPago
) : IRequest<bool>;
