using Domain.ValueObjects;
using MediatR;

namespace Application.Facturas;

public sealed record CreateFactura(
    IdVO OrdenServicioId,
    DineroVO MontoRepuestos,
    DineroVO ManoObra,
    DineroVO Total,
    FechaHistoricaVO FechaGeneracion
) : IRequest<IdVO>;
