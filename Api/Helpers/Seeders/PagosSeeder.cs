using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class PagosSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Pagos.AnyAsync()) return;

        var factura = await db.Facturas.FirstOrDefaultAsync();
        var metodo = await db.MetodosPago.FirstOrDefaultAsync();
        var estado = await db.EstadosPago.FirstOrDefaultAsync();

        if (factura == null || metodo == null || estado == null)
            return;

        db.Pagos.AddRange(new[]
        {
            new Pago
            {
                FacturaId = factura.Id,
                MetodoPagoId = metodo.Id,
                EstadoPagoId = estado.Id,
                Monto = factura.Total,
                FechaPago = new FechaHistoricaVO(DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Pago
            {
                FacturaId = factura.Id,
                MetodoPagoId = metodo.Id,
                EstadoPagoId = estado.Id,
                Monto = factura.Total,
                FechaPago = new FechaHistoricaVO(DateTime.UtcNow),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        });

        await db.SaveChangesAsync();
    }
}


