using Domain.ValueObjects;
using MediatR;

namespace Application.Facturas;

public sealed record UpdateFactura(
    IdVO Id,
    IdVO OrdenServicioId,
    DineroVO MontoRepuestos,
    DineroVO ManoObra,
    DineroVO Total,
    FechaHistoricaVO FechaGeneracion
) : IRequest<bool>;
