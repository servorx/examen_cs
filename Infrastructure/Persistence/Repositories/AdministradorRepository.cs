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
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly AppDbContext _context;

        public AdministradorRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(Administrador admin, CancellationToken ct = default)
        {
            await _context.Administradores.AddAsync(admin, ct);
            await _context.SaveChangesAsync(ct);
            return admin.Id.Value; // devuelve el ID generado por la BD
        }

        public async Task<bool> UpdateAsync(Administrador admin, CancellationToken ct = default)
        {
            _context.Administradores.Update(admin);
            var updated = await _context.SaveChangesAsync(ct);
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
        {
            var admin = await _context.Administradores
                .FirstOrDefaultAsync(a => a.Id == id, ct);

            if (admin == null) return false;

            _context.Administradores.Remove(admin);
            var deleted = await _context.SaveChangesAsync(ct);
            return deleted > 0;
        }

        public async Task<bool> ExistsByNombreAsync(NombreVO nombre, CancellationToken ct = default)
        {
            return await _context.Administradores
                .AnyAsync(a => a.Nombre == nombre, ct);
        }

        public async Task<Administrador?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        {
            return await _context.Administradores
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id, ct);
        }

        public async Task<IReadOnlyList<Administrador>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Administradores
                .Include(a => a.User)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Administrador>> GetByNivelAccesoAsync(NivelAccesoVO nivel, CancellationToken ct = default)
        {
            return await _context.Administradores
                .Include(a => a.User)
                .Where(a => a.NivelAcceso.Value == nivel.Value)
                .ToListAsync(ct);
        }
    }
}