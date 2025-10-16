using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Recepcionistas;
public class CreateRecepcionistaHandler : IRequestHandler<CreateRecepcionista, IdVO>
{
    private readonly IRecepcionistaRepository _repository;

    public CreateRecepcionistaHandler(IRecepcionistaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateRecepcionista request, CancellationToken cancellationToken)
    {
        var recepcionista = new Recepcionista(
            id: IdVO.CreateNew(),
            nombre: request.Nombre,
            telefono: request.Telefono,
            anioExperiencia: request.AnioExperiencia,
            userId: request.UserId
        );

        await _repository.AddAsync(recepcionista, cancellationToken);
        return recepcionista.Id;
    }
}

