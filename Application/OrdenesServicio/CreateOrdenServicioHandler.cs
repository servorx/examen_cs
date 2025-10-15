using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.OrdenesServicio;

public class CreateOrdenServicioHandler : IRequestHandler<CreateOrdenServicio, IdVO>
{
    private readonly IOrdenServicioRepository _repository;

    public CreateOrdenServicioHandler(IOrdenServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateOrdenServicio request, CancellationToken cancellationToken)
    {
        var orden = new OrdenServicio(
            id: IdVO.CreateNew(),
            vehiculo: new Vehiculo { Id = request.VehiculoId }, // solo el Id necesario
            mecanico: new Mecanico { Id = request.MecanicoId },
            tipoServicio: new TipoServicio { Id = request.TipoServicioId },
            estado: new EstadoOrden { Id = request.EstadoId },
            fechaIngreso: request.FechaIngreso,
            fechaEntregaEstimada: request.FechaEntregaEstimada
        );

        await _repository.AddAsync(orden, cancellationToken);
        return orden.Id;
    }
}
