using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EstadosOrden;

public class UpdateEstadoOrdenHandler : IRequestHandler<UpdateEstadoOrden, bool>
{
    private readonly IEstadoOrdenRepository _repository;

    public UpdateEstadoOrdenHandler(IEstadoOrdenRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateEstadoOrden request, CancellationToken cancellationToken)
    {
        var estado = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (estado is null)
            return false;

        estado.Nombre = request.Nombre;

        return await _repository.UpdateAsync(estado, cancellationToken);
    }
}
