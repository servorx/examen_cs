using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class EstadoPagoRepository : IEstadoPagoRepository
    {
        private readonly AppDbContext _context;

        public EstadoPagoRepository(AppDbContext context) =>_context = context;

        public async Task<int> AddAsync(EstadoPago estado, CancellationToken ct = default)
        {
            await _context.EstadosPago.AddAsync(estado, ct);
            await _context.SaveChangesAsync(ct);
            return estado.Id.Value; // devuelve el ID generado por la BD
        }

        public async Task<bool> UpdateAsync(EstadoPago estado, CancellationToken ct = default)
        {
            _context.EstadosPago.Update(estado);
            var updated = await _context.SaveChangesAsync(ct);
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
        {
            var entity = await _context.EstadosPago
                .FirstOrDefaultAsync(e => e.Id == id, ct);

            if (entity == null) return false;

            _context.EstadosPago.Remove(entity);
            var deleted = await _context.SaveChangesAsync(ct);
            return deleted > 0;
        }

        public async Task<EstadoPago?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _context.EstadosPago
                .FirstOrDefaultAsync(e => e.Id == id, ct);
        }

        public async Task<EstadoPago?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default)
        {
            return await _context.EstadosPago
                .FirstOrDefaultAsync(e => e.Nombre == nombre, ct);
        }

        public async Task<IReadOnlyList<EstadoPago>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.EstadosPago.ToListAsync(ct);
        }
    }
}
