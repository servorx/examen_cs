using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Administradores
{
    public class CreateAdministradorHandler : IRequestHandler<CreateAdministrador, IdVO>
    {
        private readonly IAdministradorRepository _repository;

        public CreateAdministradorHandler(IAdministradorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IdVO> Handle(CreateAdministrador request, CancellationToken cancellationToken)
        {
            var administrador = new Administrador(
                id: IdVO.CreateNew(),
                nombre: request.Nombre,
                telefono: request.Telefono,
                nivelAcceso: request.NivelAcceso,
                areaResponsabilidad: request.AreaResponsabilidad,
                isActive: request.IsActive,
                userId: request.UserId
            );

            await _repository.AddAsync(administrador, cancellationToken);
            return administrador.Id;
        }
    }
}
