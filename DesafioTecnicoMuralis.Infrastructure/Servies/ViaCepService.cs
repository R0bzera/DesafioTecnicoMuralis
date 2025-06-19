using AutoMapper;
using DesafioTecnicoMuralis.API.DTOs;
using DesafioTecnicoMuralis.Application.Interfaces.Service;
using DesafioTecnicoMuralis.Application.Retornos;
using DesafioTecnicoMuralis.Infrastructure.ExternalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Infrastructure.Servies
{
    public class ViaCepService : ICepService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ViaCepService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<Retorno<EnderecoDto>> BuscarEnderecoPorCepAsync(string cep)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ViaCepRetorno>($"https://viacep.com.br/ws/{cep}/json/");

                if (response is null || response.Erro)
                    return Retorno<EnderecoDto>.Falha("CEP inválido ou não encontrado.");

                var endereco = _mapper.Map<EnderecoDto>(response);
                endereco.Cep = cep;

                return Retorno<EnderecoDto>.Ok(endereco);
            }
            catch (Exception ex)
            {
                return Retorno<EnderecoDto>.Falha($"Erro ao consultar CEP: {ex.Message}");
            }
        }
    }
}