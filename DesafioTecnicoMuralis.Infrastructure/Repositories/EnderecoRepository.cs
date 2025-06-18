using DesafioTecnicoMuralis.Application.Interfaces.Repository;
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

        public async Task AdicionarEnderecosAsync(int clienteId, List<EnderecoEntity> enderecos)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            foreach (var endereco in enderecos)
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Enderecos (Cep, Rua, Numero, Bairro, Cidade, Estado, ClienteId)
                                    VALUES ($cep, $rua, $numero, $bairro, $cidade, $estado, $clienteId);";

                cmd.Parameters.AddWithValue("$cep", endereco.Cep);
                cmd.Parameters.AddWithValue("$rua", endereco.Logadouro);
                cmd.Parameters.AddWithValue("$numero", endereco.Numero);
                cmd.Parameters.AddWithValue("$cidade", endereco.Cidade);
                cmd.Parameters.AddWithValue("$clienteId", clienteId);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
