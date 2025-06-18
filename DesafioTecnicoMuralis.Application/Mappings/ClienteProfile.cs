using AutoMapper;
using DesafioTecnicoMuralis.API.DTOs;
using DesafioTecnicoMuralis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Application.Mappings
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteDto, ClienteEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
                .ForMember(dest => dest.DataAlteracao, opt => opt.Ignore());

            CreateMap<EnderecoDto, EnderecoEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ContatoDto, ContatoEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}