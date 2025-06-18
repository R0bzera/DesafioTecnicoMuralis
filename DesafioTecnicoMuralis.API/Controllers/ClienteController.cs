using DesafioTecnicoMuralis.API.DTOs;
using DesafioTecnicoMuralis.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTecnicoMuralis.API.Controllers;

[ApiController]
[Route("api/cliente")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost("criar")]
    public async Task<IActionResult> Criar([FromBody] ClienteDto dto)
    {
        await _clienteService.CriarAsync(dto);
        return Created("", null);
    }

    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {
        var clientes = await _clienteService.ListarTodosAsync();
        return Ok(clientes);
    }

    [HttpGet("obter/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);
        if (cliente is null) return NotFound();
        return Ok(cliente);
    }

    [HttpPut("editar/{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ClienteDto dto)
    {
        try
        {
            await _clienteService.AtualizarAsync(id, dto);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("remover/{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        try
        {
            await _clienteService.RemoverAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}