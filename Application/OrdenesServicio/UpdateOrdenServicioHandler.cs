using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.OrdenesServicio;

public class UpdateOrdenServicioHandler : IRequestHandler<UpdateOrdenServicio, bool>
{
    private readonly IOrdenServicioRepository _repository;

    public UpdateOrdenServicioHandler(IOrdenServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateOrdenServicio request, CancellationToken cancellationToken)
    {
        var orden = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (orden is null)
            return false;

        orden.Vehiculo = new Vehiculo { Id = request.VehiculoId };
        orden.Mecanico = new Mecanico { Id = request.MecanicoId };
        orden.TipoServicio = new TipoServicio { Id = request.TipoServicioId };
        orden.Estado = new EstadoOrden { Id = request.EstadoId };
        orden.FechaIngreso = request.FechaIngreso;
        orden.FechaEntregaEstimada = request.FechaEntregaEstimada;

        return await _repository.UpdateAsync(orden, cancellationToken);
    }
}
