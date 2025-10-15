using Application.Abstractions;
using MediatR;

namespace Application.Clientes;

public class UpdateClienteHandler : IRequestHandler<UpdateCliente, bool>
{
    private readonly IClienteRepository _repository;

    public UpdateClienteHandler(IClienteRepository repository) =>_repository = repository;

    public async Task<bool> Handle(UpdateCliente request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (cliente is null)
            return false;

        cliente.Nombre = request.Nombre;
        cliente.Correo = request.Correo;
        cliente.Telefono = request.Telefono;
        cliente.Direccion = request.Direccion;
        cliente.IsActive = request.IsActive;

        return await _repository.UpdateAsync(cliente, cancellationToken);
    }
}
