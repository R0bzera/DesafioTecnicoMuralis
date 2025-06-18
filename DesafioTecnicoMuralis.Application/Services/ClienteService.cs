using AutoMapper;
using DesafioTecnicoMuralis.API.DTOs;
using DesafioTecnicoMuralis.Application.Interfaces.Repository;
using DesafioTecnicoMuralis.Application.Interfaces.Service;
using DesafioTecnicoMuralis.Domain.Entities;

namespace DesafioTecnicoMuralis.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICepService _cepService;
        private readonly IEnderecoRepository _enderecoRepository;

        public ClienteService(IClienteRepository repository, IMapper mapper, ICepService cepService, IEnderecoRepository enderecoRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _cepService = cepService;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<IEnumerable<ClienteDto>> ListarTodosAsync()
        {
            var clientes = await _repository.ListarTodosAsync();
            return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto?> ObterPorIdAsync(int id)
        {
            var cliente = await _repository.ObterPorIdAsync(id);
            return cliente is null ? null : _mapper.Map<ClienteDto>(cliente);
        }

        public async Task CriarAsync(ClienteDto dto)
        {
            if (dto.Enderecos?.Any() == true)
            {
                foreach (var endereco in dto.Enderecos)
                {
                    if (!string.IsNullOrWhiteSpace(endereco.Cep) &&
                        string.IsNullOrWhiteSpace(endereco.Rua))
                    {
                        var enderecoPreenchido = await _cepService.BuscarEnderecoPorCepAsync(endereco.Cep);
                        if (enderecoPreenchido != null)
                        {
                            endereco.Rua = enderecoPreenchido.Rua;
                            endereco.Bairro = enderecoPreenchido.Bairro;
                            endereco.Cidade = enderecoPreenchido.Cidade;
                            endereco.Estado = enderecoPreenchido.Estado;
                        }
                    }
                }
            }

            var cliente = _mapper.Map<ClienteEntity>(dto);
            cliente.DataCadastro = DateTime.Now;
            cliente.DataAlteracao = DateTime.Now;

            await _repository.AdicionarAsync(cliente);
        }

        public async Task AtualizarAsync(int id, ClienteDto dto)
        {
            var clienteExistente = await _repository.ObterPorIdAsync(id);
            if (clienteExistente is null)
                throw new InvalidOperationException("Cliente não encontrado.");

            var clienteAtualizado = _mapper.Map<ClienteEntity>(dto);
            clienteAtualizado.Id = id;
            clienteAtualizado.DataCadastro = clienteExistente.DataCadastro;
            clienteAtualizado.DataAlteracao = DateTime.Now;

            await _repository.AtualizarAsync(clienteAtualizado);
        }

        public async Task RemoverAsync(int id)
        {
            var cliente = await _repository.ObterPorIdAsync(id);
            if (cliente is null)
                throw new InvalidOperationException("Cliente não encontrado.");

            await _repository.RemoverAsync(id);
        }
    }
}