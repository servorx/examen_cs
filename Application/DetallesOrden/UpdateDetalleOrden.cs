using Domain.ValueObjects;
using MediatR;

namespace Application.DetalleOrdenes;

public sealed record UpdateDetalleOrden(
    IdVO OrdenServicioId,
    IdVO RepuestoId,
    CantidadVO Cantidad,
    DineroVO Costo
) : IRequest<bool>;
