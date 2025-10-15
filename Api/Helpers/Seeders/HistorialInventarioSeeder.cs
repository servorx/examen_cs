using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class HistorialInventarioSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.HistorialesInventario.AnyAsync()) return;

        var repuesto = await db.Repuestos.FirstOrDefaultAsync();
        var admin = await db.Administradores.FirstOrDefaultAsync();
        var tipoMov = await db.TiposMovimiento.FirstOrDefaultAsync();

        if (repuesto == null || admin == null || tipoMov == null)
            return;

        db.HistorialesInventario.Add(new HistorialInventario
        {
            RepuestoId = repuesto.Id,
            AdminId = admin.Id,
            TipoMovimientoId = tipoMov.Id,
            Cantidad = new CantidadVO(10),
            FechaMovimiento = new FechaHistoricaVO(DateTime.UtcNow),
            Observaciones = new DescripcionVO("Carga inicial de stock"),
        });

        await db.SaveChangesAsync();
    }
}

