
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IRepuestoRepository
{
    Task<Repuesto?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<Repuesto?> GetByCodigoAsync(CodigoRepuestoVO codigo, CancellationToken ct = default);
    Task<IReadOnlyList<Repuesto>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Repuesto>> GetByProveedorIdAsync(IdVO proveedorId, CancellationToken ct = default);

    // true o false
    Task<bool> ExistsByCodigoAsync(CodigoRepuestoVO codigo, CancellationToken ct = default);
    Task<int> AddAsync(Repuesto repuesto, CancellationToken ct = default);
    Task<bool> UpdateAsync(Repuesto repuesto, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
