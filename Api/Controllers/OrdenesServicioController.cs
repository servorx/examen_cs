using Api.DTOs.OrdenesServicio;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[Authorize(Roles = "Mecanico, Administrador")]
public class OrdenesServicioController : BaseApiController
{
    private readonly IOrdenServicioService _service;
    private readonly IMapper _mapper;

    public OrdenesServicioController(IOrdenServicioService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET: api/OrdenServicio/all
    [HttpGet("all")]
    [EnableRateLimiting("readCommon")]
    public async Task<ActionResult<IEnumerable<OrdenServicioDto>>> GetAllAsync(CancellationToken ct)
    {
        var ordenes = await _service.GetAllAsync(ct);
        var result = _mapper.Map<IEnumerable<OrdenServicioDto>>(ordenes);
        return Ok(result);
    }

    // GET: api/OrdenServicio/{id}
    [HttpGet("{id:int}")]
    [EnableRateLimiting("readCommon")]
    public async Task<ActionResult<OrdenServicioDetailDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var orden = await _service.GetByIdAsync(new IdVO(id), ct);
        if (orden is null)
            return NotFound("Orden de servicio no encontrada.");

        var result = _mapper.Map<OrdenServicioDetailDto>(orden);
        return Ok(result);
    }

    // GET: api/OrdenServicio/vehiculo/{vehiculoId}
    [HttpGet("vehiculo/{vehiculoId:int}")]
    [EnableRateLimiting("readCommon")]
    public async Task<ActionResult<IEnumerable<OrdenServicioDto>>> GetByVehiculoAsync(int vehiculoId, CancellationToken ct)
    {
        var ordenes = await _service.GetByVehiculoAsync(new IdVO(vehiculoId), ct);
        var result = _mapper.Map<IEnumerable<OrdenServicioDto>>(ordenes);
        return Ok(result);
    }

    // GET: api/OrdenServicio/mecanico/{mecanicoId}
    [HttpGet("mecanico/{mecanicoId:int}")]
    [EnableRateLimiting("readCommon")]
    public async Task<ActionResult<IEnumerable<OrdenServicioDto>>> GetByMecanicoAsync(int mecanicoId, CancellationToken ct)
    {
        var ordenes = await _service.GetByMecanicoAsync(new IdVO(mecanicoId), ct);
        var result = _mapper.Map<IEnumerable<OrdenServicioDto>>(ordenes);
        return Ok(result);
    }

    // POST: api/OrdenServicio
    [HttpPost]
    [EnableRateLimiting("writeByRole")]
    public async Task<ActionResult<OrdenServicioDto>> CreateAsync([FromBody] CreateOrdenServicioDto dto, CancellationToken ct)
    {
        if (dto is null)
            return BadRequest("Datos inválidos.");

        var orden = new OrdenServicio(
            IdVO.CreateNew(),
            new Vehiculo { Id = new IdVO(dto.VehiculoId) },
            new Mecanico { Id = new IdVO(dto.MecanicoId) },
            new Recepcionista { Id = new IdVO(dto.RecepcionistaId) },
            new TipoServicio { Id = new IdVO(dto.TipoServicioId) },
            new EstadoOrden { Id = new IdVO(dto.EstadoId) },
            new FechaHistoricaVO(dto.FechaIngreso),
            new FechaHistoricaVO(dto.FechaEntregaEstimada)
        );

        try
        {
            var id = await _service.AddAsync(orden, ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var result = _mapper.Map<OrdenServicioDto>(orden);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = orden.Id.Value }, result);
    }

    //PUT: api/OrdenServicio/{id}
    [HttpPut("{id:int}")]
    [EnableRateLimiting("writeByRole")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateOrdenServicioDto dto, CancellationToken ct)
    {
        if (dto is null)
            return BadRequest("Datos inválidos.");

        var existing = await _service.GetByIdAsync(new IdVO(id), ct);
        if (existing is null)
            return NotFound("Orden de servicio no encontrada.");

        existing.VehiculoId = new IdVO(dto.VehiculoId);
        existing.MecanicoId = new IdVO(dto.MecanicoId);
        existing.TipoServicioId = new IdVO(dto.TipoServicioId);
        existing.EstadoId = new IdVO(dto.EstadoId);
        existing.FechaIngreso = new FechaHistoricaVO(dto.FechaIngreso);
        existing.FechaEntregaEstimada = new FechaHistoricaVO(dto.FechaEntregaEstimada);

        try
        {
            var updated = await _service.UpdateAsync(existing, ct);
            if (!updated)
                return BadRequest("No se pudo actualizar la orden de servicio.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }

    // DELETE: api/OrdenServicio/{id}
    [HttpDelete("{id:int}")]
    [EnableRateLimiting("writeByRole")]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(new IdVO(id), ct);
        if (!deleted)
            return NotFound("Orden de servicio no encontrada.");

        return NoContent();
    }
}
