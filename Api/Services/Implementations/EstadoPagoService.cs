using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations
{
    public class EstadoPagoService : IEstadoPagoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EstadoPagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CrearAsync(EstadoPago estadoPago, CancellationToken ct = default)
        {
            // Validar duplicados por nombre antes de crear
            var existente = await _unitOfWork.EstadoPago.GetByNombreAsync(estadoPago.Nombre, ct);
            if (existente != null)
                throw new Exception($"Ya existe un estado de pago con el nombre '{estadoPago.Nombre.Value}'");

            return await _unitOfWork.EstadoPago.AddAsync(estadoPago, ct);
        }

        public async Task<bool> ActualizarAsync(EstadoPago estadoPago, CancellationToken ct = default)
        {
            var existente = await _unitOfWork.EstadoPago.GetByIdAsync(estadoPago.Id, ct);
            if (existente == null)
                throw new Exception("El estado de pago no existe.");

            // Evita duplicar nombres
            var duplicado = await _unitOfWork.EstadoPago.GetByNombreAsync(estadoPago.Nombre, ct);
            if (duplicado != null && duplicado.Id.Value != estadoPago.Id.Value)
                throw new Exception($"Ya existe otro estado con el nombre '{estadoPago.Nombre.Value}'");

            return await _unitOfWork.EstadoPago.UpdateAsync(estadoPago, ct);
        }

        public async Task<bool> EliminarAsync(IdVO id, CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoPago.DeleteAsync(id, ct);
        }

        public async Task<EstadoPago?> ObtenerPorIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoPago.GetByIdAsync(id, ct);
        }

        public async Task<EstadoPago?> ObtenerPorNombreAsync(NombreVO nombre, CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoPago.GetByNombreAsync(nombre, ct);
        }

        public async Task<IReadOnlyList<EstadoPago>> ObtenerTodosAsync(CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoPago.GetAllAsync(ct);
        }
    }
}
