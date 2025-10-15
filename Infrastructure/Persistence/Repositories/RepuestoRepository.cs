using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class RepuestoRepository : IRepuestoRepository
    {
        private readonly AppDbContext _context;

        public RepuestoRepository(AppDbContext context) =>_context = context;

        public async Task<int> AddAsync(Repuesto repuesto, CancellationToken ct = default)
        {
            await _context.Repuestos.AddAsync(repuesto, ct);
            await _context.SaveChangesAsync(ct);
            return repuesto.Id.Value; // devuelve el ID generado por la base de datos
        }

        public async Task<bool> UpdateAsync(Repuesto repuesto, CancellationToken ct = default)
        {
            _context.Repuestos.Update(repuesto);
            var updated = await _context.SaveChangesAsync(ct);
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
        {
            var repuesto = await _context.Repuestos.FirstOrDefaultAsync(r => r.Id == id, ct);
            if (repuesto == null) return false;

            _context.Repuestos.Remove(repuesto);
            var deleted = await _context.SaveChangesAsync(ct);
            return deleted > 0;
        }

        // true o false, define si existe o no
        public async Task<bool> ExistsByCodigoAsync(CodigoRepuestoVO codigo, CancellationToken ct = default)
        {
            var repuesto = await _context.Repuestos
                .FirstOrDefaultAsync(r => r.Codigo.Value == codigo.Value, ct);
            return repuesto != null;
        }

        public async Task<Repuesto?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _context.Repuestos
                .Include(r => r.Proveedor)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
        }

        public async Task<Repuesto?> GetByCodigoAsync(CodigoRepuestoVO codigo, CancellationToken ct = default)
        {
            return await _context.Repuestos
                .Include(r => r.Proveedor)
                .FirstOrDefaultAsync(r => r.Codigo.Value == codigo.Value, ct);
        }

        public async Task<IReadOnlyList<Repuesto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Repuestos
                .Include(r => r.Proveedor)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Repuesto>> GetByProveedorIdAsync(IdVO proveedorId, CancellationToken ct = default)
        {
            return await _context.Repuestos
                .Include(r => r.Proveedor)
                .Where(r => r.ProveedorId != null && r.ProveedorId.Value == proveedorId.Value)
                .ToListAsync(ct);
        }
    }
}
