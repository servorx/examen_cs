using Domain.ValueObjects;
using MediatR;

namespace Application.DetalleOrdenes;

public sealed record CreateDetalleOrden(
    IdVO OrdenServicioId,
    IdVO RepuestoId,
    CantidadVO Cantidad,
    DineroVO Costo
    // aqui el request se pasa como tupla ya que es una llave compuesta
) : IRequest<(IdVO OrdenServicioId, IdVO RepuestoId)>;
