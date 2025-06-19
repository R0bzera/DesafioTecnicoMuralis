using DesafioTecnicoMuralis.Domain.Enums;
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
        public TipoContato TipoContato { get; set; }
        public string Contato { get; set; }
        public int ClienteId { get; set; }
        public ClienteEntity Cliente { get; set; } = null!;
    }
}