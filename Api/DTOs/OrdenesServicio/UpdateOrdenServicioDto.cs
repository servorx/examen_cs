namespace Api.DTOs.OrdenesServicio;

public sealed record UpdateOrdenServicioDto(
    int VehiculoId,
    int MecanicoId,
    int TipoServicioId,
    int EstadoId,
    DateTime FechaIngreso,
    DateTime FechaEntregaEstimada
);
