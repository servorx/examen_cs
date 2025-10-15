using Domain.ValueObjects;
using MediatR;

namespace Application.HistorialesInventario;

public sealed record UpdateHistorialInventario(
    IdVO Id,
    IdVO RepuestoId,
    IdVO? AdminId,
    IdVO TipoMovimientoId,
    CantidadVO Cantidad,
    FechaHistoricaVO FechaMovimiento,
    DescripcionVO? Observaciones
) : IRequest<bool>;
