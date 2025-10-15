namespace Api.DTOs.Citas;

public record CitaDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNombre { get; set; } = string.Empty;
    public int VehiculoId { get; set; }
    public string VehiculoPlaca { get; set; } = string.Empty;
    public DateTime FechaCita { get; set; }
    public string? Motivo { get; set; }
    public int EstadoId { get; set; }
    public string EstadoNombre { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

