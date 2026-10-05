using DeskFlow.API.Models.Dtos;
using DeskFlow.API.Models.Enums;
using DeskFlow.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeskFlow.API.Controllers;

[ApiController]
[Route("api/chamados")]
public class ChamadosController : ControllerBase
{
    private readonly IChamadoService _service;
    public ChamadosController(IChamadoService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Abrir([FromBody] CriarChamadoDto dto)
    {
        var chamado = await _service.AbrirAsync(dto);
        return CreatedAtAction(nameof(Detalhes), new { id = chamado.Id }, chamado);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detalhes(int id) =>
        Ok(await _service.ObterDetalhesAsync(id));

    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] StatusChamado? status,
        [FromQuery] Prioridade? prioridade,
        [FromQuery] int? categoriaId) =>
        Ok(await _service.ListarAsync(status, prioridade, categoriaId));

    [HttpPatch("{id:int}/iniciar")]
    public async Task<IActionResult> Iniciar(int id)
    {
        await _service.IniciarAsync(id);
        return NoContent();
    }

    [HttpPatch("{id:int}/encerrar")]
    public async Task<IActionResult> Encerrar(int id, [FromBody] EncerrarChamadoDto dto)
    {
        await _service.EncerrarAsync(id, dto);
        return NoContent();
    }

    [HttpPost("{id:int}/interacoes")]
    public async Task<IActionResult> AdicionarInteracao(int id, [FromBody] CriarInteracaoDto dto)
    {
        var interacao = await _service.AdicionarInteracaoAsync(id, dto);
        return CreatedAtAction(nameof(Detalhes), new { id }, interacao);
    }
}