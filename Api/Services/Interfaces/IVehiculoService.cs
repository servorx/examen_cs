using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces;

public interface IVehiculoService
{
    Task<Vehiculo?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Vehiculo>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Vehiculo>> GetByClienteIdAsync(IdVO clienteId, CancellationToken ct = default);

    Task<int> AddAsync(Vehiculo vehiculo, CancellationToken ct = default);
    Task<bool> UpdateAsync(Vehiculo vehiculo, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);

    Task<bool> ExistsByVinAsync(VinVO vin, CancellationToken ct = default);
}
