
using Application.Abstractions;
using MediatR;

namespace Application.EstadosCita;

public class UpdateEstadoCitaHandler : IRequestHandler<UpdateEstadoCita, bool>
{
    private readonly IEstadoCitaRepository _repository;

    public UpdateEstadoCitaHandler(IEstadoCitaRepository repository) => _repository = repository;
    public async Task<bool> Handle(UpdateEstadoCita request, CancellationToken cancellationToken)
    {
        var estado = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (estado is null)
            return false;

        estado.Nombre = request.Nombre;

        return await _repository.UpdateAsync(estado, cancellationToken);
    }
}
