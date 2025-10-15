using Api.DTOs.EstadosCita;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Administrador")]
public sealed class EstadosCitaController : BaseApiController
{
    private readonly IEstadoCitaService _estadoCitaService;
    private readonly IMapper _mapper;

    public EstadosCitaController(IEstadoCitaService estadoCitaService, IMapper mapper)
    {
        _estadoCitaService = estadoCitaService;
        _mapper = mapper;
    }

    //  GET: api/EstadoCita/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<EstadoCitaDto>>> GetAllAsync(CancellationToken ct)
    {
        var estados = await _estadoCitaService.ObtenerTodosAsync(ct);
        var result = _mapper.Map<IEnumerable<EstadoCitaDto>>(estados);
        return Ok(result);
    }

    //  GET: api/EstadoCita/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EstadoCitaDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var estado = await _estadoCitaService.ObtenerPorIdAsync(new IdVO(id), ct);
        if (estado is null)
            return NotFound(new { message = $"No se encontró un estado con ID {id}." });

        var result = _mapper.Map<EstadoCitaDto>(estado);
        return Ok(result);
    }

    //  GET: api/EstadoCita/nombre/{nombre}
    [HttpGet("nombre/{nombre}")]
    public async Task<ActionResult<EstadoCitaDto>> GetByNombreAsync(string nombre, CancellationToken ct)
    {
        var estado = await _estadoCitaService.ObtenerPorNombreAsync(new NombreVO(nombre), ct);
        if (estado is null)
            return NotFound(new { message = $"No se encontró un estado con el nombre '{nombre}'." });

        var result = _mapper.Map<EstadoCitaDto>(estado);
        return Ok(result);
    }

    //  POST: api/EstadoCita
    [HttpPost]
    public async Task<ActionResult<EstadoCitaDto>> CreateAsync([FromBody] CreateEstadoCitaDto dto, CancellationToken ct)
    {
        try
        {
            var estado = new EstadoCita(
                new IdVO(0),
                new NombreVO(dto.Nombre)
            );

            await _estadoCitaService.CrearAsync(estado, ct);

            var result = _mapper.Map<EstadoCitaDto>(estado);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = estado.Id.Value }, result);
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    //  PUT: api/EstadoCita/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateEstadoCitaDto dto, CancellationToken ct)
    {
        try
        {
            var estado = new EstadoCita(
                new IdVO(id),
                new NombreVO(dto.Nombre)
            );

            var actualizado = await _estadoCitaService.ActualizarAsync(estado, ct);
            if (!actualizado)
                return NotFound(new { message = $"No se pudo actualizar el estado con ID {id}." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    //  DELETE: api/EstadoCita/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var eliminado = await _estadoCitaService.EliminarAsync(new IdVO(id), ct);
        if (!eliminado)
            return NotFound(new { message = $"No se encontró el estado con ID {id}." });

        return NoContent();
    }
}
