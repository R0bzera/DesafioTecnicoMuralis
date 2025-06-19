using System.ComponentModel.DataAnnotations;

namespace DesafioTecnicoMuralis.API.DTOs
{
    public class EnderecoDto
    {
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O CEP deve conter exatamente 8 dígitos numéricos sem caracteres especiais")]
        public string Cep { get; set; } = null!;

        [Required(ErrorMessage = "O número é obrigatório.")]
        public string Numero { get; set; } = null!;

        public string? Complemento { get; set; }

        public string? Logadouro { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
    }
}