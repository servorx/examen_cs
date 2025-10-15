namespace Api.DTOs.Citas;

// DTo para crear una cita
public sealed record CreateCitaDto(
    int ClienteId,
    int VehiculoId,
    DateTime FechaCita,
    string? Motivo,
    int EstadoId
);
