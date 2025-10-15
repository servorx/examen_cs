using Api.DTOs.EstadosPago;
using Api.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Administrador")]
public sealed class EstadosPagoController : BaseApiController
{
    private readonly IEstadoPagoService _estadoPagoService;
    private readonly IMapper _mapper;

    public EstadosPagoController(IEstadoPagoService estadoPagoService, IMapper mapper)
    {
        _estadoPagoService = estadoPagoService;
        _mapper = mapper;
    }

    //  GET: api/EstadoPago/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<EstadoPagoDto>>> GetAllAsync(CancellationToken ct)
    {
        var estados = await _estadoPagoService.ObtenerTodosAsync(ct);
        var result = _mapper.Map<IEnumerable<EstadoPagoDto>>(estados);
        return Ok(result);
    }

    //  GET: api/EstadoPago/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EstadoPagoDto>> GetByIdAsync(int id, CancellationToken ct)
    {
        var estado = await _estadoPagoService.ObtenerPorIdAsync(new IdVO(id), ct);
        if (estado is null)
            return NotFound(new { message = $"No se encontró un estado con ID {id}." });

        var result = _mapper.Map<EstadoPagoDto>(estado);
        return Ok(result);
    }

    //  GET: api/EstadoPago/nombre/{nombre}
    [HttpGet("nombre/{nombre}")]
    public async Task<ActionResult<EstadoPagoDto>> GetByNombreAsync(string nombre, CancellationToken ct)
    {
        var estado = await _estadoPagoService.ObtenerPorNombreAsync(new NombreVO(nombre), ct);
        if (estado is null)
            return NotFound(new { message = $"No se encontró un estado con el nombre '{nombre}'." });

        var result = _mapper.Map<EstadoPagoDto>(estado);
        return Ok(result);
    }

    //  POST: api/EstadoPago
    [HttpPost]
    public async Task<ActionResult<EstadoPagoDto>> CreateAsync([FromBody] CreateEstadoPagoDto dto, CancellationToken ct)
    {
        try
        {
            var estado = new EstadoPago(
                new IdVO(0),
                new NombreVO(dto.Nombre)
            );

            await _estadoPagoService.CrearAsync(estado, ct);

            var result = _mapper.Map<EstadoPagoDto>(estado);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = estado.Id.Value }, result);
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    //  PUT: api/EstadoPago/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateEstadoPagoDto dto, CancellationToken ct)
    {
        try
        {
            var estado = new EstadoPago(
                new IdVO(id),
                new NombreVO(dto.Nombre)
            );

            var actualizado = await _estadoPagoService.ActualizarAsync(estado, ct);
            if (!actualizado)
                return NotFound(new { message = $"No se pudo actualizar el estado con ID {id}." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    //  DELETE: api/EstadoPago/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken ct)
    {
        var eliminado = await _estadoPagoService.EliminarAsync(new IdVO(id), ct);
        if (!eliminado)
            return NotFound(new { message = $"No se encontró el estado con ID {id}." });

        return NoContent();
    }
}
