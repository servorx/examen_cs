namespace Api.DTOs.OrdenesServicio;

public sealed record OrdenServicioDto(
    int Id,
    int VehiculoId,
    int MecanicoId,
    int RecepcionistaId,
    int TipoServicioId,
    int EstadoId,
    DateTime FechaIngreso,
    DateTime FechaEntregaEstimada
);
