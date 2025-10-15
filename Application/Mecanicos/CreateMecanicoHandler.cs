using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Mecanicos;

public class CreateMecanicoHandler : IRequestHandler<CreateMecanico, IdVO>
{
    private readonly IMecanicoRepository _repository;

    public CreateMecanicoHandler(IMecanicoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateMecanico request, CancellationToken cancellationToken)
    {
        var mecanico = new Mecanico(
            id: IdVO.CreateNew(),
            nombre: request.Nombre,
            telefono: request.Telefono,
            especialidad: request.Especialidad,
            isActive: request.IsActive,
            userId: request.UserId
        );

        await _repository.AddAsync(mecanico, cancellationToken);
        return mecanico.Id;
    }
}
