using AutoMapper;
using DesafioTecnicoMuralis.API.DTOs;
using DesafioTecnicoMuralis.Infrastructure.ExternalEntities;
using DesafioTecnicoMuralis.Infrastructure.Servies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTecnicoMuralis.Infrastructure.Mappings
{
    public class ViaCepProfile : Profile
    {
        public ViaCepProfile()
        {
            CreateMap<ViaCepRetorno, EnderecoDto>()
                .ForMember(dest => dest.Cep, opt => opt.Ignore())
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => string.Empty))
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => string.Empty))
                .ForMember(dest => dest.Logadouro, opt => opt.MapFrom(src => src.Logradouro))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Localidade))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Uf));
        }
    }
}