using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Citas;

public class CreateCitaHandler : IRequestHandler<CreateCita, IdVO>
{
    private readonly ICitaRepository _repository;

    public CreateCitaHandler(ICitaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateCita request, CancellationToken cancellationToken)
    {
        var cita = new Cita(
            clienteId: request.ClienteId,
            vehiculoId: request.VehiculoId,
            fechaCita: request.FechaCita,
            motivo: request.Motivo,
            estadoId: request.EstadoId
        );

        await _repository.AddAsync(cita, cancellationToken);
        return cita.Id;
    }
}
