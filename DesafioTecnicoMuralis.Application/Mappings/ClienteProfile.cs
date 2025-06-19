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
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos));

            CreateMap<ClienteEntity, ClienteDto>()
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos))
                .ForMember(dest => dest.Contatos, opt => opt.MapFrom(src => src.Contatos));

            CreateMap<EnderecoDto, EnderecoEntity>().ReverseMap();
            CreateMap<ContatoDto, ContatoEntity>().ReverseMap();
        }
    }
}