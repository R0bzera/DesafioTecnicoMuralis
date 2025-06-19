using DesafioTecnicoMuralis.API.DTOs;
using DesafioTecnicoMuralis.Application.DTOs;
using DesafioTecnicoMuralis.Application.Retornos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Application.Interfaces.Service
{
    public interface IClienteService
    {
        Task<Retorno<IEnumerable<ClienteCompletoDto>>> ListarTodosAsync();
        Task<Retorno<ClienteCompletoDto?>> ObterPorIdAsync(int id);
        Task<Retorno<string>> CriarAsync(ClienteDto dto);
        Task<Retorno<ClienteCompletoDto>> AtualizarAsync(int id, ClienteDto dto);
        Task<Retorno<string>> RemoverAsync(int id);
    }
}