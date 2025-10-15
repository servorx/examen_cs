
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class FacturasSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Facturas.AnyAsync()) return;

        var orden = await db.OrdenesServicio.FirstOrDefaultAsync();
        var repuesto = await db.Repuestos.FirstOrDefaultAsync();

        if (orden == null || repuesto == null)
            return;

        db.Facturas.AddRange(new[]
        {
            new Factura
            {
                OrdenServicioId = orden.Id,
                MontoRepuestos = new DineroVO(repuesto.PrecioUnitario.Value * 2),
                ManoObra = new DineroVO(50000),
                Total = new DineroVO(repuesto.PrecioUnitario.Value * 2 + 50000),
                FechaGeneracion = new FechaHistoricaVO(DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Factura
            {
                OrdenServicioId = orden.Id,
                MontoRepuestos = new DineroVO(repuesto.PrecioUnitario.Value * 3),
                ManoObra = new DineroVO(70000),
                Total = new DineroVO(repuesto.PrecioUnitario.Value * 3 + 70000),
                FechaGeneracion = new FechaHistoricaVO(DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        });
        await db.SaveChangesAsync();
    }
}
