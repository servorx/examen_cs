using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.MetodosPago;

public class UpdateMetodoPagoHandler : IRequestHandler<UpdateMetodoPago, bool>
{
    private readonly IMetodoPagoRepository _repository;

    public UpdateMetodoPagoHandler(IMetodoPagoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateMetodoPago request, CancellationToken cancellationToken)
    {
        var metodoPago = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (metodoPago is null)
            return false;

        metodoPago.Nombre = request.Nombre;
        return await _repository.UpdateAsync(metodoPago, cancellationToken);
    }
}
