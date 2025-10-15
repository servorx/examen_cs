using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.EstadosOrden;

public class CreateEstadoOrdenHandler : IRequestHandler<CreateEstadoOrden, IdVO>
{
    private readonly IEstadoOrdenRepository _repository;

    public CreateEstadoOrdenHandler(IEstadoOrdenRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateEstadoOrden request, CancellationToken cancellationToken)
    {
        var estado = new EstadoOrden(
            id: IdVO.CreateNew(),
            nombre: request.Nombre
        );

        await _repository.AddAsync(estado, cancellationToken);
        return estado.Id;
    }
}
