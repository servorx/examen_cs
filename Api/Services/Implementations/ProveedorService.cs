using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class ProveedorService : IProveedorService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProveedorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> AddAsync(Proveedor proveedor, CancellationToken ct = default)
    {
        // Validar nombre único
        if (await _unitOfWork.Proveedores.ExistsByNombreAsync(proveedor.Nombre, ct))
            throw new Exception($"Ya existe un proveedor con el nombre '{proveedor.Nombre.Value}'");

        var result = await _unitOfWork.Proveedores.AddAsync(proveedor, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<bool> UpdateAsync(Proveedor proveedor, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Proveedores.GetByIdAsync(proveedor.Id, ct);
        if (existing == null)
            throw new Exception("No se puede actualizar un proveedor que no existe.");

        // Validar nombre único si se modificó
        if (existing.Nombre.Value != proveedor.Nombre.Value &&
            await _unitOfWork.Proveedores.ExistsByNombreAsync(proveedor.Nombre, ct))
        {
            throw new Exception($"Ya existe un proveedor con el nombre '{proveedor.Nombre.Value}'");
        }

        var result = await _unitOfWork.Proveedores.UpdateAsync(proveedor, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.Proveedores.GetByIdAsync(id, ct);
        if (existing == null)
            throw new Exception("No se puede eliminar un proveedor que no existe.");

        var result = await _unitOfWork.Proveedores.DeleteAsync(id, ct);
        await _unitOfWork.SaveChanges(ct);

        return result;
    }

    public async Task<IReadOnlyList<Proveedor>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.Proveedores.GetAllAsync(ct);

    public async Task<IReadOnlyList<Proveedor>> GetActivosAsync(CancellationToken ct = default)
        => await _unitOfWork.Proveedores.GetActivosAsync(ct);

    public async Task<Proveedor?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.Proveedores.GetByIdAsync(id, ct);

    public async Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default)
        => await _unitOfWork.Proveedores.ExistsByNombreAsync(nombre, ct);
}
