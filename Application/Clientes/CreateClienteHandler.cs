
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Clientes;

public class CreateClienteHandler : IRequestHandler<CreateCliente, IdVO>
{
    private readonly IClienteRepository _repository;

    public CreateClienteHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IdVO> Handle(CreateCliente request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente(
            id: IdVO.CreateNew(),
            nombre: request.Nombre,
            correo: request.Correo,
            telefono: request.Telefono,
            direccion: request.Direccion,
            isActive: request.IsActive,
            userId: request.UserId
        );

        await _repository.AddAsync(cliente, cancellationToken);
        return cliente.Id;
    }
}
