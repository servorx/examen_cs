using Api.DTOs.HistorialesInventario;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class HistorialesInventarioController : BaseApiController
{
    private readonly IHistorialInventarioService _historialInventarioService;
    private readonly IMapper _mapper;

    public HistorialesInventarioController(
        IHistorialInventarioService historialInventarioService,
        IMapper mapper)
    {
        _historialInventarioService = historialInventarioService;
        _mapper = mapper;
    }

    //  GET: api/HistorialesInventario/all
    [HttpGet("all")]
    [Authorize(Roles = "Proveedor, Mecanico, Administrador")]
    public async Task<ActionResult<IEnumerable<HistorialInventarioDto>>> GetAll(CancellationToken ct)
    {
        var historiales = await _historialInventarioService.GetAllAsync(ct);
        var historialesDto = _mapper.Map<IEnumerable<HistorialInventarioDto>>(historiales);
        return Ok(historialesDto);
    }

    //  GET: api/HistorialesInventario/{id}
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Proveedor, Mecanico, Administrador")]
    public async Task<ActionResult<HistorialInventarioDto>> GetById(int id, CancellationToken ct)
    {
        var historial = await _historialInventarioService.GetByIdAsync(new IdVO(id), ct);
        if (historial is null)
            return NotFound($"No se encontró el historial con ID {id}");

        var dto = _mapper.Map<HistorialInventarioDto>(historial);
        return Ok(dto);
    }

    //  GET: api/HistorialesInventario/repuesto/{repuestoId}
    [HttpGet("repuesto/{repuestoId:int}")]
    [Authorize(Roles = "Proveedor, Mecanico, Administrador")]
    public async Task<ActionResult<IEnumerable<HistorialInventarioDto>>> GetByRepuestoId(int repuestoId, CancellationToken ct)
    {
        var historiales = await _historialInventarioService.GetByRepuestoIdAsync(new IdVO(repuestoId), ct);
        var dto = _mapper.Map<IEnumerable<HistorialInventarioDto>>(historiales);
        return Ok(dto);
    }

    //  GET: api/HistorialesInventario/admin/{adminId}
    [HttpGet("admin/{adminId:int}")]
    [Authorize(Roles = "Proveedor, Mecanico, Administrador")]
    public async Task<ActionResult<IEnumerable<HistorialInventarioDto>>> GetByAdminId(int adminId, CancellationToken ct)
    {
        var historiales = await _historialInventarioService.GetByAdminIdAsync(new IdVO(adminId), ct);
        var dto = _mapper.Map<IEnumerable<HistorialInventarioDto>>(historiales);
        return Ok(dto);
    }

    //  POST: api/HistorialesInventario
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<int>> Create([FromBody] CreateHistorialInventarioDto dto, CancellationToken ct)
    {
        var historial = new HistorialInventario(
            new IdVO(0),
            new IdVO(dto.RepuestoId),
            dto.AdminId.HasValue ? new IdVO(dto.AdminId.Value) : null,
            new IdVO(dto.TipoMovimientoId),
            new CantidadVO(dto.Cantidad),
            new FechaHistoricaVO(dto.FechaMovimiento),
            string.IsNullOrWhiteSpace(dto.Observaciones) ? null : new DescripcionVO(dto.Observaciones)
        );

        var newId = await _historialInventarioService.AddAsync(historial, ct);
        return CreatedAtAction(nameof(GetById), new { id = newId }, newId);
    }

    //  PUT: api/HistorialesInventario/{id}
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateHistorialInventarioDto dto, CancellationToken ct)
    {
        var historialExistente = await _historialInventarioService.GetByIdAsync(new IdVO(id), ct);
        if (historialExistente is null)
            return NotFound($"No se encontró el historial con ID {id}");

        historialExistente.Cantidad = new CantidadVO(dto.Cantidad);
        historialExistente.FechaMovimiento = new FechaHistoricaVO(dto.FechaMovimiento);
        historialExistente.Observaciones = string.IsNullOrWhiteSpace(dto.Observaciones)
            ? null
            : new DescripcionVO(dto.Observaciones);

        var actualizado = await _historialInventarioService.UpdateAsync(historialExistente, ct);
        if (!actualizado)
            return BadRequest("No se pudo actualizar el historial de inventario.");

        return NoContent();
    }

    //  DELETE: api/HistorialesInventario/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> Delete(int id, CancellationToken ct)
    {
        var eliminado = await _historialInventarioService.DeleteAsync(new IdVO(id), ct);
        if (!eliminado)
            return NotFound($"No se encontró el historial con ID {id}");

        return NoContent();
    }
}