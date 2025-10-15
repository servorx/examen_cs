using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.TipoMovimientos;

public class CreateTipoMovimientoHandler : IRequestHandler<CreateTipoMovimiento, IdVO>
{
    private readonly ITipoMovimientoRepository _repository;

    public CreateTipoMovimientoHandler(ITipoMovimientoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateTipoMovimiento request, CancellationToken cancellationToken)
    {
        var tipoMovimiento = new TipoMovimiento(
            id: IdVO.CreateNew(),
            nombre: request.Nombre
        );

        await _repository.AddAsync(tipoMovimiento, cancellationToken);
        return tipoMovimiento.Id;
    }
}
