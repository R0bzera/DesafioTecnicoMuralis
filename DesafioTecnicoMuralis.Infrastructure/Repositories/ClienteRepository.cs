using DesafioTecnicoMuralis.Application.Interfaces.Repository;
using DesafioTecnicoMuralis.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace DesafioTecnicoMuralis.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<IEnumerable<ClienteEntity>> ListarTodosAsync()
        {
            var clientes = new List<ClienteEntity>();

            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Nome, DataCadastro, DataAlteracao FROM Clientes;";

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                clientes.Add(new ClienteEntity
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    DataCadastro = reader.GetDateTime(2),
                    DataAlteracao = reader.GetDateTime(3)
                });
            }

            return clientes;
        }

        public async Task<ClienteEntity?> ObterPorIdAsync(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText =
                "SELECT Id, Nome, DataCadastro, DataAlteracao FROM Clientes WHERE Id = $id;";
            command.Parameters.AddWithValue("$id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new ClienteEntity
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    DataCadastro = reader.GetDateTime(2),
                    DataAlteracao = reader.GetDateTime(3)
                };
            }

            return null;
        }

        public async Task<int> AdicionarAsync(ClienteEntity cliente)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Clientes (Nome, DataCadastro, DataAlteracao)
                                    VALUES ($nome, $cadastro, $alteracao);
                                    SELECT last_insert_rowid();";

            command.Parameters.AddWithValue("$nome", cliente.Nome);
            command.Parameters.AddWithValue("$cadastro", cliente.DataCadastro);
            command.Parameters.AddWithValue("$alteracao", cliente.DataAlteracao);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result); // retorna o ID recém-gerado
        }


        public async Task AtualizarAsync(ClienteEntity cliente)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText =
                @"UPDATE Clientes
                  SET Nome = $nome, DataAlteracao = $alteracao
                  WHERE Id = $id;";

            command.Parameters.AddWithValue("$id", cliente.Id);
            command.Parameters.AddWithValue("$nome", cliente.Nome);
            command.Parameters.AddWithValue("$alteracao", cliente.DataAlteracao);

            await command.ExecuteNonQueryAsync();
        }

        public async Task RemoverAsync(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Clientes WHERE Id = $id;";
            command.Parameters.AddWithValue("$id", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}