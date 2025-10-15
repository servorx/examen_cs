using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EstadosPago;

public class UpdateEstadoPagoHandler : IRequestHandler<UpdateEstadoPago, bool>
{
    private readonly IEstadoPagoRepository _repository;

    public UpdateEstadoPagoHandler(IEstadoPagoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateEstadoPago request, CancellationToken cancellationToken)
    {
        var estado = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (estado is null)
            return false;

        estado.Nombre = request.Nombre;

        return await _repository.UpdateAsync(estado, cancellationToken);
    }
}
