using Api.DTOs.Pagos;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class PagosController : BaseApiController
{
    private readonly IPagoService _service;
    private readonly IMapper _mapper;

    public PagosController(IPagoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    //  GET: api/Pago/all
    [HttpGet("all")]
    [Authorize(Roles = "Cliente, Administrador")]
    public async Task<ActionResult<IEnumerable<PagoDto>>> GetAllAsync(CancellationToken ct)
    {
        var pagos = await _service.GetAllAsync(ct);
        var result = _mapper.Map<IEnumerable<PagoDto>>(pagos);
        return Ok(result);
    }

    //  GET: api/Pago/{id}
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Cliente, Administrador")]
    public async Task<ActionResult<PagoDetailDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var pago = await _service.GetByIdAsync(new IdVO(id), ct);
        if (pago == null)
            return NotFound("Pago no encontrado.");

        var result = _mapper.Map<PagoDetailDto>(pago);
        return Ok(result);
    }

    //  GET: api/Pago/factura/{facturaId}
    [HttpGet("factura/{facturaId:int}")]
    [Authorize(Roles = "Cliente, Administrador")]
    public async Task<ActionResult<IEnumerable<PagoDto>>> GetByFacturaAsync(int facturaId, CancellationToken ct)
    {
        var pagos = await _service.GetByFacturaIdAsync(new IdVO(facturaId), ct);
        var result = _mapper.Map<IEnumerable<PagoDto>>(pagos);
        return Ok(result);
    }

    //  POST: api/Pago
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<PagoDto>> CreateAsync([FromBody] CreatePagoDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");

        var pago = new Pago
        {
            Id = IdVO.CreateNew(),
            FacturaId = new IdVO(dto.FacturaId),
            MetodoPagoId = new IdVO(dto.MetodoPagoId),
            EstadoPagoId = new IdVO(dto.EstadoPagoId),
            Monto = new DineroVO(dto.Monto),
            FechaPago = new FechaHistoricaVO(dto.FechaPago)
        };

        try
        {
            await _service.AddAsync(pago, ct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        var result = _mapper.Map<PagoDto>(pago);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = pago.Id.Value }, result);
    }

    //  PUT: api/Pago/{id}
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdatePagoDto dto, CancellationToken ct)
    {
        if (dto == null)
            return BadRequest("Datos inválidos.");

        try
        {
            // Obtener el pago existente
            var existingPago = await _service.GetByIdAsync(new IdVO(id), ct);
            if (existingPago == null)
                return NotFound("Pago no encontrado.");

            // Actualizar los valores con Value Objects
            existingPago.FacturaId = new IdVO(dto.FacturaId);
            existingPago.MetodoPagoId = new IdVO(dto.MetodoPagoId);
            existingPago.EstadoPagoId = new IdVO(dto.EstadoPagoId);
            existingPago.Monto = new DineroVO(dto.Monto);
            existingPago.FechaPago = new FechaHistoricaVO(dto.FechaPago);

            // Llamar al servicio para actualizar
            var updated = await _service.UpdateAsync(existingPago, ct);
            if (!updated)
                return BadRequest("No se pudo actualizar el pago.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //  DELETE: api/Pago/{id}
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        try
        {
            var deleted = await _service.DeleteAsync(new IdVO(id), ct);
            if (!deleted)
                return NotFound("Pago no encontrado.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return NoContent();
    }
}
