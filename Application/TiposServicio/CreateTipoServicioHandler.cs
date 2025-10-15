using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.TipoServicios;

public class CreateTipoServicioHandler : IRequestHandler<CreateTipoServicio, IdVO>
{
    private readonly ITipoServicioRepository _repository;

    public CreateTipoServicioHandler(ITipoServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateTipoServicio request, CancellationToken cancellationToken)
    {
        var tipoServicio = new TipoServicio(
            id: IdVO.CreateNew(),
            nombre: request.Nombre,
            descripcion: request.Descripcion,
            precioBase: request.PrecioBase
        );

        await _repository.AddAsync(tipoServicio, cancellationToken);
        return tipoServicio.Id;
    }
}
