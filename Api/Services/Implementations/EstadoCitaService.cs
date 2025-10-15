using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations
{
    public class EstadoCitaService : IEstadoCitaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EstadoCitaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CrearAsync(EstadoCita estadoCita, CancellationToken ct = default)
        {
            var existente = await _unitOfWork.EstadoCita.GetByNombreAsync(estadoCita.Nombre, ct);
            if (existente != null)
                throw new Exception($"Ya existe un estado de cita con el nombre '{estadoCita.Nombre.Value}'.");

            return await _unitOfWork.EstadoCita.AddAsync(estadoCita, ct);
        }

        public async Task<bool> ActualizarAsync(EstadoCita estadoCita, CancellationToken ct = default)
        {
            var existente = await _unitOfWork.EstadoCita.GetByIdAsync(estadoCita.Id, ct);
            if (existente == null)
                throw new Exception("El estado de cita no existe.");

            var duplicado = await _unitOfWork.EstadoCita.GetByNombreAsync(estadoCita.Nombre, ct);
            if (duplicado != null && duplicado.Id.Value != estadoCita.Id.Value)
                throw new Exception($"Ya existe otro estado con el nombre '{estadoCita.Nombre.Value}'.");

            return await _unitOfWork.EstadoCita.UpdateAsync(estadoCita, ct);
        }

        public async Task<bool> EliminarAsync(IdVO id, CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoCita.DeleteAsync(id, ct);
        }

        public async Task<EstadoCita?> ObtenerPorIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoCita.GetByIdAsync(id, ct);
        }

        public async Task<EstadoCita?> ObtenerPorNombreAsync(NombreVO nombre, CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoCita.GetByNombreAsync(nombre, ct);
        }

        public async Task<IReadOnlyList<EstadoCita>> ObtenerTodosAsync(CancellationToken ct = default)
        {
            return await _unitOfWork.EstadoCita.GetAllAsync(ct);
        }
    }
}
