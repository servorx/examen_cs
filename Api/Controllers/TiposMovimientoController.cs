using Api.DTOs.TiposMovimiento;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TiposMovimientoController : BaseApiController
{
    private readonly ITipoMovimientoService _service;
    private readonly IMapper _mapper;

    public TiposMovimientoController(ITipoMovimientoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    //  GET: api/TipoMovimiento/all
    [HttpGet("all")]
    [Authorize(Roles = "Proveedor, Administrador")]
    public async Task<ActionResult<IEnumerable<TipoMovimientoDto>>> GetAllAsync(CancellationToken ct)
    {
        var tipos = await _service.ObtenerTodosAsync(ct);
        var result = _mapper.Map<IEnumerable<TipoMovimientoDto>>(tipos);
        return Ok(result);
    }

    //  GET: api/TipoMovimiento/{id}
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Proveedor, Administrador")]
    public async Task<ActionResult<TipoMovimientoDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var tipo = await _service.ObtenerPorIdAsync(new IdVO(id), ct);
        if (tipo == null)
            return NotFound($"No se encontró el tipo de movimiento con ID {id}.");

        var result = _mapper.Map<TipoMovimientoDto>(tipo);
        return Ok(result);
    }

    //  POST: api/TipoMovimiento
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<TipoMovimientoDto>> CreateAsync([FromBody] CreateTipoMovimientoDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");

        var tipo = new TipoMovimiento(IdVO.CreateNew(), new NombreVO(dto.Nombre));

        try
        {
            await _service.CrearAsync(tipo, ct);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }

        var result = _mapper.Map<TipoMovimientoDto>(tipo);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = tipo.Id.Value }, result);
    }

    //  PUT: api/TipoMovimiento/{id}
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateTipoMovimientoDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");

        var existing = await _service.ObtenerPorIdAsync(new IdVO(id), ct);
        if (existing == null)
            return NotFound($"No se encontró el tipo de movimiento con ID {id}.");

        existing.Nombre = new NombreVO(dto.Nombre);

        var updated = await _service.ActualizarAsync(existing, ct);
        if (!updated)
            return BadRequest("No se pudo actualizar el tipo de movimiento.");

        return NoContent();
    }

    //  DELETE: api/TipoMovimiento/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var deleted = await _service.EliminarAsync(new IdVO(id), ct);
        if (!deleted)
            return NotFound($"No se encontró el tipo de movimiento con ID {id}.");

        return NoContent();
    }
}
