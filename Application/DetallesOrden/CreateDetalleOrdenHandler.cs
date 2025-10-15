using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.DetalleOrdenes;

public class CreateDetalleOrdenHandler : IRequestHandler<CreateDetalleOrden, (IdVO OrdenServicioId, IdVO RepuestoId)>
{
    private readonly IDetalleOrdenRepository _repository;

    public CreateDetalleOrdenHandler(IDetalleOrdenRepository repository)
    {
        _repository = repository;
    }

    public async Task<(IdVO OrdenServicioId, IdVO RepuestoId)> Handle(CreateDetalleOrden request, CancellationToken cancellationToken)
    {
        var detalle = new DetalleOrden(
            ordenServicioId: request.OrdenServicioId,
            repuestoId: request.RepuestoId,
            cantidad: request.Cantidad,
            costo: request.Costo
        );

        await _repository.AddAsync(detalle, cancellationToken);
        return (detalle.OrdenServicioId, detalle.RepuestoId);
    }
}
