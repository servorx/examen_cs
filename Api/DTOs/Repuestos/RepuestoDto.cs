namespace Api.DTOs.Repuestos;

public record RepuestoDto(
    int Id,
    string Codigo,
    string Descripcion,
    int CantidadStock,
    decimal PrecioUnitario,
    int? ProveedorId
);
