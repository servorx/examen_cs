using Api.DTOs.EstadosOrden;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Administrador")]
public sealed class EstadosOrdenController : BaseApiController
{
    private readonly IEstadoOrdenService _estadoOrdenService;
    private readonly IMapper _mapper;

    public EstadosOrdenController(IEstadoOrdenService estadoOrdenService, IMapper mapper)
    {
        _estadoOrdenService = estadoOrdenService;
        _mapper = mapper;
    }

    //  GET: api/EstadoOrden/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<EstadoOrdenDto>>> GetAllAsync(CancellationToken ct)
    {
        var estados = await _estadoOrdenService.ObtenerTodosAsync(ct);
        var result = _mapper.Map<IEnumerable<EstadoOrdenDto>>(estados);
        return Ok(result);
    }

    //  GET: api/EstadoOrden/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EstadoOrdenDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var estado = await _estadoOrdenService.ObtenerPorIdAsync(new IdVO(id), ct);
        if (estado is null)
            return NotFound(new { message = $"No se encontró un estado con ID {id}." });

        var result = _mapper.Map<EstadoOrdenDto>(estado);
        return Ok(result);
    }

    //  GET: api/EstadoOrden/nombre/{nombre}
    [HttpGet("nombre/{nombre}")]
    public async Task<ActionResult<EstadoOrdenDto>> GetByNombreAsync(string nombre, CancellationToken ct)
    {
        var estado = await _estadoOrdenService.ObtenerPorNombreAsync(new NombreVO(nombre), ct);
        if (estado is null)
            return NotFound(new { message = $"No se encontró un estado con el nombre '{nombre}'." });

        var result = _mapper.Map<EstadoOrdenDto>(estado);
        return Ok(result);
    }

    //  POST: api/EstadoOrden
    [HttpPost]
    public async Task<ActionResult<EstadoOrdenDto>> CreateAsync([FromBody] CreateEstadoOrdenDto dto, CancellationToken ct)
    {
        try
        {
            var estado = new EstadoOrden(
                new IdVO(0),
                new NombreVO(dto.Nombre)
            );

            var creado = await _estadoOrdenService.CrearAsync(estado, ct);

            var result = _mapper.Map<EstadoOrdenDto>(creado);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = estado.Id.Value }, result);
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    //  PUT: api/EstadoOrden/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateEstadoOrdenDto dto, CancellationToken ct)
    {
        try
        {
            var estado = new EstadoOrden(
                new IdVO(id),
                new NombreVO(dto.Nombre)
            );

            var actualizado = await _estadoOrdenService.ActualizarAsync(estado, ct);
            if (!actualizado)
                return NotFound(new { message = $"No se pudo actualizar el estado con ID {id}." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    //  DELETE: api/EstadoOrden/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var eliminado = await _estadoOrdenService.EliminarAsync(new IdVO(id), ct);
        if (!eliminado)
            return NotFound(new { message = $"No se encontró el estado con ID {id}." });

        return NoContent();
    }
}
