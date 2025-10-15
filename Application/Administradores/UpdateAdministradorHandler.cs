using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Administradores;
public class UpdateAdministradorHandler : IRequestHandler<UpdateAdministrador, bool>
{
    private readonly IAdministradorRepository _repository;

    public UpdateAdministradorHandler(IAdministradorRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateAdministrador request, CancellationToken cancellationToken)
    {
        var administrador = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (administrador == null) return false;

        administrador.Nombre = request.Nombre;
        administrador.Telefono = request.Telefono;
        administrador.NivelAcceso = request.NivelAcceso;
        administrador.AreaResponsabilidad = request.AreaResponsabilidad;
        administrador.IsActive = request.IsActive;

        return await _repository.UpdateAsync(administrador, cancellationToken);
    }
}
