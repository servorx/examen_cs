using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class MecanicoService : IMecanicoService
{
    private readonly IUnitOfWork _unitOfWork;

    public MecanicoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Mecanico?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.Mecanicos.GetByIdAsync(id, ct);

    public async Task<IReadOnlyList<Mecanico>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.Mecanicos.GetAllAsync(ct);

    public async Task<IReadOnlyList<Mecanico>> GetActiveAsync(CancellationToken ct = default)
    {
        var all = await _unitOfWork.Mecanicos.GetAllAsync(ct);
        return all.Where(m => m.IsActive.Value).ToList();
    }

    public async Task<int> AddAsync(Mecanico mecanico, CancellationToken ct = default)
    {
        // Validación de negocio: nombre único
        if (await _unitOfWork.Mecanicos.ExistsByNombreAsync(mecanico.Nombre, ct))
            throw new Exception($"Ya existe un mecánico con el nombre '{mecanico.Nombre.Value}'");

        var id = await _unitOfWork.Mecanicos.AddAsync(mecanico, ct);
        await _unitOfWork.SaveChanges(ct);

        return id;
    }

    public async Task<bool> UpdateAsync(Mecanico mecanico, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Mecanicos.GetByIdAsync(mecanico.Id, ct);
        if (existing == null)
            return false;

        // Validar nombre único si cambió
        if (existing.Nombre.Value != mecanico.Nombre.Value &&
            await _unitOfWork.Mecanicos.ExistsByNombreAsync(mecanico.Nombre, ct))
        {
            throw new Exception($"Ya existe un mecánico con el nombre '{mecanico.Nombre.Value}'");
        }

        var updated = await _unitOfWork.Mecanicos.UpdateAsync(mecanico, ct);
        await _unitOfWork.SaveChanges(ct);

        return updated;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Mecanicos.GetByIdAsync(id, ct);
        if (existing == null)
            return false;

        var deleted = await _unitOfWork.Mecanicos.DeleteAsync(id, ct);
        await _unitOfWork.SaveChanges(ct);

        return deleted;
    }

    public async Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default)
        => await _unitOfWork.Mecanicos.ExistsByNombreAsync(nombre, ct);
}
