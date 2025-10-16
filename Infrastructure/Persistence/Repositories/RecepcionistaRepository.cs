using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class RecepcionistaRepository : IRecepcionistaRepository
    {
        private readonly AppDbContext _context;

        public RecepcionistaRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(Recepcionista admin, CancellationToken ct = default)
        {
            await _context.Recepcionistas.AddAsync(admin, ct);
            await _context.SaveChangesAsync(ct);
            return admin.Id.Value; // devuelve el ID generado por la BD
        }

        public async Task<bool> UpdateAsync(Recepcionista admin, CancellationToken ct = default)
        {
            _context.Recepcionistas.Update(admin);
            var updated = await _context.SaveChangesAsync(ct);
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
        {
            var admin = await _context.Recepcionistas
                .FirstOrDefaultAsync(a => a.Id == id, ct);

            if (admin == null) return false;

            _context.Recepcionistas.Remove(admin);
            var deleted = await _context.SaveChangesAsync(ct);
            return deleted > 0;
        }

        public async Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default)
        {
            return await _context.Recepcionistas
                .AnyAsync(a => a.Nombre == nombre, ct);
        }

        public async Task<Recepcionista?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _context.Recepcionistas
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id, ct);
        }

        public async Task<IReadOnlyList<Recepcionista>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Recepcionistas
                .Include(a => a.User)
                .ToListAsync(ct);
        }
    }
}