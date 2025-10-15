using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.MetodosPago;

public class CreateMetodoPagoHandler : IRequestHandler<CreateMetodoPago, IdVO>
{
    private readonly IMetodoPagoRepository _repository;

    public CreateMetodoPagoHandler(IMetodoPagoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateMetodoPago request, CancellationToken cancellationToken)
    {
        var metodoPago = new MetodoPago(
            id: IdVO.CreateNew(),
            nombre: request.Nombre
        );

        await _repository.AddAsync(metodoPago, cancellationToken);
        return metodoPago.Id;
    }
}
