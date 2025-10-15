using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IOrdenServicioRepository
{
    Task<OrdenServicio?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<OrdenServicio>> GetByVehiculoIdAsync(IdVO vehiculoId, CancellationToken ct = default);
    Task<IReadOnlyList<OrdenServicio>> GetByMecanicoIdAsync(IdVO mecanicoId, CancellationToken ct = default);
    Task<IReadOnlyList<OrdenServicio>> GetAllAsync(CancellationToken ct = default);

    Task<int> AddAsync(OrdenServicio orden, CancellationToken ct = default);
    Task<bool> UpdateAsync(OrdenServicio orden, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
