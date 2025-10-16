using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Recepcionistas;
public class UpdateRecepcionistaHandler : IRequestHandler<UpdateRecepcionista, bool>
{
    private readonly IRecepcionistaRepository _repository;

    public UpdateRecepcionistaHandler(IRecepcionistaRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateRecepcionista request, CancellationToken cancellationToken)
    {
        var recepcionista = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (recepcionista == null) return false;

        recepcionista.Nombre = request.Nombre;
        recepcionista.Telefono = request.Telefono;
        recepcionista.AnioExperiencia = request.AnioExperiencia;
        recepcionista.IsActive = request.IsActive;

        return await _repository.UpdateAsync(recepcionista, cancellationToken);
    }
}
