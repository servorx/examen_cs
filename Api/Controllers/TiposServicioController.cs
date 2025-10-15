using Api.DTOs.TiposServicio;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TiposServicioController : BaseApiController
{
    private readonly ITipoServicioService _service;
    private readonly IMapper _mapper;

    public TiposServicioController(ITipoServicioService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/TipoServicio/all
    [HttpGet("all")]
    [Authorize(Roles = "Mecanico, Administrador")]
    public async Task<ActionResult<IEnumerable<TipoServicioDto>>> GetAllAsync(CancellationToken ct)
    {
        var tipos = await _service.GetAllAsync(ct);
        var result = _mapper.Map<IEnumerable<TipoServicioDto>>(tipos);
        return Ok(result);
    }

    // GET: api/TipoServicio/{id}
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Mecanico, Administrador")]
    public async Task<ActionResult<TipoServicioDetailDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var tipo = await _service.GetByIdAsync(new IdVO(id), ct);
        if (tipo == null)
            return NotFound($"No se encontró el tipo de servicio con ID {id}.");

        var dto = _mapper.Map<TipoServicioDetailDto>(tipo);
        return Ok(dto);
    }

    // GET: api/TipoServicio/nombre/{nombre}
    [HttpGet("nombre/{nombre}")]
    [Authorize(Roles = "Mecanico, Administrador")]
    public async Task<ActionResult<TipoServicioDetailDto>> GetByNombreAsync(string nombre, CancellationToken ct)
    {
        var tipo = await _service.GetByNombreAsync(new NombreVO(nombre), ct);
        if (tipo == null)
            return NotFound($"No se encontró el tipo de servicio con nombre '{nombre}'.");

        var dto = _mapper.Map<TipoServicioDetailDto>(tipo);
        return Ok(dto);
    }

    // POST: api/TipoServicio
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<TipoServicioDetailDto>> CreateAsync([FromBody] CreateTipoServicioDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");

        var tipo = _mapper.Map<TipoServicio>(dto);

        try
        {
            await _service.AddAsync(tipo, ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var result = _mapper.Map<TipoServicioDetailDto>(tipo);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = tipo.Id.Value }, result);
    }

    //  PUT: api/TipoServicio/{id}
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateTipoServicioDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");

        var existing = await _service.GetByIdAsync(new IdVO(id), ct);
        if (existing == null)
            return NotFound($"No se encontró el tipo de servicio con ID {id}.");

        _mapper.Map(dto, existing);

        var updated = await _service.UpdateAsync(existing, ct);
        if (!updated)
            return BadRequest("No se pudo actualizar el tipo de servicio.");

        return NoContent();
    }

    //  DELETE: api/TipoServicio/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(new IdVO(id), ct);
        if (!deleted)
            return NotFound($"No se encontró el tipo de servicio con ID {id}.");

        return NoContent();
    }
}
