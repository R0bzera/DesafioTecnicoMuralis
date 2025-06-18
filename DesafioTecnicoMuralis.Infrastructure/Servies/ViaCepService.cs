using DesafioTecnicoMuralis.API.DTOs;
using DesafioTecnicoMuralis.Application.Interfaces.Service;
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

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EnderecoDto?> BuscarEnderecoPorCepAsync(string cep)
        {
            var response = await _httpClient.GetFromJsonAsync<ViaCepResponse>($"https://viacep.com.br/ws/{cep}/json/");
            if (response is null || response.Erro) return null;

            return new EnderecoDto
            {
                Cep = cep,
                Rua = response.Logradouro,
                Bairro = response.Bairro,
                Cidade = response.Localidade,
                Estado = response.Uf,
                Numero = string.Empty
            };
        }
    }

    public class ViaCepResponse
    {
        public string Logradouro { get; set; } = "";
        public string Bairro { get; set; } = "";
        public string Localidade { get; set; } = "";
        public string Uf { get; set; } = "";
        public bool Erro { get; set; } = false;
    }
}