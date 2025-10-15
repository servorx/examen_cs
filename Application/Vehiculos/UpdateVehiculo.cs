using Domain.ValueObjects;
using MediatR;

namespace Application.Vehiculos;

public sealed record UpdateVehiculo(
    IdVO Id,
    IdVO ClienteId,
    NombreVO Marca,
    NombreVO Modelo,
    AnioVehiculoVO Anio,
    VinVO Vin,
    KilometrajeVO Kilometraje
) : IRequest<bool>;
