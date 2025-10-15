namespace Api.DTOs.HistorialesInventario;

public record CreateHistorialInventarioDto(
    int RepuestoId,
    int? AdminId,
    int TipoMovimientoId,
    int Cantidad,
    DateTime FechaMovimiento,
    string? Observaciones
);
