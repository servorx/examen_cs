using Domain.ValueObjects;

namespace Domain.Entities;

public class Factura : BaseEntity
{
    public IdVO Id { get; set; } = null!;
    public IdVO OrdenServicioId { get; set; } = null!;
    public DineroVO MontoRepuestos { get; set; } = null!;
    public DineroVO ManoObra { get; set; } = null!;
    public DineroVO Total { get; set; } = null!;
    public FechaHistoricaVO FechaGeneracion { get; set; } = null!;

    // Relaciones
    public OrdenServicio OrdenServicio { get; set; } = null!;
    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public Factura() { }
    public Factura(IdVO id, IdVO ordenServicioId, DineroVO montoRepuestos, DineroVO manoObra, DineroVO total, FechaHistoricaVO fechaGeneracion)
    {
        Id = id;
        OrdenServicioId = ordenServicioId;
        MontoRepuestos = montoRepuestos;
        ManoObra = manoObra;
        Total = total;
        FechaGeneracion = fechaGeneracion;
    }
}
