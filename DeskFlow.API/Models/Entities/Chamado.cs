using System.ComponentModel.DataAnnotations;
using DeskFlow.API.Models.Enums;

namespace DeskFlow.API.Models.Entities;

public class Chamado
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Titulo { get; set; } = string.Empty;

    [Required, MaxLength(2000)]
    public string Descricao { get; set; } = string.Empty;

    public Prioridade Prioridade { get; set; }
    public StatusChamado Status { get; set; } = StatusChamado.Aberto;

    [Required, MaxLength(100)]
    public string SolicitanteNome { get; set; } = string.Empty;

    public DateTime DataAbertura { get; set; }
    public DateTime? DataFechamento { get; set; }

    [MaxLength(2000)]
    public string? Solucao { get; set; }

    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    public ICollection<Interacao> Interacoes { get; set; } = new List<Interacao>();
}