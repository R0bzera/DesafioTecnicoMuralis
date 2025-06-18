using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Domain.Entities
{
    public class ContatoEntity
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }

        public int ClienteId { get; set; }
        public ClienteEntity Cliente { get; set; } = null!;
    }
}
