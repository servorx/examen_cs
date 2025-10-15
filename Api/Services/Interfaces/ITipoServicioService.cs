using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface ITipoServicioService
{
    Task<TipoServicio?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<TipoServicio?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    Task<IReadOnlyList<TipoServicio>> GetAllAsync(CancellationToken ct = default);

    Task<int> AddAsync(TipoServicio tipoServicio, CancellationToken ct = default);
    Task<bool> UpdateAsync(TipoServicio tipoServicio, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
