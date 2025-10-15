using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly AppDbContext _context;

        public FacturaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Factura factura, CancellationToken ct = default)
        {
            await _context.Facturas.AddAsync(factura, ct);
            await _context.SaveChangesAsync(ct);
            return factura.Id.Value; // devuelve el ID generado por la BD
        }

        public async Task<bool> UpdateAsync(Factura factura, CancellationToken ct = default)
        {
            _context.Facturas.Update(factura);
            var updated = await _context.SaveChangesAsync(ct);
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
        {
            var factura = await _context.Facturas
                .Include(f => f.Pagos)
                .Include(f => f.OrdenServicio)
                .FirstOrDefaultAsync(f => f.Id == id, ct);

            if (factura == null) return false;

            _context.Facturas.Remove(factura);
            var deleted = await _context.SaveChangesAsync(ct);
            return deleted > 0;
        }

        public async Task<Factura?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _context.Facturas
                .Include(f => f.Pagos)
                .Include(f => f.OrdenServicio)
                .FirstOrDefaultAsync(f => f.Id == id, ct);
        }

        public async Task<IReadOnlyList<Factura>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Facturas
                .Include(f => f.Pagos)
                .Include(f => f.OrdenServicio)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Factura>> GetByOrdenServicioIdAsync(IdVO ordenServicioId, CancellationToken ct = default)
        {
            return await _context.Facturas
                .Include(f => f.Pagos)
                .Include(f => f.OrdenServicio)
                .Where(f => f.OrdenServicioId == ordenServicioId)
                .ToListAsync(ct);
        }
    }
}
