using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers.Seeders;

public static class OrdenesYFacturasSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.OrdenesServicio.AnyAsync()) return;

        var vehiculo = await db.Vehiculos.FirstOrDefaultAsync();
        var mecanico = await db.Mecanicos.FirstOrDefaultAsync();
        var tipoServicio = await db.TiposServicio.FirstOrDefaultAsync();
        var estadoOrden = await db.EstadosOrden.FirstOrDefaultAsync();

        if (vehiculo == null || mecanico == null || tipoServicio == null || estadoOrden == null) return;

        var orden = new OrdenServicio
        {
            VehiculoId = vehiculo.Id,
            MecanicoId = mecanico.Id,
            TipoServicioId = tipoServicio.Id,
            EstadoId = estadoOrden.Id,
            FechaIngreso = new FechaHistoricaVO(DateTime.UtcNow),
            FechaEntregaEstimada = new FechaHistoricaVO(DateTime.UtcNow.AddDays(2))
        };

        db.OrdenesServicio.Add(orden);
        await db.SaveChangesAsync();

        var repuesto = await db.Repuestos.FirstOrDefaultAsync();
        if (repuesto != null)
        {
            db.DetallesOrden.Add(new DetalleOrden
            {
                OrdenServicioId = orden.Id,
                RepuestoId = repuesto.Id,
                Cantidad = new CantidadVO(2),
                Costo = new DineroVO(repuesto.PrecioUnitario.Value * 2)
            });
            await db.SaveChangesAsync();
        }

        var subtotal = (await db.DetallesOrden
            .Where(d => d.OrdenServicioId == orden.Id)
            .ToListAsync())
            .Sum(d => d.Costo.Value);

        db.Facturas.Add(new Factura
        {
            OrdenServicioId = orden.Id,
            MontoRepuestos = new DineroVO(subtotal),
            ManoObra = new DineroVO(50000),
            Total = new DineroVO(subtotal + 50000),
            FechaGeneracion = new FechaHistoricaVO(DateTime.UtcNow)
        });

        await db.SaveChangesAsync();

        var factura = await db.Facturas.FirstOrDefaultAsync();
        var metodo = await db.MetodosPago.FirstOrDefaultAsync();
        var estadoPago = await db.EstadosPago.FirstOrDefaultAsync();

        if (factura != null && metodo != null && estadoPago != null)
        {
            db.Pagos.Add(new Pago
            {
                FacturaId = factura.Id,
                MetodoPagoId = metodo.Id,
                EstadoPagoId = estadoPago.Id,
                Monto = factura.Total,
                FechaPago = new FechaHistoricaVO(DateTime.UtcNow)
            });

            await db.SaveChangesAsync();
        }
    }
}
