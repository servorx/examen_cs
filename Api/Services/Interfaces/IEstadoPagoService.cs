using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Interfaces
{
    public interface IEstadoPagoService
    {
        Task<int> CrearAsync(EstadoPago estadoPago, CancellationToken ct = default);
        Task<bool> ActualizarAsync(EstadoPago estadoPago, CancellationToken ct = default);
        Task<bool> EliminarAsync(IdVO id, CancellationToken ct = default);
        Task<EstadoPago?> ObtenerPorIdAsync(IdVO id, CancellationToken ct = default);
        Task<EstadoPago?> ObtenerPorNombreAsync(NombreVO nombre, CancellationToken ct = default);
        Task<IReadOnlyList<EstadoPago>> ObtenerTodosAsync(CancellationToken ct = default);
    }
}
