using Domain.ValueObjects;
using MediatR;

namespace Application.HistorialesInventario;

public sealed record CreateHistorialInventario(
    IdVO RepuestoId,
    IdVO? AdminId,
    IdVO TipoMovimientoId,
    CantidadVO Cantidad,
    FechaHistoricaVO FechaMovimiento,
    DescripcionVO? Observaciones
) : IRequest<IdVO>;
