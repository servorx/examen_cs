using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IDetalleOrdenRepository
{
    Task<DetalleOrden?> GetByIdsAsync(IdVO ordenServicioId, IdVO repuestoId, CancellationToken ct = default);
    Task<IReadOnlyList<DetalleOrden>> GetByOrdenServicioIdAsync(IdVO ordenServicioId, CancellationToken ct = default);
    Task<IReadOnlyList<DetalleOrden>> GetByRepuestoIdAsync(IdVO repuestoId, CancellationToken ct = default);

    Task<int> AddAsync(DetalleOrden detalle, CancellationToken ct = default);
    Task<bool> UpdateAsync(DetalleOrden detalle, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO ordenServicioId, IdVO repuestoId, CancellationToken ct = default);
}
