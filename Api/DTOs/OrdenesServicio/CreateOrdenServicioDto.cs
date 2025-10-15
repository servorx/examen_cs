namespace Api.DTOs.OrdenesServicio;

public sealed record CreateOrdenServicioDto(
    int VehiculoId,
    int MecanicoId,
    int TipoServicioId,
    int EstadoId,
    DateTime FechaIngreso,
    DateTime FechaEntregaEstimada
);
