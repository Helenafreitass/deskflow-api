using DeskFlow.API.Data;
using DeskFlow.API.Models.Entities;
using DeskFlow.API.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DeskFlow.API.Repositories;

public class ChamadoRepository : IChamadoRepository
{
    private readonly AppDbContext _ctx;
    public ChamadoRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<Chamado> AdicionarAsync(Chamado chamado)
    {
        _ctx.Chamados.Add(chamado);
        await _ctx.SaveChangesAsync();
        return chamado;
    }

    public Task<Chamado?> ObterPorIdAsync(int id) =>
        _ctx.Chamados.Include(c => c.Categoria).FirstOrDefaultAsync(c => c.Id == id);

    public Task<Chamado?> ObterDetalhesAsync(int id) =>
        _ctx.Chamados
            .Include(c => c.Categoria)
            .Include(c => c.Interacoes)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Chamado>> ListarAsync(StatusChamado? status, Prioridade? prioridade, int? categoriaId)
    {
        var q = _ctx.Chamados.Include(c => c.Categoria).AsQueryable();
        if (status.HasValue) q = q.Where(c => c.Status == status.Value);
        if (prioridade.HasValue) q = q.Where(c => c.Prioridade == prioridade.Value);
        if (categoriaId.HasValue) q = q.Where(c => c.CategoriaId == categoriaId.Value);
        return await q.ToListAsync();
    }

    public async Task AtualizarAsync(Chamado chamado)
    {
        _ctx.Chamados.Update(chamado);
        await _ctx.SaveChangesAsync();
    }

    public async Task<Interacao> AdicionarInteracaoAsync(Interacao interacao)
    {
        _ctx.Interacoes.Add(interacao);
        await _ctx.SaveChangesAsync();
        return interacao;
    }
}