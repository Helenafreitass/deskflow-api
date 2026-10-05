using System.ComponentModel.DataAnnotations;
using DeskFlow.API.Models.Enums;

namespace DeskFlow.API.Models.Dtos;

public class CriarChamadoDto
{
    [Required, MaxLength(150)] public string Titulo { get; set; } = string.Empty;
    [Required, MaxLength(2000)] public string Descricao { get; set; } = string.Empty;
    [Required] public Prioridade Prioridade { get; set; }
    [Required, MaxLength(100)] public string SolicitanteNome { get; set; } = string.Empty;
    [Required] public int CategoriaId { get; set; }
}