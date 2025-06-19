using DesafioTecnicoMuralis.Application.DTOs;
using DesafioTecnicoMuralis.Application.Interfaces.Repository;
using DesafioTecnicoMuralis.Application.Retornos;
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

        public async Task<Retorno<IEnumerable<ClienteCompletoDto>>> ListarTodosAsync()
        {
            try
            {
                var clientes = new List<ClienteCompletoDto>();

                using var connection = new SqliteConnection(_connectionString);
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT C.Id, C.Nome, C.DataCadastro, E.Cep, E.Logadouro, E.Cidade, E.Numero, E.Complemento, CON.Contato 
                                    FROM Clientes C
                                    JOIN Enderecos E on E.ClienteId = C.Id
                                    JOIN Contatos CON on CON.ClienteId = C.Id";

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    clientes.Add(new ClienteCompletoDto
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        DataCadastro = reader.GetDateTime(2),
                        Cep = reader.GetString(3),
                        Logadouro = reader.GetString(4),
                        Cidade = reader.GetString(5),
                        Numero = reader.GetString(6),
                        Complemento = reader.GetString(7),
                        Contato = reader.GetString(8)
                    });
                }

                return Retorno<IEnumerable<ClienteCompletoDto>>.Ok(clientes);
            }
            catch (Exception ex)
            {
                return Retorno<IEnumerable<ClienteCompletoDto>>.Falha($"Erro ao listar clientes: {ex.Message}");
            }
        }

        public async Task<Retorno<ClienteCompletoDto?>> ObterPorIdAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT C.Id, C.Nome, C.DataCadastro, E.Cep, E.Logadouro, E.Cidade, E.Numero, E.Complemento, CON.Contato 
                                    FROM Clientes C
                                    JOIN Enderecos E on E.ClienteId = C.Id
                                    JOIN Contatos CON on CON.ClienteId = C.Id
                                    WHERE C.Id = $id;";
                command.Parameters.AddWithValue("$id", id);

                using var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    var dto = new ClienteCompletoDto
                    {
                        Id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        DataCadastro = reader.GetDateTime(2),
                        Cep = reader.GetString(3),
                        Logadouro = reader.GetString(4),
                        Cidade = reader.GetString(5),
                        Numero = reader.GetString(6),
                        Complemento = reader.GetString(7),
                        Contato = reader.GetString(8)
                    };

                    return Retorno<ClienteCompletoDto?>.Ok(dto);
                }

                return Retorno<ClienteCompletoDto?>.Falha("Cliente não encontrado.");
            }
            catch (Exception ex)
            {
                return Retorno<ClienteCompletoDto?>.Falha($"Erro ao obter cliente: {ex.Message}");
            }
        }

        public async Task<Retorno<int>> AdicionarAsync(ClienteEntity cliente)
        {
            try
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
                return Retorno<int>.Ok(Convert.ToInt32(result));
            }
            catch (Exception ex)
            {
                return Retorno<int>.Falha($"Erro ao adicionar cliente: {ex.Message}");
            }
        }

        public async Task<Retorno<bool>> AtualizarAsync(ClienteEntity cliente)
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                await connection.OpenAsync();
                var transaction = (SqliteTransaction)await connection.BeginTransactionAsync();

                var cmdCliente = connection.CreateCommand();
                cmdCliente.Transaction = transaction;
                cmdCliente.CommandText = @"UPDATE Clientes SET Nome = $nome, DataAlteracao = $alteracao WHERE Id = $id;";
                cmdCliente.Parameters.AddWithValue("$id", cliente.Id);
                cmdCliente.Parameters.AddWithValue("$nome", cliente.Nome);
                cmdCliente.Parameters.AddWithValue("$alteracao", cliente.DataAlteracao);
                await cmdCliente.ExecuteNonQueryAsync();

                foreach (var endereco in cliente.Enderecos)
                {
                    var cmdEndereco = connection.CreateCommand();
                    cmdEndereco.Transaction = transaction;
                    cmdEndereco.CommandText = @"UPDATE Enderecos
                                            SET Cep = $cep, Logadouro = $logadouro, Numero = $numero,
                                                Complemento = $complemento, Cidade = $cidade
                                            WHERE ClienteId = $clienteId;";
                    cmdEndereco.Parameters.AddWithValue("$clienteId", cliente.Id);
                    cmdEndereco.Parameters.AddWithValue("$cep", endereco.Cep);
                    cmdEndereco.Parameters.AddWithValue("$logadouro", endereco.Logadouro);
                    cmdEndereco.Parameters.AddWithValue("$numero", endereco.Numero);
                    cmdEndereco.Parameters.AddWithValue("$complemento", endereco.Complemento ?? "");
                    cmdEndereco.Parameters.AddWithValue("$cidade", endereco.Cidade);
                    await cmdEndereco.ExecuteNonQueryAsync();
                }

                foreach (var contato in cliente.Contatos)
                {
                    var cmdContato = connection.CreateCommand();
                    cmdContato.Transaction = transaction;
                    cmdContato.CommandText = @"UPDATE Contatos
                                           SET TipoContatoId = $tipoContatoId, Contato = $contato
                                           WHERE ClienteId = $clienteId;";
                    cmdContato.Parameters.AddWithValue("$clienteId", cliente.Id);
                    cmdContato.Parameters.AddWithValue("$tipoContatoId", (int)contato.TipoContato);
                    cmdContato.Parameters.AddWithValue("$contato", contato.Contato);
                    await cmdContato.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
                return Retorno<bool>.Ok(true, "Cliente atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return Retorno<bool>.Falha($"Erro ao atualizar cliente: {ex.Message}");
            }
        }

        public async Task<Retorno<bool>> RemoverAsync(int id)
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                await connection.OpenAsync();
                using var transaction = connection.BeginTransaction();

                var cmdEnderecos = connection.CreateCommand();
                cmdEnderecos.Transaction = transaction;
                cmdEnderecos.CommandText = "DELETE FROM Enderecos WHERE ClienteId = $id;";
                cmdEnderecos.Parameters.AddWithValue("$id", id);
                await cmdEnderecos.ExecuteNonQueryAsync();

                var cmdContatos = connection.CreateCommand();
                cmdContatos.Transaction = transaction;
                cmdContatos.CommandText = "DELETE FROM Contatos WHERE ClienteId = $id;";
                cmdContatos.Parameters.AddWithValue("$id", id);
                await cmdContatos.ExecuteNonQueryAsync();

                var cmdCliente = connection.CreateCommand();
                cmdCliente.Transaction = transaction;
                cmdCliente.CommandText = "DELETE FROM Clientes WHERE Id = $id;";
                cmdCliente.Parameters.AddWithValue("$id", id);
                await cmdCliente.ExecuteNonQueryAsync();

                await transaction.CommitAsync();
                return Retorno<bool>.Ok(true, "Cliente removido com sucesso.");
            }
            catch (Exception ex)
            {
                return Retorno<bool>.Falha($"Erro ao remover cliente: {ex.Message}");
            }
        }
    }
}