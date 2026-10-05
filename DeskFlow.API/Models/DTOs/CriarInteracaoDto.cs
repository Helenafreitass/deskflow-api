using System.ComponentModel.DataAnnotations;

namespace DeskFlow.API.Models.Dtos;

public class CriarInteracaoDto
{
    [Required, MaxLength(100)] public string Autor { get; set; } = string.Empty;
    [Required, MaxLength(2000)] public string Mensagem { get; set; } = string.Empty;
}