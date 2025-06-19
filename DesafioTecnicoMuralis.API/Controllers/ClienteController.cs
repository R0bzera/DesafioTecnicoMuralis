using DesafioTecnicoMuralis.API.DTOs;
using DesafioTecnicoMuralis.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTecnicoMuralis.API.Controllers;

[ApiController]
[Route("cliente")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly ICepService _cepService;

    public ClienteController(IClienteService clienteService, ICepService cepService)
    {
        _clienteService = clienteService;
        _cepService = cepService;
    }

    [HttpPost("criar")]
    public async Task<IActionResult> Criar([FromBody] ClienteDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var resultado = await _clienteService.CriarAsync(dto);

            if (resultado.Sucesso)
                return Created(string.Empty, resultado.Dados);

            return BadRequest(resultado.Mensagem);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("listar")]
    public async Task<IActionResult> Listar()
    {
        try
        {
            var resultado = await _clienteService.ListarTodosAsync();

            if (resultado.Sucesso)
                return Ok(resultado.Dados);

            return BadRequest(resultado.Mensagem);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("obter/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        try
        {
            var resultado = await _clienteService.ObterPorIdAsync(id);

            if (resultado.Sucesso)
                return Ok(resultado.Dados);

            return BadRequest(resultado.Mensagem);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("editar/{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ClienteDto dto)
    {
        try
        {
            var resultado = await _clienteService.AtualizarAsync(id, dto);

            if (resultado.Sucesso)
                return Ok(resultado.Dados);

            return BadRequest(resultado.Mensagem);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("remover/{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        try
        {
            var resultado = await _clienteService.RemoverAsync(id);

            if (resultado.Sucesso)
                return Ok(resultado.Dados);

            return BadRequest(resultado.Mensagem);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("consultar-cep/{cep}")]
    public async Task<IActionResult> ConsultarCep(string cep)
    {
        try
        {
            var resultado = await _cepService.BuscarEnderecoPorCepAsync(cep);

            if (resultado.Sucesso)
                return Ok(resultado.Dados);

            return BadRequest(resultado.Mensagem);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}