using DeskFlow.API.Models.Entities;
using DeskFlow.API.Models.Enums;

namespace DeskFlow.API.Repositories;

public interface IChamadoRepository
{
    Task<Chamado> AdicionarAsync(Chamado chamado);
    Task<Chamado?> ObterPorIdAsync(int id);
    Task<Chamado?> ObterDetalhesAsync(int id);
    Task<IEnumerable<Chamado>> ListarAsync(StatusChamado? status, Prioridade? prioridade, int? categoriaId);
    Task AtualizarAsync(Chamado chamado);
    Task<Interacao> AdicionarInteracaoAsync(Interacao interacao);
}