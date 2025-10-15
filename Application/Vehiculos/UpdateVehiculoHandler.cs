using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Vehiculos;

public class UpdateVehiculoHandler : IRequestHandler<UpdateVehiculo, bool>
{
    private readonly IVehiculoRepository _repository;
    private readonly IClienteRepository _clienteRepository;

    public UpdateVehiculoHandler(IVehiculoRepository repository, IClienteRepository clienteRepository)
    {
        _repository = repository;
        _clienteRepository = clienteRepository;
    }

    public async Task<bool> Handle(UpdateVehiculo request, CancellationToken cancellationToken)
    {
        var vehiculo = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (vehiculo is null)
            return false;

        var cliente = await _clienteRepository.GetByIdAsync(request.ClienteId, cancellationToken);
        if (cliente is null)
            throw new Exception("Cliente no encontrado.");

        vehiculo.Cliente = cliente;
        vehiculo.Marca = request.Marca;
        vehiculo.Modelo = request.Modelo;
        vehiculo.Anio = request.Anio;
        vehiculo.Vin = request.Vin;
        vehiculo.Kilometraje = request.Kilometraje;

        return await _repository.UpdateAsync(vehiculo, cancellationToken);
    }
}
