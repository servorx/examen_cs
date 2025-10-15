using Domain.Entities;
using Domain.ValueObjects; 
namespace Application.Abstractions;

public interface IMetodoPagoRepository
{
    Task<MetodoPago?> GetByIdAsync(IdVO id, CancellationToken ct = default);
    Task<MetodoPago?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default);
    Task<IReadOnlyList<MetodoPago>> GetAllAsync(CancellationToken ct = default);
    Task<int> AddAsync(MetodoPago metodo, CancellationToken ct = default);
    Task<bool> UpdateAsync(MetodoPago metodo, CancellationToken ct = default);
    Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default);
}