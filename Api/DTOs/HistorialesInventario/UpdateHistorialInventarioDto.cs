namespace Api.DTOs.HistorialesInventario;

public record UpdateHistorialInventarioDto(
    int Cantidad,
    DateTime FechaMovimiento,
    string? Observaciones
);
