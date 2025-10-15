namespace Api.DTOs.TiposServicio;

public sealed record CreateTipoServicioDto(
    string Nombre,
    string Descripcion, 
    decimal PrecioBase
);
