using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Domain.Entities
{
    public class EnderecoEntity
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logadouro { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int ClienteId { get; set; }

        public ClienteEntity Cliente { get; set; } = null!;
    }
}