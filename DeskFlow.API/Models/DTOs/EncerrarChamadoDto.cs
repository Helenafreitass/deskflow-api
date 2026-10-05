using System.ComponentModel.DataAnnotations;

namespace DeskFlow.API.Models.Dtos;

public class EncerrarChamadoDto
{
    [Required, MaxLength(2000)] public string Solucao { get; set; } = string.Empty;
}