﻿using DesafioTecnicoMuralis.Application.Retornos;
using DesafioTecnicoMuralis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Application.Interfaces.Repository
{
    public interface IEnderecoRepository
    {
        Task<Retorno<string>> AdicionarEnderecosAsync(int clienteId, List<EnderecoEntity> enderecos);
    }
}
