using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Infrastructure.ExternalEntities
{
    public class ViaCepRetorno
    {
        public string Logradouro { get; set; } = "";
        public string Bairro { get; set; } = "";
        public string Localidade { get; set; } = "";
        public string Uf { get; set; } = "";
        public bool Erro { get; set; } = false;
    }
}
