using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IVehiculoRepository
{
    Task<Vehiculo?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<Vehiculo?> GetByVinAsync(VinVO vin, CancellationToken ct = default);
    Task<IReadOnlyList<Vehiculo>> GetByClienteIdAsync(IdVO clienteId, CancellationToken ct = default);
    Task<IReadOnlyList<Vehiculo>> GetAllAsync(CancellationToken ct = default);

    // devolver false o tru
    Task<bool> ExistsByVinAsync(VinVO vin, CancellationToken ct = default);
    // metodos de insercion, actualizacion y eliminacion
    Task<int> AddAsync(Vehiculo vehiculo, CancellationToken ct = default);
    Task<bool> UpdateAsync(Vehiculo vehiculo, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
