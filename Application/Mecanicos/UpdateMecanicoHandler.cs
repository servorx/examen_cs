using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Mecanicos;

public class UpdateMecanicoHandler : IRequestHandler<UpdateMecanico, bool>
{
    private readonly IMecanicoRepository _repository;

    public UpdateMecanicoHandler(IMecanicoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateMecanico request, CancellationToken cancellationToken)
    {
        var mecanico = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (mecanico is null)
            return false;

        mecanico.Nombre = request.Nombre;
        mecanico.Telefono = request.Telefono;
        mecanico.Especialidad = request.Especialidad;
        mecanico.IsActive = request.IsActive;

        return await _repository.UpdateAsync(mecanico, cancellationToken);
    }
}
