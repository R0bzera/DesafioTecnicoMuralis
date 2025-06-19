using DesafioTecnicoMuralis.API.DTOs;
using DesafioTecnicoMuralis.Application.Retornos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Application.Interfaces.Service
{
    public interface ICepService
    {
        Task<Retorno<EnderecoDto>> BuscarEnderecoPorCepAsync(string cep);
    }
}
