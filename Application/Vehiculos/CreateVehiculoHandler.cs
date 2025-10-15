using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Vehiculos;

public class CreateVehiculoHandler : IRequestHandler<CreateVehiculo, IdVO>
{
    private readonly IVehiculoRepository _repository;
    private readonly IClienteRepository _clienteRepository;

    public CreateVehiculoHandler(IVehiculoRepository repository, IClienteRepository clienteRepository)
    {
        _repository = repository;
        _clienteRepository = clienteRepository;
    }

    public async Task<IdVO> Handle(CreateVehiculo request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId, cancellationToken);
        if (cliente is null)
            throw new Exception("Cliente no encontrado.");

        var vehiculo = new Vehiculo(
            id: IdVO.CreateNew(),
            cliente: cliente,
            marca: request.Marca,
            modelo: request.Modelo,
            anio: request.Anio,
            vin: request.Vin,
            kilometraje: request.Kilometraje
        );

        await _repository.AddAsync(vehiculo, cancellationToken);
        return vehiculo.Id;
    }
}
