using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface IOrdenServicioService
{
    Task<OrdenServicio?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<OrdenServicio>> GetAllAsync(CancellationToken ct = default);

    Task<int> AddAsync(OrdenServicio ordenServicio, CancellationToken ct = default);
    Task<bool> UpdateAsync(OrdenServicio ordenServicio, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);

    // Métodos adicionales de negocio
    Task<IReadOnlyList<OrdenServicio>> GetByVehiculoAsync(IdVO vehiculoId, CancellationToken ct = default);
    Task<IReadOnlyList<OrdenServicio>> GetByMecanicoAsync(IdVO mecanicoId, CancellationToken ct = default);
}
