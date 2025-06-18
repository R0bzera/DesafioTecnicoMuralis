namespace DesafioTecnicoMuralis.API.DTOs
{
    public class ClienteDto
    {
        public string Nome { get; set; } = string.Empty;

        public List<EnderecoDto> Enderecos { get; set; } = new();
        public List<ContatoDto> Contatos { get; set; } = new();
    }
}