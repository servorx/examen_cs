namespace Api.DTOs.TiposServicio;

public sealed record UpdateTipoServicioDto(
    int Id,
    string Nombre,
    string Descripcion,
    decimal PrecioBase
);
