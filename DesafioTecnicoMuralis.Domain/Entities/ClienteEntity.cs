using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Domain.Entities
{
    public class ClienteEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao{ get; set; }
        public ICollection<EnderecoEntity> Enderecos { get; set; } = new List<EnderecoEntity>();
        public ICollection<ContatoEntity> Contatos { get; set; } = new List<ContatoEntity>();
    }
}