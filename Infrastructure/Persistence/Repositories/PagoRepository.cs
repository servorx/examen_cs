using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly AppDbContext _context;

        public PagoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Pago pago, CancellationToken ct = default)
        {
            await _context.Pagos.AddAsync(pago, ct);
            await _context.SaveChangesAsync(ct);
            return pago.Id.Value.GetHashCode(); // aqui devuelve el id del pago
        }

        public async Task<bool> UpdateAsync(Pago pago, CancellationToken ct = default)
        {
            _context.Pagos.Update(pago);
            var updated = await _context.SaveChangesAsync(ct);
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
        {
            var pago = await _context.Pagos.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (pago == null) return false;

            _context.Pagos.Remove(pago);
            var deleted = await _context.SaveChangesAsync(ct);
            return deleted > 0;
        }

        public async Task<Pago?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _context.Pagos
                .Include(p => p.MetodoPago)
                .Include(p => p.EstadoPago)
                .Include(p => p.Factura)
                .FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task<IReadOnlyList<Pago>> GetByFacturaIdAsync(IdVO facturaId, CancellationToken ct = default)
        {
            return await _context.Pagos
                .Include(p => p.MetodoPago)
                .Include(p => p.EstadoPago)
                .Include(p => p.Factura)
                .Where(p => p.FacturaId == facturaId)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Pago>> GetByMetodoPagoIdAsync(IdVO metodoPagoId, CancellationToken ct = default)
        {
            return await _context.Pagos
                .Include(p => p.MetodoPago)
                .Include(p => p.EstadoPago)
                .Include(p => p.Factura)
                .Where(p => p.MetodoPagoId == metodoPagoId)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Pago>> GetByEstadoPagoIdAsync(IdVO estadoPagoId, CancellationToken ct = default)
        {
            return await _context.Pagos
                .Include(p => p.MetodoPago)
                .Include(p => p.EstadoPago)
                .Include(p => p.Factura)
                .Where(p => p.EstadoPagoId == estadoPagoId)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Pago>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Pagos
                .Include(p => p.MetodoPago)
                .Include(p => p.EstadoPago)
                .Include(p => p.Factura)
                .ToListAsync(ct);
        }
    }
}
