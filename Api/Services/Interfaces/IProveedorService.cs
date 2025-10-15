using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface IProveedorService
{
    Task<Proveedor?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Proveedor>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Proveedor>> GetActivosAsync(CancellationToken ct = default);

    Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    Task<int> AddAsync(Proveedor proveedor, CancellationToken ct = default);
    Task<bool> UpdateAsync(Proveedor proveedor, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
