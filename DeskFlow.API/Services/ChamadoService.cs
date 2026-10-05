using DeskFlow.API.Models.Dtos;
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Models.Enums;
using DeskFlow.API.Repositories;

namespace DeskFlow.API.Services;

public class ChamadoService : IChamadoService
{
    private readonly IChamadoRepository _repo;
    private readonly ICategoriaRepository _categoriaRepo;

    public ChamadoService(IChamadoRepository repo, ICategoriaRepository categoriaRepo)
    {
        _repo = repo;
        _categoriaRepo = categoriaRepo;
    }

    public async Task<Chamado> AbrirAsync(CriarChamadoDto dto)
    {
        var categoria = await _categoriaRepo.ObterPorIdAsync(dto.CategoriaId);
        if (categoria is null)
            throw new ArgumentException($"Categoria {dto.CategoriaId} não existe.");

        var chamado = new Chamado
        {
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Prioridade = dto.Prioridade,
            SolicitanteNome = dto.SolicitanteNome,
            CategoriaId = dto.CategoriaId,
            Status = StatusChamado.Aberto,
            DataAbertura = DateTime.Now
        };

        return await _repo.AdicionarAsync(chamado);
    }

    public async Task<Chamado> ObterDetalhesAsync(int id)
    {
        var chamado = await _repo.ObterDetalhesAsync(id);
        if (chamado is null) throw new KeyNotFoundException($"Chamado {id} não encontrado.");
        return chamado;
    }

    public Task<IEnumerable<Chamado>> ListarAsync(StatusChamado? s, Prioridade? p, int? c) =>
        _repo.ListarAsync(s, p, c);

    public async Task IniciarAsync(int id)
    {
        var chamado = await _repo.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException($"Chamado {id} não encontrado.");

        if (chamado.Status != StatusChamado.Aberto)
            throw new InvalidOperationException("Só é possível iniciar chamados com status Aberto.");

        chamado.Status = StatusChamado.EmAndamento;
        await _repo.AtualizarAsync(chamado);
    }

    public async Task EncerrarAsync(int id, EncerrarChamadoDto dto)
    {
        var chamado = await _repo.ObterPorIdAsync(id)
            ?? throw new KeyNotFoundException($"Chamado {id} não encontrado.");

        if (chamado.Status == StatusChamado.Fechado)
            throw new InvalidOperationException("Chamado já está fechado.");

        if (string.IsNullOrWhiteSpace(dto.Solucao))
            throw new ArgumentException("Solução é obrigatória para encerrar.");

        chamado.Status = StatusChamado.Fechado;
        chamado.Solucao = dto.Solucao;
        chamado.DataFechamento = DateTime.Now;
        await _repo.AtualizarAsync(chamado);
    }

    public async Task<Interacao> AdicionarInteracaoAsync(int chamadoId, CriarInteracaoDto dto)
    {
        var chamado = await _repo.ObterPorIdAsync(chamadoId)
            ?? throw new KeyNotFoundException($"Chamado {chamadoId} não encontrado.");

        if (chamado.Status == StatusChamado.Fechado)
            throw new InvalidOperationException("Não é possível comentar em chamado Fechado.");

        var interacao = new Interacao
        {
            ChamadoId = chamadoId,
            Autor = dto.Autor,
            Mensagem = dto.Mensagem,
            DataRegistro = DateTime.Now
        };

        return await _repo.AdicionarInteracaoAsync(interacao);
    }
}