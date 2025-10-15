using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface IFacturaService
{
    Task<Factura?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Factura>> GetAllAsync(CancellationToken ct = default);
    Task<int> AddAsync(Factura factura, CancellationToken ct = default);
    Task<bool> UpdateAsync(Factura factura, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);

    // Lógica de negocio
    Task<decimal> CalcularTotalAsync(IdVO facturaId, CancellationToken ct = default);
    Task<IReadOnlyList<Factura>> GetByOrdenServicioIdAsync(IdVO ordenServicioId, CancellationToken ct = default);
}
