
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface IEstadoOrdenRepository
{
    Task<EstadoOrden?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<EstadoOrden?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    Task<IReadOnlyList<EstadoOrden>> GetAllAsync(CancellationToken ct = default);

    Task<EstadoOrden> AddAsync(EstadoOrden estado, CancellationToken ct = default);
    Task<bool> UpdateAsync(EstadoOrden estado, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}
