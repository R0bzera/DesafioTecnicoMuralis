using AutoMapper;
using DesafioTecnicoMuralis.API.DTOs;
using DesafioTecnicoMuralis.Application.DTOs;
using DesafioTecnicoMuralis.Application.Interfaces.Repository;
using DesafioTecnicoMuralis.Application.Interfaces.Service;
using DesafioTecnicoMuralis.Application.Retornos;
using DesafioTecnicoMuralis.Domain.Entities;

namespace DesafioTecnicoMuralis.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICepService _cepService;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IContatoRepository _contatoRepository;

        public ClienteService(
            IClienteRepository repository,
            IMapper mapper,
            ICepService cepService,
            IEnderecoRepository enderecoRepository,
            IContatoRepository contatoRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _cepService = cepService;
            _enderecoRepository = enderecoRepository;
            _contatoRepository = contatoRepository;
        }

        public async Task<Retorno<IEnumerable<ClienteCompletoDto>>> ListarTodosAsync()
        {
            try
            {
                var resultado = await _repository.ListarTodosAsync();

                if (!resultado.Sucesso || resultado.Dados == null || !resultado.Dados.Any())
                    return Retorno<IEnumerable<ClienteCompletoDto>>.Falha(resultado.Mensagem ?? "Nenhum cliente encontrado.");

                return Retorno<IEnumerable<ClienteCompletoDto>>.Ok(resultado.Dados);
            }
            catch (Exception ex)
            {
                return Retorno<IEnumerable<ClienteCompletoDto>>.Falha($"Erro ao listar clientes: {ex.Message}");
            }
        }

        public async Task<Retorno<ClienteCompletoDto?>> ObterPorIdAsync(int id)
        {
            var resultado = await _repository.ObterPorIdAsync(id);

            if (!resultado.Sucesso || resultado.Dados is null)
                return Retorno<ClienteCompletoDto?>.Falha(resultado.Mensagem ?? "Cliente não encontrado.");

            return Retorno<ClienteCompletoDto?>.Ok(resultado.Dados);
        }

        public async Task<Retorno<string>> CriarAsync(ClienteDto dto)
        {
            try
            {
                if (dto.Enderecos?.Any() == true)
                {
                    foreach (var endereco in dto.Enderecos)
                    {
                        if (!string.IsNullOrWhiteSpace(endereco.Cep) &&
                            string.IsNullOrWhiteSpace(endereco.Logadouro))
                        {
                            var resultadoCep = await _cepService.BuscarEnderecoPorCepAsync(endereco.Cep);

                            if (!resultadoCep.Sucesso || resultadoCep.Dados is null)
                                return Retorno<string>.Falha(resultadoCep.Mensagem ?? "Erro ao buscar endereço.");

                            var enderecoPreenchido = resultadoCep.Dados;

                            endereco.Logadouro = enderecoPreenchido.Logadouro;
                            endereco.Bairro = enderecoPreenchido.Bairro;
                            endereco.Cidade = enderecoPreenchido.Cidade;
                            endereco.Estado = enderecoPreenchido.Estado;
                            endereco.Complemento ??= enderecoPreenchido.Complemento;
                        }
                    }
                }

                var cliente = _mapper.Map<ClienteEntity>(dto);
                cliente.DataCadastro = DateTime.Now;

                var resultadoId = await _repository.AdicionarAsync(cliente);

                if (!resultadoId.Sucesso || resultadoId.Dados == 0)
                    return Retorno<string>.Falha(resultadoId.Mensagem ?? "Erro ao cadastrar cliente.");

                await _enderecoRepository.AdicionarEnderecosAsync(resultadoId.Dados, cliente.Enderecos.ToList());
                await _contatoRepository.AdicionarContatosAsync(resultadoId.Dados, cliente.Contatos.ToList());

                return Retorno<string>.Ok($"Cliente ID {resultadoId.Dados} cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                return Retorno<string>.Falha($"Erro ao cadastrar cliente: {ex.Message}");
            }
        }

        public async Task<Retorno<ClienteCompletoDto>> AtualizarAsync(int id, ClienteDto dto)
        {
            try
            {
                var resultadoCliente = await _repository.ObterPorIdAsync(id);

                if (!resultadoCliente.Sucesso || resultadoCliente.Dados == null)
                    return Retorno<ClienteCompletoDto>.Falha("Cliente não encontrado.");

                if (dto.Enderecos?.Any() == true)
                {
                    foreach (var endereco in dto.Enderecos)
                    {
                        if (!string.IsNullOrWhiteSpace(endereco.Cep) &&
                            string.IsNullOrWhiteSpace(endereco.Logadouro))
                        {
                            var resultadoCep = await _cepService.BuscarEnderecoPorCepAsync(endereco.Cep);

                            if (!resultadoCep.Sucesso || resultadoCep.Dados == null)
                                return Retorno<ClienteCompletoDto>.Falha(resultadoCep.Mensagem ?? "Erro ao buscar endereço.");

                            var enderecoPreenchido = resultadoCep.Dados;
                            endereco.Logadouro = enderecoPreenchido.Logadouro;
                            endereco.Bairro = enderecoPreenchido.Bairro;
                            endereco.Cidade = enderecoPreenchido.Cidade;
                            endereco.Estado = enderecoPreenchido.Estado;
                            endereco.Complemento ??= enderecoPreenchido.Complemento;
                        }
                    }
                }

                var clienteAtualizado = _mapper.Map<ClienteEntity>(dto);
                clienteAtualizado.Id = id;
                clienteAtualizado.DataCadastro = resultadoCliente.Dados.DataCadastro;
                clienteAtualizado.DataAlteracao = DateTime.Now;

                await _repository.AtualizarAsync(clienteAtualizado);

                var clienteAtualizadoCompleto = await _repository.ObterPorIdAsync(id);
                if (!clienteAtualizadoCompleto.Sucesso || clienteAtualizadoCompleto.Dados == null)
                    return Retorno<ClienteCompletoDto>.Falha("Erro ao recuperar cliente atualizado.");

                return Retorno<ClienteCompletoDto>.Ok(clienteAtualizadoCompleto.Dados);
            }
            catch (Exception ex)
            {
                return Retorno<ClienteCompletoDto>.Falha($"Erro ao atualizar cliente: {ex.Message}");
            }
        }
        public async Task<Retorno<string>> RemoverAsync(int id)
        {
            try
            {
                var cliente = await _repository.ObterPorIdAsync(id);
                if (!cliente.Sucesso || cliente.Dados is null)
                    return Retorno<string>.Falha("Cliente não encontrado.");

                await _repository.RemoverAsync(id);
                return Retorno<string>.Ok("Cliente removido com sucesso.");
            }
            catch (Exception ex)
            {
                return Retorno<string>.Falha($"Erro ao remover cliente: {ex.Message}");
            }
        }
    }
}