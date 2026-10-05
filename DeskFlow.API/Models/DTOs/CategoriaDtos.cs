using System.ComponentModel.DataAnnotations;

namespace DeskFlow.API.Models.DTOs;

public class CategoriaRequest
{
    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
}

public record CategoriaResponse(int Id, string Nome);