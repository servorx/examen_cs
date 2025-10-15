using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IPagoRepository
{
    Task<Pago?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Pago>> GetByFacturaIdAsync(IdVO facturaId, CancellationToken ct = default);
    Task<IReadOnlyList<Pago>> GetByMetodoPagoIdAsync(IdVO metodoPagoId, CancellationToken ct = default);
    Task<IReadOnlyList<Pago>> GetByEstadoPagoIdAsync(IdVO estadoPagoId, CancellationToken ct = default);
    Task<IReadOnlyList<Pago>> GetAllAsync(CancellationToken ct = default);

    Task<int> AddAsync(Pago pago, CancellationToken ct = default);
    Task<bool> UpdateAsync(Pago pago, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
