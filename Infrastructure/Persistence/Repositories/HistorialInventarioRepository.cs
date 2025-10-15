using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class HistorialInventarioRepository : IHistorialInventarioRepository
    {
        private readonly AppDbContext _context;

        public HistorialInventarioRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(HistorialInventario historial, CancellationToken ct = default)
        {
            await _context.HistorialesInventario.AddAsync(historial, ct);
            await _context.SaveChangesAsync(ct);
            return historial.Id.Value;
        }

        public async Task<bool> UpdateAsync(HistorialInventario historial, CancellationToken ct = default)
        {
            _context.HistorialesInventario.Update(historial);
            var updated = await _context.SaveChangesAsync(ct);
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
        {
            var historial = await _context.HistorialesInventario
                .FirstOrDefaultAsync(h => h.Id == id, ct);

            if (historial == null) return false;

            _context.HistorialesInventario.Remove(historial);
            var deleted = await _context.SaveChangesAsync(ct);
            return deleted > 0;
        }

        public async Task<HistorialInventario?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _context.HistorialesInventario
                .Include(h => h.Repuesto)
                .Include(h => h.Administrador)
                .Include(h => h.TipoMovimiento)
                .FirstOrDefaultAsync(h => h.Id == id, ct);
        }

        public async Task<IReadOnlyList<HistorialInventario>> GetByRepuestoIdAsync(IdVO repuestoId, CancellationToken ct = default)
        {
            return await _context.HistorialesInventario
                .Include(h => h.Repuesto)
                .Include(h => h.Administrador)
                .Include(h => h.TipoMovimiento)
                .Where(h => h.RepuestoId == repuestoId)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<HistorialInventario>> GetByAdminIdAsync(IdVO adminId, CancellationToken ct = default)
        {
            return await _context.HistorialesInventario
                .Include(h => h.Repuesto)
                .Include(h => h.Administrador)
                .Include(h => h.TipoMovimiento)
                .Where(h => h.AdminId != null && h.AdminId == adminId)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<HistorialInventario>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.HistorialesInventario
                .Include(h => h.Repuesto)
                .Include(h => h.Administrador)
                .Include(h => h.TipoMovimiento)
                .ToListAsync(ct);
        }
    }
}
