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
    public class ContatoRepository : IContatoRepository
    {
        private readonly string _connectionString;

        public ContatoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<Retorno<string>> AdicionarContatosAsync(int clienteId, IEnumerable<ContatoEntity> contatos)
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                await connection.OpenAsync();

                foreach (var contato in contatos)
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"INSERT INTO Contatos (TipoContatoId, Contato, ClienteId)
                                    VALUES ($tipoContatoId, $contato, $clienteId);";

                    cmd.Parameters.AddWithValue("$tipoContatoId", (int)contato.TipoContato);
                    cmd.Parameters.AddWithValue("$contato", contato.Contato);
                    cmd.Parameters.AddWithValue("$clienteId", clienteId);

                    await cmd.ExecuteNonQueryAsync();
                }

                return Retorno<string>.Ok("Contatos adicionados com sucesso.");
            }
            catch (Exception ex)
            {
                return Retorno<string>.Falha($"Erro ao adicionar contatos: {ex.Message}");
            }
        }
    }
}
