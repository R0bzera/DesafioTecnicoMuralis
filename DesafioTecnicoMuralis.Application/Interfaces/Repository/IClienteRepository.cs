using DesafioTecnicoMuralis.Application.DTOs;
using DesafioTecnicoMuralis.Application.Retornos;
using DesafioTecnicoMuralis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Application.Interfaces.Repository
{
    public interface IClienteRepository
    {
        Task<Retorno<IEnumerable<ClienteCompletoDto>>> ListarTodosAsync();
        Task<Retorno<ClienteCompletoDto?>> ObterPorIdAsync(int id);
        Task<Retorno<int>> AdicionarAsync(ClienteEntity cliente);
        Task<Retorno<bool>> AtualizarAsync(ClienteEntity cliente);
        Task<Retorno<bool>> RemoverAsync(int id);
    }
}
