using DesafioTecnicoMuralis.Application.Interfaces.Repository;
using DesafioTecnicoMuralis.Application.Retornos;
using DesafioTecnicoMuralis.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly string _connectionString;

        public EnderecoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<Retorno<string>> AdicionarEnderecosAsync(int clienteId, List<EnderecoEntity> enderecos)
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                await connection.OpenAsync();

                foreach (var endereco in enderecos)
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"INSERT INTO Enderecos (Cep, Logadouro, Numero, Complemento, Cidade, ClienteId) 
                                VALUES ($cep, $logadouro, $numero, $complemento, $cidade, $clienteId);";

                    cmd.Parameters.AddWithValue("$cep", endereco.Cep);
                    cmd.Parameters.AddWithValue("$logadouro", endereco.Logadouro);
                    cmd.Parameters.AddWithValue("$cidade", endereco.Cidade);
                    cmd.Parameters.AddWithValue("$numero", endereco.Numero);
                    cmd.Parameters.AddWithValue("$complemento", endereco.Complemento ?? string.Empty);
                    cmd.Parameters.AddWithValue("$clienteId", clienteId);

                    await cmd.ExecuteNonQueryAsync();
                }

                return Retorno<string>.Ok("Endereços adicionados com sucesso.");
            }
            catch (Exception ex)
            {
                return Retorno<string>.Falha($"Erro ao adicionar endereços: {ex.Message}");
            }
        }
    }
}