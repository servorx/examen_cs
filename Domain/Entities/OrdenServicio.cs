using Domain.ValueObjects;

namespace Domain.Entities;

public class OrdenServicio : BaseEntity
{
    public IdVO Id { get; set; } = null!;
    public IdVO VehiculoId { get; set; } = null!;
    public IdVO MecanicoId { get; set; } = null!;
    public IdVO TipoServicioId { get; set; } = null!;
    public IdVO EstadoId { get; set; } = null!;
    public FechaHistoricaVO FechaIngreso { get; set; } = null!;
    public FechaHistoricaVO FechaEntregaEstimada { get; set; } = null!;

    // Relaciones
    public Vehiculo Vehiculo { get; set; } = null!;
    public Mecanico Mecanico { get; set; } = null!;
    public TipoServicio TipoServicio { get; set; } = null!;
    public EstadoOrden Estado { get; set; } = null!;

    public ICollection<DetalleOrden> Detalles { get; set; } = new List<DetalleOrden>();
    public ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    // constructores
    public OrdenServicio() { }
    public OrdenServicio(IdVO id, Vehiculo vehiculo, Mecanico mecanico, TipoServicio tipoServicio, EstadoOrden estado, FechaHistoricaVO fechaIngreso, FechaHistoricaVO fechaEntregaEstimada)
    {
        Id = id;
        Vehiculo = vehiculo;
        Mecanico = mecanico;
        TipoServicio = tipoServicio;
        Estado = estado;
        FechaIngreso = fechaIngreso;
        FechaEntregaEstimada = fechaEntregaEstimada;
    }
}
