using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface ICitaRepository
{
    Task<Cita?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<IReadOnlyList<Cita>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Cita>> GetByClienteIdAsync(IdVO clienteId, CancellationToken ct = default);
    Task<IReadOnlyList<Cita>> GetByVehiculoIdAsync(IdVO vehiculoId, CancellationToken ct = default);
    Task<IReadOnlyList<Cita>> GetByFechaAsync(FechaCitaVO fecha, CancellationToken ct = default);

    Task<int> AddAsync(Cita cita, CancellationToken ct = default);
    Task<bool> UpdateAsync(Cita cita, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
