namespace Api.DTOs.Repuestos;

public record UpdateRepuestoDto(
    string Codigo,
    string Descripcion,
    int CantidadStock,
    decimal PrecioUnitario,
    int? ProveedorId
);
