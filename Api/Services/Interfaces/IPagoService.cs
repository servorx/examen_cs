using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface IPagoService
{
    Task<Pago?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Pago>> GetAllAsync(CancellationToken ct = default);
    Task<int> AddAsync(Pago pago, CancellationToken ct = default);
    Task<bool> UpdateAsync(Pago pago, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);

    // Lógica de negocio
    Task<decimal> GetTotalPagadoPorFacturaAsync(IdVO facturaId, CancellationToken ct = default);
    Task<IReadOnlyList<Pago>> GetByFacturaIdAsync(IdVO facturaId, CancellationToken ct = default);
}
