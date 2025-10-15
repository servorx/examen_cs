using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.EstadosPago;

public class CreateEstadoPagoHandler : IRequestHandler<CreateEstadoPago, IdVO>
{
    private readonly IEstadoPagoRepository _repository;

    public CreateEstadoPagoHandler(IEstadoPagoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateEstadoPago request, CancellationToken cancellationToken)
    {
        var estado = new EstadoPago(
            id: IdVO.CreateNew(),
            nombre: request.Nombre
        );

        await _repository.AddAsync(estado, cancellationToken);
        return estado.Id;
    }
}
