using Application.Abstractions;
using MediatR;

namespace Application.Citas;

public class UpdateCitaHandler : IRequestHandler<UpdateCita, bool>
{
    private readonly ICitaRepository _repository;

    public UpdateCitaHandler(ICitaRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateCita request, CancellationToken cancellationToken)
    {
        var cita = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (cita is null)
            return false;

        cita.ClienteId = request.ClienteId;
        cita.VehiculoId = request.VehiculoId;
        cita.FechaCita = request.FechaCita;
        cita.Motivo = request.Motivo;
        cita.EstadoId = request.EstadoId;

        return await _repository.UpdateAsync(cita, cancellationToken);
    }
}
