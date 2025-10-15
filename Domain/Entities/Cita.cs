using Domain.ValueObjects;

namespace Domain.Entities;

public class Cita : BaseEntity
{
    public IdVO Id { get; set; } = null!;
    public IdVO ClienteId { get; set; } = null!;
    public IdVO VehiculoId { get; set; } = null!;
    public FechaCitaVO FechaCita { get; set; } = null!;
    public DescripcionVO? Motivo { get; set; }
    public IdVO EstadoId { get; set; } = null!;

    // Relaciones
    public Cliente Cliente { get; set; } = null!;
    public Vehiculo Vehiculo { get; set; } = null!;
    public EstadoCita Estado { get; set; } = null!;

    // constructores
    public Cita() { }
    public Cita( IdVO clienteId, IdVO vehiculoId, FechaCitaVO fechaCita, DescripcionVO? motivo, IdVO estadoId)
    {
        ClienteId = clienteId;
        VehiculoId = vehiculoId;
        FechaCita = fechaCita;
        Motivo = motivo;
        EstadoId = estadoId;
    }
}
