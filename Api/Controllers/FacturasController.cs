using Api.DTOs.Facturas;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public sealed class FacturasController : BaseApiController
{
    private readonly IFacturaService _facturaService;
    private readonly IMapper _mapper;

    public FacturasController(IFacturaService facturaService, IMapper mapper)
    {
        _facturaService = facturaService;
        _mapper = mapper;
    }

    //  GET: api/facturas/all
    [HttpGet("all")]
    [Authorize(Roles = "Cliente, Administrador")]
    public async Task<ActionResult<IEnumerable<FacturaDto>>> GetAllAsync(CancellationToken ct)
    {
        var facturas = await _facturaService.GetAllAsync(ct);
        var result = _mapper.Map<IEnumerable<FacturaDto>>(facturas);
        return Ok(result);
    }

    //  GET: api/facturas/{id}
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Cliente, Administrador")]
    public async Task<ActionResult<FacturaDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var factura = await _facturaService.GetByIdAsync(new IdVO(id), ct);
        if (factura is null)
            return NotFound($"No se encontró la factura con ID {id}");

        var result = _mapper.Map<FacturaDto>(factura);
        return Ok(result);
    }

    //  GET: api/facturas/orden/{ordenServicioId}
    [HttpGet("orden/{ordenServicioId:int}")]
    [Authorize(Roles = "Cliente, Administrador")]
    public async Task<ActionResult<IEnumerable<FacturaDto>>> GetByOrdenServicioIdAsync(int ordenServicioId, CancellationToken ct)
    {
        var facturas = await _facturaService.GetByOrdenServicioIdAsync(new IdVO(ordenServicioId), ct);
        var result = _mapper.Map<IEnumerable<FacturaDto>>(facturas);
        return Ok(result);
    }

    //  POST: api/facturas
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> CreateAsync([FromBody] CreateFacturaDto dto, CancellationToken ct)
    {
        var factura = _mapper.Map<Factura>(dto);

        var result = await _facturaService.AddAsync(factura, ct);
        if (result <= 0)
            return BadRequest("No se pudo crear la factura");

        var createdDto = _mapper.Map<FacturaDto>(factura);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = factura.Id.Value }, createdDto);
    }

    //  PUT: api/facturas/{id}
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> UpdateAsync(int id, UpdateFacturaDto dto, CancellationToken ct)
    {
        var existing = await _facturaService.GetByIdAsync(new IdVO(id), ct);
        if (existing is null)
            return NotFound($"No se encontró la factura con ID {id}");

        _mapper.Map(dto, existing); // aplica los cambios del DTO sobre la entidad

        var updated = await _facturaService.UpdateAsync(existing, ct);
        if (!updated)
            return BadRequest("No se pudo actualizar la factura");

        return NoContent();
    }

    //  DELETE: api/facturas/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var deleted = await _facturaService.DeleteAsync(new IdVO(id), ct);
        if (!deleted)
            return NotFound($"No se encontró la factura con ID {id}");

        return NoContent();
    }

    //  GET: api/facturas/{id}/calcular-total
    [HttpGet("{id:int}/calcular-total")]
    [Authorize]
    public async Task<ActionResult<decimal>> CalcularTotalAsync(int id, CancellationToken ct)
    {
        var total = await _facturaService.CalcularTotalAsync(new IdVO(id), ct);
        return Ok(total);
    }
}
