using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.TipoMovimientos;

public class UpdateTipoMovimientoHandler : IRequestHandler<UpdateTipoMovimiento, bool>
{
    private readonly ITipoMovimientoRepository _repository;

    public UpdateTipoMovimientoHandler(ITipoMovimientoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateTipoMovimiento request, CancellationToken cancellationToken)
    {
        var tipoMovimiento = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (tipoMovimiento is null)
            return false;

        tipoMovimiento.Nombre = request.Nombre;

        return await _repository.UpdateAsync(tipoMovimiento, cancellationToken);
    }
}
