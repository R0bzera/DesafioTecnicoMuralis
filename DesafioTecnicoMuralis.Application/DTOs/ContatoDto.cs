using System.ComponentModel.DataAnnotations;

namespace DesafioTecnicoMuralis.API.DTOs
{
    public class ContatoDto
    {
        [Required(ErrorMessage = "O tipo de contato é obrigatório.")]
        [Range(1, 2, ErrorMessage = "Tipo de contato inválido. Use 1 para Email, 2 para Telefone.")]
        public int TipoContato { get; set; }

        [Required(ErrorMessage = "O contato é obrigatório.")]
        public string Contato { get; set; } = null!;
    }
}