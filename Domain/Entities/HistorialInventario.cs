using Domain.ValueObjects;

namespace Domain.Entities;

public class HistorialInventario
{
    public IdVO Id { get; set; } = null!;
    public IdVO RepuestoId { get; set; } = null!;
    public IdVO? AdminId { get; set; }
    public IdVO TipoMovimientoId { get; set; } = null!;
    public CantidadVO Cantidad { get; set; } = null!;
    public FechaHistoricaVO FechaMovimiento { get; set; } = null!;
    public DescripcionVO? Observaciones { get; set; }

    // Relaciones
    public Repuesto Repuesto { get; set; } = null!;
    public Administrador? Administrador { get; set; }
    public TipoMovimiento TipoMovimiento { get; set; } = null!;

    public HistorialInventario() { }
    public HistorialInventario(IdVO id, IdVO repuestoId, IdVO? adminId, IdVO tipoMovimientoId, CantidadVO cantidad, FechaHistoricaVO fechaMovimiento, DescripcionVO? observaciones)
    {
        Id = id;
        RepuestoId = repuestoId;
        AdminId = adminId;
        TipoMovimientoId = tipoMovimientoId;
        Cantidad = cantidad;
        FechaMovimiento = fechaMovimiento;
        Observaciones = observaciones;
    }
}
