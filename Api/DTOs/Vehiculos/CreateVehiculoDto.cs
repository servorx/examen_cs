namespace Api.DTOs.Vehiculos;

// POST /api/vehiculos
public sealed record CreateVehiculoDto(
    int ClienteId,
    string Marca,
    string Modelo,
    int Anio,
    string Vin,
    int Kilometraje
);
