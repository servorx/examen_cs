using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class RecepcionistaService : IRecepcionistaService
{
    private readonly IUnitOfWork _unitOfWork;

    public RecepcionistaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Recepcionista?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.Recepcionistas.GetByIdAsync(id, ct);

    public async Task<IReadOnlyList<Recepcionista>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.Recepcionistas.GetAllAsync(ct);

    public async Task<IReadOnlyList<Recepcionista>> GetActiveAsync(CancellationToken ct = default)
    {
        var all = await _unitOfWork.Recepcionistas.GetAllAsync(ct);
        return all.Where(a => a.IsActive.Value).ToList();
    }

    public async Task<int> AddAsync(Recepcionista recepcionista, CancellationToken ct = default)
    {
        if (await _unitOfWork.Recepcionistas.ExistsByNombreAsync(recepcionista.Nombre, ct))
            throw new Exception($"Ya existe un recepcionista con el nombre '{recepcionista.Nombre.Value}'");

        var result = await _unitOfWork.Recepcionistas.AddAsync(recepcionista, ct);
        await _unitOfWork.SaveChanges(ct); //guardar los cambios

        return result;
    }

    public async Task<bool> UpdateAsync(Recepcionista recepcionista, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Recepcionistas.GetByIdAsync(recepcionista.Id, ct);
        if (existing == null)
            return false;

        // Validar duplicados
        if (existing.Nombre.Value != recepcionista.Nombre.Value &&
            await _unitOfWork.Recepcionistas.ExistsByNombreAsync(recepcionista.Nombre, ct))
        {
            throw new Exception($"Ya existe un recepcionista con el nombre '{recepcionista.Nombre.Value}'");
        }

        var updated = await _unitOfWork.Recepcionistas.UpdateAsync(recepcionista, ct);
        if (updated)
        // guardar los cambios en cada metodo
            await _unitOfWork.SaveChanges(ct);

        return updated;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Recepcionistas.GetByIdAsync(id, ct);
        if (existing == null)
            return false;

        var deleted = await _unitOfWork.Recepcionistas.DeleteAsync(id, ct);
        if (deleted)
            await _unitOfWork.SaveChanges(ct);

        return deleted;
    }

    public async Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default)
        => await _unitOfWork.Recepcionistas.ExistsByNombreAsync(nombre, ct);
}
