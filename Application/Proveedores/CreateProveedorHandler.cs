using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Proveedores;

public class CreateProveedorHandler : IRequestHandler<CreateProveedor, IdVO>
{
    private readonly IProveedorRepository _repository;

    public CreateProveedorHandler(IProveedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateProveedor request, CancellationToken cancellationToken)
    {
        var proveedor = new Proveedor(
            id: IdVO.CreateNew(),
            nombre: request.Nombre,
            telefono: request.Telefono,
            correo: request.Correo,
            direccion: request.Direccion,
            isActive: request.IsActive,
            userId: request.UserId
        );

        await _repository.AddAsync(proveedor, cancellationToken);
        return proveedor.Id;
    }
}
