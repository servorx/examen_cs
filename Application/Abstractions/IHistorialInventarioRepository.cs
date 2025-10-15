using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IHistorialInventarioRepository
{
    Task<HistorialInventario?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<HistorialInventario>> GetByRepuestoIdAsync(IdVO repuestoId, CancellationToken ct = default);
    Task<IReadOnlyList<HistorialInventario>> GetByAdminIdAsync(IdVO adminId, CancellationToken ct = default);
    Task<IReadOnlyList<HistorialInventario>> GetAllAsync(CancellationToken ct = default);

    Task<int> AddAsync(HistorialInventario historial, CancellationToken ct = default);
    Task<bool> UpdateAsync(HistorialInventario historial, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
