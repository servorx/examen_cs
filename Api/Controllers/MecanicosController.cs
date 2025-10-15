using Api.DTOs.Mecanicos;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public sealed class MecanicosController : BaseApiController
{
    private readonly IMecanicoService _mecanicoService;
    private readonly IMapper _mapper;

    public MecanicosController(IMecanicoService mecanicoService, IMapper mapper)
    {
        _mecanicoService = mecanicoService;
        _mapper = mapper;
    }

    // GET: api/Mecanicos/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<MecanicoDto>>> GetAllAsync(CancellationToken ct)
    {
        var mecanicos = await _mecanicoService.GetAllAsync(ct);
        var result = _mapper.Map<IEnumerable<MecanicoDto>>(mecanicos);
        return Ok(result);
    }

    // GET: api/Mecanicos/activos
    [HttpGet("activos")]
    public async Task<ActionResult<IEnumerable<MecanicoDto>>> GetActiveAsync(CancellationToken ct)
    {
        var activos = await _mecanicoService.GetActiveAsync(ct);
        var result = _mapper.Map<IEnumerable<MecanicoDto>>(activos);
        return Ok(result);
    }

    //  GET: api/Mecanicos/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MecanicoDetailDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var mecanico = await _mecanicoService.GetByIdAsync(new IdVO(id), ct);
        if (mecanico is null)
            return NotFound($"No se encontró el mecánico con ID {id}.");

        var result = _mapper.Map<MecanicoDetailDto>(mecanico);
        return Ok(result);
    }

    //  POST: api/Mecanicos
    [HttpPost]
    public async Task<ActionResult<MecanicoDto>> CreateAsync([FromBody] CreateMecanicoDto dto, CancellationToken ct)
    {
        // Validar nombre duplicado
        var existeNombre = await _mecanicoService.ExistsByNombreAsync(new NombreVO(dto.Nombre), ct);
        if (existeNombre)
            return Conflict($"Ya existe un mecánico con el nombre '{dto.Nombre}'.");

        var mecanico = new Mecanico(
            new IdVO(0),
            new NombreVO(dto.Nombre),
            string.IsNullOrWhiteSpace(dto.Telefono) ? null : new TelefonoVO(dto.Telefono),
            string.IsNullOrWhiteSpace(dto.Especialidad) ? null : new EspecialidadVO(dto.Especialidad),
            new EstadoVO(dto.IsActive),
            dto.UserId
        );

        var id = await _mecanicoService.AddAsync(mecanico, ct);
        var result = _mapper.Map<MecanicoDto>(mecanico);

        return CreatedAtAction(nameof(GetByIdAsync), new { id }, result);
    }

    //  PUT: api/Mecanicos/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateMecanicoDto dto, CancellationToken ct)
    {
        var existing = await _mecanicoService.GetByIdAsync(new IdVO(id), ct);
        if (existing is null)
            return NotFound($"No se encontró el mecánico con ID {id}.");

        existing.Nombre = new NombreVO(dto.Nombre);
        existing.Telefono = dto.Telefono is null ? null : new TelefonoVO(dto.Telefono);
        existing.Especialidad = dto.Especialidad is null ? null : new EspecialidadVO(dto.Especialidad);
        existing.IsActive = dto.IsActive is null ? existing.IsActive : new EstadoVO(dto.IsActive.Value);

        var updated = await _mecanicoService.UpdateAsync(existing, ct);
        if (!updated)
            return BadRequest("No se pudo actualizar el mecánico.");

        return NoContent();
    }

    //  DELETE: api/Mecanicos/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var deleted = await _mecanicoService.DeleteAsync(new IdVO(id), ct);
        if (!deleted)
            return NotFound($"No se encontró el mecánico con ID {id}.");

        return NoContent();
    }
}
