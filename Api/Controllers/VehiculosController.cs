using Api.DTOs.Vehiculos;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class VehiculosController : BaseApiController
{
    private readonly IVehiculoService _vehiculoService;
    private readonly IMapper _mapper;

    public VehiculosController(IVehiculoService vehiculoService, IMapper mapper)
    {
        _vehiculoService = vehiculoService;
        _mapper = mapper;
    }

    // GET: api/vehiculos/all
    [HttpGet("all")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<VehiculoDto>>> GetAll(CancellationToken ct)
    {
        var vehiculos = await _vehiculoService.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<VehiculoDto>>(vehiculos);
        return Ok(dto);
    }

    // GET: api/vehiculos/{id}
    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<VehiculoDetailDto>> GetById(int id, CancellationToken ct)
    {
        var vehiculo = await _vehiculoService.GetByIdAsync(new IdVO(id), ct);
        if (vehiculo is null)
            return NotFound(new { message = $"No se encontró un vehículo con ID {id}" });

        var dto = _mapper.Map<VehiculoDetailDto>(vehiculo);
        return Ok(dto);
    }

    // GET: api/vehiculos/vin/{vin}
    [HttpGet("vin/{vin}")]
    [Authorize]
    public async Task<ActionResult<VehiculoDetailDto>> GetByVin(string vin, CancellationToken ct)
    {
        var vinVo = new VinVO(vin);
        var vehiculos = await _vehiculoService.GetAllAsync(ct);
        var vehiculo = vehiculos.FirstOrDefault(v => v.Vin.Value.Equals(vin, StringComparison.OrdinalIgnoreCase));

        if (vehiculo is null)
            return NotFound(new { message = $"No se encontró un vehículo con VIN {vin}" });

        var dto = _mapper.Map<VehiculoDetailDto>(vehiculo);
        return Ok(dto);
    }

    // GET: api/vehiculos/cliente/{clienteId}
    [HttpGet("cliente/{clienteId:int}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<VehiculoDto>>> GetByClienteId(int clienteId, CancellationToken ct)
    {
        var vehiculos = await _vehiculoService.GetByClienteIdAsync(new IdVO(clienteId), ct);
        var dto = _mapper.Map<IEnumerable<VehiculoDto>>(vehiculos);
        return Ok(dto);
    }

    // POST: api/vehiculos
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Create([FromBody] CreateVehiculoDto body, CancellationToken ct)
    {
        try
        {
            var vehiculo = new Vehiculo
            {
                ClienteId = new IdVO(body.ClienteId),
                Marca = new NombreVO(body.Marca),
                Modelo = new NombreVO(body.Modelo),
                Anio = new AnioVehiculoVO(body.Anio),
                Vin = new VinVO(body.Vin),
                Kilometraje = new KilometrajeVO(body.Kilometraje)
            };

            await _vehiculoService.AddAsync(vehiculo, ct);

            var dto = _mapper.Map<VehiculoDto>(vehiculo);
            return CreatedAtAction(nameof(GetById), new { id = vehiculo.Id.Value }, dto);
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // PUT: api/vehiculos/{id}
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVehiculoDto body, CancellationToken ct)
    {
        var existing = await _vehiculoService.GetByIdAsync(new IdVO(id), ct);
        if (existing is null)
            return NotFound(new { message = $"No se encontró un vehículo con ID {id}" });

        // Mapeo manual para evitar sobrescribir relaciones
        existing.Marca = new NombreVO(body.Marca);
        existing.Modelo = new NombreVO(body.Modelo);
        existing.Anio = new AnioVehiculoVO(body.Anio);
        existing.Vin = new VinVO(body.Vin);
        existing.Kilometraje = new KilometrajeVO(body.Kilometraje);

        try
        {
            var updated = await _vehiculoService.UpdateAsync(existing, ct);
            if (!updated)
                return StatusCode(500, new { message = "Error actualizando el vehículo" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // DELETE: api/vehiculos/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _vehiculoService.DeleteAsync(new IdVO(id), ct);
        if (!deleted)
            return NotFound(new { message = $"No se encontró un vehículo con ID {id}" });

        return NoContent();
    }
}
