using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces
{
    public interface IMetodoPagoService
    {
        Task<int> CrearAsync(MetodoPago metodoPago, CancellationToken ct = default);
        Task<bool> ActualizarAsync(MetodoPago metodoPago, CancellationToken ct = default);
        Task<bool> EliminarAsync(IdVO id, CancellationToken ct = default);
        Task<MetodoPago?> ObtenerPorIdAsync(IdVO id, CancellationToken ct = default);
        Task<MetodoPago?> ObtenerPorNombreAsync(NombreVO nombre, CancellationToken ct = default);
        Task<IReadOnlyList<MetodoPago>> ObtenerTodosAsync(CancellationToken ct = default);
    }
}
