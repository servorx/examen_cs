using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface ITipoServicioRepository
{
    Task<TipoServicio?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<TipoServicio?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    Task<IReadOnlyList<TipoServicio>> GetAllAsync(CancellationToken ct = default);

    Task<int> AddAsync(TipoServicio tipo, CancellationToken ct = default);
    Task<bool> UpdateAsync(TipoServicio tipo, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
