using Domain.ValueObjects;

namespace Domain.Entities;

public class Vehiculo : BaseEntity
{
    public IdVO Id { get; set; } = null!;
    public IdVO ClienteId { get; set; } = null!;
    public NombreVO Marca { get; set; } = null!;
    public NombreVO Modelo { get; set; } = null!;
    public AnioVehiculoVO Anio { get; set; } = null!;
    public VinVO Vin { get; set; } = null!;
    public KilometrajeVO Kilometraje { get; set; } = null!;

    // Relaciones
    public Cliente Cliente { get; set; } = null!;
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    public ICollection<OrdenServicio> OrdenesServicio { get; set; } = new List<OrdenServicio>();

    // constructores
    public Vehiculo() { }
    public Vehiculo(IdVO id, Cliente cliente, NombreVO marca, NombreVO modelo, AnioVehiculoVO anio, VinVO vin, KilometrajeVO kilometraje)
    {
        Id = id;
        Cliente = cliente;
        Marca = marca;
        Modelo = modelo;
        Anio = anio;
        Vin = vin;
        Kilometraje = kilometraje;
    }
}
