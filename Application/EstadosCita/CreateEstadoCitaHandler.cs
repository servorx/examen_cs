
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.EstadosCita;

public class CreateEstadoCitaHandler : IRequestHandler<CreateEstadoCita, IdVO>
{
    private readonly IEstadoCitaRepository _repository;

    public CreateEstadoCitaHandler(IEstadoCitaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateEstadoCita request, CancellationToken cancellationToken)
    {
        var estado = new EstadoCita(
            id: IdVO.CreateNew(),
            nombre: request.Nombre
        );

        await _repository.AddAsync(estado, cancellationToken);
        return estado.Id;
    }
}
