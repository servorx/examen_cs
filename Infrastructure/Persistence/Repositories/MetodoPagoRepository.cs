using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class MetodoPagoRepository : IMetodoPagoRepository
    {
        private readonly AppDbContext _context;

        public MetodoPagoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(MetodoPago metodo, CancellationToken ct = default)
        {
            await _context.MetodosPago.AddAsync(metodo, ct);
            await _context.SaveChangesAsync(ct);
            return metodo.Id.Value;
        }

        public async Task<bool> UpdateAsync(MetodoPago metodo, CancellationToken ct = default)
        {
            _context.MetodosPago.Update(metodo);
            var updated = await _context.SaveChangesAsync(ct);
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
        {
            var metodo = await _context.MetodosPago
                .FirstOrDefaultAsync(m => m.Id == id, ct);

            if (metodo == null) return false;

            _context.MetodosPago.Remove(metodo);
            var deleted = await _context.SaveChangesAsync(ct);
            return deleted > 0;
        }

        public async Task<MetodoPago?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _context.MetodosPago
                .FirstOrDefaultAsync(m => m.Id == id, ct);
        }

        public async Task<MetodoPago?> GetByNombreAsync(NombreVO nombre, CancellationToken ct = default)
        {
            return await _context.MetodosPago
                .FirstOrDefaultAsync(m => m.Nombre == nombre, ct);
        }

        public async Task<IReadOnlyList<MetodoPago>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.MetodosPago
                .ToListAsync(ct);
        }
    }
}
