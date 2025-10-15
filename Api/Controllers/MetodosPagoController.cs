using Api.DTOs.MetodosPago;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Administrador")]
public class MetodosPagoController : BaseApiController
{
    private readonly IMetodoPagoService _service;
    private readonly IMapper _mapper;

    public MetodosPagoController(IMetodoPagoService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    //  GET: api/MetodoPago/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<MetodoPagoDto>>> GetAllAsync(CancellationToken ct)
    {
        var metodos = await _service.ObtenerTodosAsync(ct);
        var result = _mapper.Map<IEnumerable<MetodoPagoDto>>(metodos);
        return Ok(result);
    }

    //  GET: api/MetodoPago/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MetodoPagoDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var metodo = await _service.ObtenerPorIdAsync(new IdVO(id), ct);
        if (metodo is null)
            return NotFound("Método de pago no encontrado.");

        var result = _mapper.Map<MetodoPagoDto>(metodo);
        return Ok(result);
    }

    // GET: api/MetodoPago/nombre/{nombre}
    [HttpGet("nombre/{nombre}")]
    public async Task<ActionResult<MetodoPagoDto>> GetByNombreAsync(string nombre, CancellationToken ct)
    {
        var metodo = await _service.ObtenerPorNombreAsync(new NombreVO(nombre), ct);
        if (metodo is null)
            return NotFound("Método de pago no encontrado.");

        var result = _mapper.Map<MetodoPagoDto>(metodo);
        return Ok(result);
    }

    //  POST: api/MetodoPago
    [HttpPost]
    public async Task<ActionResult<MetodoPagoDto>> CreateAsync([FromBody] CreateMetodoPagoDto dto, CancellationToken ct)
    {
        if (dto is null)
            return BadRequest("Datos inválidos.");

        var metodo = new MetodoPago(
            IdVO.CreateNew(),
            new NombreVO(dto.Nombre)
        );

        var id = await _service.CrearAsync(metodo, ct);
        var result = _mapper.Map<MetodoPagoDto>(metodo);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = metodo.Id.Value }, result);
    }

    //  PUT: api/MetodoPago/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateMetodoPagoDto dto, CancellationToken ct)
    {
        if (dto is null)
            return BadRequest("Datos inválidos.");

        var existing = await _service.ObtenerPorIdAsync(new IdVO(id), ct);
        if (existing is null)
            return NotFound("Método de pago no encontrado.");

        existing.Nombre = new NombreVO(dto.Nombre);
        var updated = await _service.ActualizarAsync(existing, ct);

        if (!updated)
            return BadRequest("No se pudo actualizar el método de pago.");

        return NoContent();
    }

    //  DELETE: api/MetodoPago/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var deleted = await _service.EliminarAsync(new IdVO(id), ct);
        if (!deleted)
            return NotFound("Método de pago no encontrado.");

        return NoContent();
    }
}
