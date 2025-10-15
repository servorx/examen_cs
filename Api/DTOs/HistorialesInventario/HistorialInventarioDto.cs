namespace Api.DTOs.HistorialesInventario;

public record HistorialInventarioDto(        
    int Id,
    int RepuestoId,
    int? AdminId,
    int TipoMovimientoId,
    int Cantidad,
    DateTime FechaMovimiento,
    string? Observaciones
);
