using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Application.DTOs
{
    public class ClienteCompletoDto
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }

        public string Cep { get; set; } = string.Empty;
        public string Logadouro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;

        public string Contato { get; set; } = string.Empty;
    }
}
