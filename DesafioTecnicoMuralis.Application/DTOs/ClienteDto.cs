using System.ComponentModel.DataAnnotations;

namespace DesafioTecnicoMuralis.API.DTOs
{
    public class ClienteDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Pelo menos um endereço deve ser informado.")]
        public List<EnderecoDto> Enderecos { get; set; } = new();

        [Required(ErrorMessage = "Pelo menos um contato deve ser informado.")]
        public List<ContatoDto> Contatos { get; set; } = new();
    }
}