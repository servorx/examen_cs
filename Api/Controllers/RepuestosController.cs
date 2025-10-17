using Api.DTOs.Repuestos;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

public class RepuestosController : BaseApiController
{
    private readonly IRepuestoService _service;
    private readonly IMapper _mapper;

    public RepuestosController(IRepuestoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    //  GET: api/Repuesto/all
    [HttpGet("all")]
    [Authorize(Roles = "Mecanico, Proveedor, Administrador")]
    [EnableRateLimiting("readCommon")]
    public async Task<ActionResult<IEnumerable<RepuestoDto>>> GetAllAsync(CancellationToken ct)
    {
        var repuestos = await _service.GetAllAsync(ct);
        var result = _mapper.Map<IEnumerable<RepuestoDto>>(repuestos);
        return Ok(result);
    }

    //  GET: api/Repuesto/{id}
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Mecanico, Proveedor, Administrador")]
    [EnableRateLimiting("readCommon")]
    public async Task<ActionResult<RepuestoDetailDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var repuesto = await _service.GetByIdAsync(new IdVO(id), ct);
        if (repuesto == null)
            return NotFound("Repuesto no encontrado.");

        var result = _mapper.Map<RepuestoDetailDto>(repuesto);
        return Ok(result);
    }

    //  POST: api/Repuesto
    [HttpPost]
    [Authorize(Roles = "Proveedor, Administrador")]
    [EnableRateLimiting("writeByRole")]
    public async Task<ActionResult<RepuestoDto>> CreateAsync([FromBody] CreateRepuestoDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");

        var repuesto = new Repuesto
        {
            Id = IdVO.CreateNew(),
            Codigo = new CodigoRepuestoVO(dto.Codigo),
            Descripcion = new DescripcionVO(dto.Descripcion),
            CantidadStock = new CantidadVO(dto.CantidadStock),
            PrecioUnitario = new DineroVO(dto.PrecioUnitario),
            ProveedorId = dto.ProveedorId.HasValue ? new IdVO(dto.ProveedorId.Value) : null
        };

        try
        {
            await _service.AddAsync(repuesto, ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var result = _mapper.Map<RepuestoDto>(repuesto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = repuesto.Id.Value }, result);
    }

    //  PUT: api/Repuesto/{id}
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Proveedor, Administrador")]
    [EnableRateLimiting("writeByRole")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateRepuestoDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");

        try
        {
            var existing = await _service.GetByIdAsync(new IdVO(id), ct);
            if (existing == null)
                return NotFound("Repuesto no encontrado.");

            existing.Codigo = new CodigoRepuestoVO(dto.Codigo);
            existing.Descripcion = new DescripcionVO(dto.Descripcion);
            existing.CantidadStock = new CantidadVO(dto.CantidadStock);
            existing.PrecioUnitario = new DineroVO(dto.PrecioUnitario);
            existing.ProveedorId = dto.ProveedorId.HasValue ? new IdVO(dto.ProveedorId.Value) : null;

            var updated = await _service.UpdateAsync(existing, ct);
            if (!updated)
                return BadRequest("No se pudo actualizar el repuesto.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //  DELETE: api/Repuesto/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Proveedor, Administrador")]
    [EnableRateLimiting("writeByRole")]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        try
        {
            var deleted = await _service.DeleteAsync(new IdVO(id), ct);
            if (!deleted)
                return NotFound("Repuesto no encontrado.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //  PATCH: api/Repuesto/{id}/stock
    [HttpPatch("{id:int}/stock")]
    [Authorize(Roles = "Proveedor, Administrador")]
    public async Task<ActionResult> UpdateStockAsync(int id, [FromQuery] int cantidad, CancellationToken ct)
    {
        try
        {
            var result = await _service.UpdateStockAsync(new IdVO(id), cantidad, ct);
            if (!result)
                return BadRequest("No se pudo actualizar el stock.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
