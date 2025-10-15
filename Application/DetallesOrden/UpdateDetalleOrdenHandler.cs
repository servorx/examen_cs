using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.DetalleOrdenes;

public class UpdateDetalleOrdenHandler : IRequestHandler<UpdateDetalleOrden, bool>
{
    private readonly IDetalleOrdenRepository _repository;

    public UpdateDetalleOrdenHandler(IDetalleOrdenRepository repository) =>_repository = repository;

    public async Task<bool> Handle(UpdateDetalleOrden request, CancellationToken cancellationToken)
    {
        var detalle = await _repository.GetByIdsAsync(request.OrdenServicioId, request.RepuestoId, cancellationToken);
        if (detalle is null)
            return false;

        detalle.Cantidad = request.Cantidad;
        detalle.Costo = request.Costo;

        return await _repository.UpdateAsync(detalle, cancellationToken);
    }
}
