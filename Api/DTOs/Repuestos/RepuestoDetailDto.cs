namespace Api.DTOs.Repuestos;

public record RepuestoDetailDto(
    int Id,
    string Codigo,
    string Descripcion,
    int CantidadStock,
    decimal PrecioUnitario,
    int? ProveedorId,
    string? ProveedorNombre,
    IEnumerable<string>? Historiales,
    IEnumerable<string>? DetallesOrden
);
