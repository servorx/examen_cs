using Api.DTOs.Facturas;
using Api.DTOs.Vehiculos;
using Api.DTOs.Mecanicos;
using Api.DTOs.TiposServicio;
using Api.DTOs.EstadosOrden;
using Api.DTOs.DetallesOrden;

namespace Api.DTOs.OrdenesServicio;

public sealed record OrdenServicioDetailDto(
    int Id,
    DateTime FechaIngreso,
    DateTime FechaEntregaEstimada,
    VehiculoDto Vehiculo,
    MecanicoDto Mecanico,
    TipoServicioDto TipoServicio,
    EstadoOrdenDto Estado,
    IReadOnlyList<DetalleOrdenDto> Detalles,
    IReadOnlyList<FacturaDto> Facturas
);