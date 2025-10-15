using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface IRepuestoService
{
    Task<Repuesto?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Repuesto>> GetAllAsync(CancellationToken ct = default);

    Task<int> AddAsync(Repuesto repuesto, CancellationToken ct = default);
    Task<bool> UpdateAsync(Repuesto repuesto, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);

    // Métodos adicionales de negocio
    Task<bool> ExistsByCodigoAsync(CodigoRepuestoVO codigo, CancellationToken ct = default);
    Task<bool> UpdateStockAsync(IdVO id, int cantidad, CancellationToken ct = default);
}
