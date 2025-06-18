using DesafioTecnicoMuralis.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Application.Interfaces.Service
{
    public interface IClienteService
    {
        Task CriarAsync(ClienteDto dto);
        Task<IEnumerable<ClienteDto>> ListarTodosAsync();
        Task<ClienteDto?> ObterPorIdAsync(int id);
        Task AtualizarAsync(int id, ClienteDto dto);
        Task RemoverAsync(int id);
    }
}