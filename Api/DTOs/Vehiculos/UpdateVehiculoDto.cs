namespace Api.DTOs.Vehiculos;

public sealed record UpdateVehiculoDto(
    int Id,
    string Marca,
    string Modelo,
    int Anio,
    string Vin,
    int Kilometraje
);