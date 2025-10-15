using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Api.DTOs.Citas;
using Api.Services.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers;

[Authorize(Roles = "Cliente, Administrador")]
public class CitasController : BaseApiController
{
    private readonly ICitaService _service;
    private readonly IMapper _mapper;

    public CitasController(ICitaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET /api/citas/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CitaDto>>> GetAll(CancellationToken ct)
    {
        try
        {
            var citas = await _service.GetAllAsync(ct);
            var result = _mapper.Map<IEnumerable<CitaDto>>(citas);
            return Ok(result);
        }
        catch
        {
            return StatusCode(500, new { message = "Ocurrió un error al obtener las citas. Por favor, inténtalo de nuevo más tarde." });
        }
    }

    // GET /api/citas/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CitaDto>> GetById(int id, CancellationToken ct)
    {
        try
        {
            if (id <= 0)
                return BadRequest(new { message = "El ID proporcionado no es válido. Debe ser mayor que cero." });

            var cita = await _service.GetByIdAsync(new IdVO(id), ct);
            if (cita is null)
                return NotFound(new { message = $"No se encontró ninguna cita con el ID {id}." });

            var dto = _mapper.Map<CitaDto>(cita);
            return Ok(dto);
        }
        catch (ArgumentException ex)
        {
            // Errores relacionados con parámetros o Value Objects
            return BadRequest(new { message = "Error de argumento.", detail = ex.Message });
        }
        catch (DbUpdateException ex)
        {
            // Errores de base de datos (como claves foráneas o fallos en la consulta)
            return StatusCode(500, new { message = "Error en la base de datos al obtener la cita.", detail = ex.InnerException?.Message ?? ex.Message });
        }
        catch (Exception ex)
        {
            // Cualquier otro tipo de error inesperado
            return StatusCode(500, new { message = "Ocurrió un error inesperado al obtener la cita.", detail = ex.Message });
        }
    }

    // GET /api/citas/cliente/{clienteId}
    [HttpGet("cliente/{clienteId:int}")]
    public async Task<ActionResult<IEnumerable<CitaDto>>> GetByCliente(int clienteId, CancellationToken ct)
    {
        var citas = await _service.GetByClienteIdAsync(new IdVO(clienteId), ct);
        var result = _mapper.Map<IEnumerable<CitaDto>>(citas);
        return Ok(result);
    }

    // GET /api/citas/vehiculo/{vehiculoId}
    [HttpGet("vehiculo/{vehiculoId:int}")]
    public async Task<ActionResult<IEnumerable<CitaDto>>> GetByVehiculo(int vehiculoId, CancellationToken ct)
    {
        var citas = await _service.GetByVehiculoIdAsync(new IdVO(vehiculoId), ct);
        var result = _mapper.Map<IEnumerable<CitaDto>>(citas);
        return Ok(result);
    }

    // POST /api/citas
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateCitaDto dto, CancellationToken ct)
    {
        try
        {
            var cita = _mapper.Map<Cita>(dto);
            var id = await _service.AddAsync(cita, ct);

            return CreatedAtAction(nameof(GetById), new { id = cita.Id.Value }, _mapper.Map<CitaDto>(cita));
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // PUT /api/citas/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateCitaDto dto, CancellationToken ct)
    {
        var existing = await _service.GetByIdAsync(new IdVO(id), ct);
        if (existing is null)
            return NotFound(new { message = $"No se encontró la cita con ID {id}" });

        _mapper.Map(dto, existing);

        try
        {
            var updated = await _service.UpdateAsync(existing, ct);
            if (!updated)
                return StatusCode(500, new { message = "Error actualizando la cita" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    // DELETE /api/citas/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(new IdVO(id), ct);
        if (!deleted)
            return NotFound(new { message = $"No se encontró la cita con ID {id}" });

        return NoContent();
    }
}