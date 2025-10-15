using Domain.ValueObjects;
using MediatR;

namespace Application.Pagos;

public sealed record CreatePago(
    IdVO FacturaId,
    IdVO MetodoPagoId,
    IdVO EstadoPagoId,
    DineroVO Monto,
    FechaHistoricaVO FechaPago
) : IRequest<IdVO>;
