using AutoMapper;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Entities;

namespace Questao5.Application.Mapper
{
    public class OrderingMappingProfile : Profile
    {
        public OrderingMappingProfile()
        {
            CreateMap<Movimento, AddMovimentoCommandRequest>()
                .ReverseMap()
                .ForMember(dest => dest.IdMovimento, opt => opt.Ignore()) 
                .ForMember(dest => dest.DataMovimento, opt => opt.Ignore());
        }
    }
}
