using DeskFlow.API.Models.Dtos;
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Models.Enums;

namespace DeskFlow.API.Services;

public interface IChamadoService
{
    Task<Chamado> AbrirAsync(CriarChamadoDto dto);
    Task<Chamado> ObterDetalhesAsync(int id);
    Task<IEnumerable<Chamado>> ListarAsync(StatusChamado? status, Prioridade? prioridade, int? categoriaId);
    Task IniciarAsync(int id);
    Task EncerrarAsync(int id, EncerrarChamadoDto dto);
    Task<Interacao> AdicionarInteracaoAsync(int chamadoId, CriarInteracaoDto dto);
}