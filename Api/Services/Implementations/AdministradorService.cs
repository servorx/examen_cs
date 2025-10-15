using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class AdministradorService : IAdministradorService
{
    private readonly IUnitOfWork _unitOfWork;

    public AdministradorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Administrador?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.Admins.GetByIdAsync(id, ct);

    public async Task<IReadOnlyList<Administrador>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.Admins.GetAllAsync(ct);

    public async Task<IReadOnlyList<Administrador>> GetActiveAsync(CancellationToken ct = default)
    {
        var all = await _unitOfWork.Admins.GetAllAsync(ct);
        return all.Where(a => a.IsActive.Value).ToList();
    }

    public async Task<int> AddAsync(Administrador administrador, CancellationToken ct = default)
    {
        if (await _unitOfWork.Admins.ExistsByNombreAsync(administrador.Nombre, ct))
            throw new Exception($"Ya existe un administrador con el nombre '{administrador.Nombre.Value}'");

        var result = await _unitOfWork.Admins.AddAsync(administrador, ct);
        await _unitOfWork.SaveChanges(ct); //guardar los cambios

        return result;
    }

    public async Task<bool> UpdateAsync(Administrador administrador, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Admins.GetByIdAsync(administrador.Id, ct);
        if (existing == null)
            return false;

        // Validar duplicados
        if (existing.Nombre.Value != administrador.Nombre.Value &&
            await _unitOfWork.Admins.ExistsByNombreAsync(administrador.Nombre, ct))
        {
            throw new Exception($"Ya existe un administrador con el nombre '{administrador.Nombre.Value}'");
        }

        var updated = await _unitOfWork.Admins.UpdateAsync(administrador, ct);
        if (updated)
        // guardar los cambios en cada metodo
            await _unitOfWork.SaveChanges(ct);

        return updated;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Admins.GetByIdAsync(id, ct);
        if (existing == null)
            return false;

        var deleted = await _unitOfWork.Admins.DeleteAsync(id, ct);
        if (deleted)
            await _unitOfWork.SaveChanges(ct);

        return deleted;
    }

    public async Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default)
        => await _unitOfWork.Admins.ExistsByNombreAsync(nombre, ct);
}
