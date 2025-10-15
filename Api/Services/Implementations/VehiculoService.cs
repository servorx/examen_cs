using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class VehiculoService : IVehiculoService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehiculoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Vehiculo?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.Vehiculos.GetByIdAsync(id, ct);

    public async Task<IReadOnlyList<Vehiculo>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.Vehiculos.GetAllAsync(ct);

    public async Task<IReadOnlyList<Vehiculo>> GetByClienteIdAsync(IdVO clienteId, CancellationToken ct = default)
        => await _unitOfWork.Vehiculos.GetByClienteIdAsync(clienteId, ct);

    public async Task<int> AddAsync(Vehiculo vehiculo, CancellationToken ct = default)
    {
        // Validar VIN único
        if (await _unitOfWork.Vehiculos.ExistsByVinAsync(vehiculo.Vin, ct))
            throw new Exception($"Ya existe un vehículo con VIN '{vehiculo.Vin.Value}'");

        var result = await _unitOfWork.Vehiculos.AddAsync(vehiculo, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<bool> UpdateAsync(Vehiculo vehiculo, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Vehiculos.GetByIdAsync(vehiculo.Id, ct);
        if (existing == null)
            return false;

        // Validar VIN único si se modificó
        if (existing.Vin.Value != vehiculo.Vin.Value &&
            await _unitOfWork.Vehiculos.ExistsByVinAsync(vehiculo.Vin, ct))
        {
            throw new Exception($"Ya existe un vehículo con VIN '{vehiculo.Vin.Value}'");
        }

        var result = await _unitOfWork.Vehiculos.UpdateAsync(vehiculo, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Vehiculos.GetByIdAsync(id, ct);
        if (existing == null)
            return false;

        var result = await _unitOfWork.Vehiculos.DeleteAsync(id, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<bool> ExistsByVinAsync(VinVO vin, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Vehiculos.GetByVinAsync(vin, ct);
        if (existing == null)
            return false;

        var result = await _unitOfWork.Vehiculos.ExistsByVinAsync(vin, ct);
        return result;
    }
}
