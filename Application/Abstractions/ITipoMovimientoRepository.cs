
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface ITipoMovimientoRepository
{
    Task<TipoMovimiento?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<TipoMovimiento?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    Task<IReadOnlyList<TipoMovimiento>> GetAllAsync(CancellationToken ct = default);

    Task<int> AddAsync(TipoMovimiento tipo, CancellationToken ct = default);
    Task<bool> UpdateAsync(TipoMovimiento tipo, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
