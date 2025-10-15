using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Proveedores;

public class UpdateProveedorHandler : IRequestHandler<UpdateProveedor, bool>
{
    private readonly IProveedorRepository _repository;

    public UpdateProveedorHandler(IProveedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateProveedor request, CancellationToken cancellationToken)
    {
        var proveedor = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (proveedor is null)
            return false;

        proveedor.Nombre = request.Nombre;
        proveedor.Telefono = request.Telefono;
        proveedor.Correo = request.Correo;
        proveedor.Direccion = request.Direccion;
        proveedor.IsActive = request.IsActive;

        return await _repository.UpdateAsync(proveedor, cancellationToken);
    }
}
