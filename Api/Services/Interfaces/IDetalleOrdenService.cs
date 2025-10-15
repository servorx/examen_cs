using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface IDetalleOrdenService
{
    Task<DetalleOrden?> GetByIdAsync(IdVO ordenServicioId, IdVO repuestoId, CancellationToken ct = default);
    Task<IReadOnlyList<DetalleOrden>> GetByOrdenIdAsync(IdVO ordenServicioId, CancellationToken ct = default);
    Task<IReadOnlyList<DetalleOrden>> GetByRepuestoIdAsync(IdVO repuestoId, CancellationToken ct = default);
    Task<int> AddAsync(DetalleOrden detalle, CancellationToken ct = default);
    Task<bool> UpdateAsync(DetalleOrden detalle, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO ordenServicioId, IdVO repuestoId, CancellationToken ct = default);
}
