using AutoMapper;
using RotaViagem.Application.DTOs;
using RotaViagem.Domain.Entities;

namespace RotaViagem.Application.Mapping
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Rota, RotaDto>()
                .ForMember(dest => dest.Origem, src => src.MapFrom(s => s.Origem))
                .ForMember(dest => dest.Destino, src => src.MapFrom(s => s.Destino))
                .ForMember(dest => dest.Valor, src => src.MapFrom(s => s.Valor))
                .ReverseMap();
            //CreateMap<Rota, RotaDto>().ReverseMap();
        }
    }
}
