using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class CitaService : ICitaService
{
    private readonly IUnitOfWork _unitOfWork;

    public CitaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Cita?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.Citas.GetByIdAsync(id, ct);

    public async Task<IReadOnlyList<Cita>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.Citas.GetAllAsync(ct);

    public async Task<IReadOnlyList<Cita>> GetByClienteIdAsync(IdVO clienteId, CancellationToken ct = default)
        => await _unitOfWork.Citas.GetByClienteIdAsync(clienteId, ct);

    public async Task<IReadOnlyList<Cita>> GetByVehiculoIdAsync(IdVO vehiculoId, CancellationToken ct = default)
        => await _unitOfWork.Citas.GetByVehiculoIdAsync(vehiculoId, ct);

    public async Task<int> AddAsync(Cita cita, CancellationToken ct = default)
    {
        // Validación: la fecha debe ser futura
        if (cita.FechaCita.Value <= DateTime.UtcNow)
            throw new Exception("La fecha de la cita debe ser futura.");

        var result = await _unitOfWork.Citas.AddAsync(cita, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<bool> UpdateAsync(Cita cita, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Citas.GetByIdAsync(cita.Id, ct);
        if (existing == null)
            return false;

        if (cita.FechaCita.Value <= DateTime.UtcNow)
            throw new Exception("La fecha de la cita debe ser futura.");

        var updated = await _unitOfWork.Citas.UpdateAsync(cita, ct);
        if (updated)
            await _unitOfWork.SaveChanges(ct);

        return updated;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Citas.GetByIdAsync(id, ct);
        if (existing == null)
            return false;

        var deleted = await _unitOfWork.Citas.DeleteAsync(id, ct);
        if (deleted)
            await _unitOfWork.SaveChanges(ct);

        return deleted;
    }
}
