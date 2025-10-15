using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context) => _context = context;

    public async Task<Cliente?> GetByIdAsync(IdVO id, CancellationToken ct = default)
    {
        return await _context.Clientes
            .Include(c => c.Vehiculos)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IReadOnlyList<Cliente>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Clientes
            .Include(c => c.Vehiculos)
            .ToListAsync(ct);
    }

    public async Task<Cliente?> GetByUserIdAsync(IdVO userId, CancellationToken ct = default)
    {
        return await _context.Clientes
            .Include(c => c.Vehiculos)
            .FirstOrDefaultAsync(c => c.UserId == userId.Value, ct);
    }

    public async Task<bool> ExistsByEmailAsync(CorreoVO correo, CancellationToken ct = default)
    {
        return await _context.Clientes.AnyAsync(c => c.Correo == correo, ct);
    }

    public async Task<int> AddAsync(Cliente cliente, CancellationToken ct = default)
    {
        await _context.Clientes.AddAsync(cliente, ct);
        await _context.SaveChangesAsync(ct);
        return cliente.Id.Value;
    }

    public async Task<bool> UpdateAsync(Cliente cliente, CancellationToken ct = default)
    {
        _context.Clientes.Update(cliente);
        var updated = await _context.SaveChangesAsync(ct);
        return updated > 0;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id, ct);
        if (cliente == null) return false;

        _context.Clientes.Remove(cliente);
        var deleted = await _context.SaveChangesAsync(ct);
        return deleted > 0;
    }
}