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
        Task<IEnumerable<ClienteEntity>> ListarTodosAsync();
        Task<ClienteEntity?> ObterPorIdAsync(int id);
        Task<int>AdicionarAsync(ClienteEntity cliente);
        Task AtualizarAsync(ClienteEntity cliente);
        Task RemoverAsync(int id);
    }
}
