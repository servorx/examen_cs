using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.TipoServicios;

public class UpdateTipoServicioHandler : IRequestHandler<UpdateTipoServicio, bool>
{
    private readonly ITipoServicioRepository _repository;

    public UpdateTipoServicioHandler(ITipoServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateTipoServicio request, CancellationToken cancellationToken)
    {
        var tipoServicio = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (tipoServicio is null)
            return false;

        tipoServicio.Nombre = request.Nombre;
        tipoServicio.Descripcion = request.Descripcion;
        tipoServicio.PrecioBase = request.PrecioBase;

        return await _repository.UpdateAsync(tipoServicio, cancellationToken);
    }
}
