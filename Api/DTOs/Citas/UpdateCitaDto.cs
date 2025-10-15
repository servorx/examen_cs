namespace Api.DTOs.Citas;

public sealed record UpdateCitaDto(
    DateTime? FechaCita,
    string? Motivo,
    int? EstadoId
);
